using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

#region Services
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder
    .Services.AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
        options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

#endregion

#region Builder

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.MapCarter();

app.Run();
#endregion
