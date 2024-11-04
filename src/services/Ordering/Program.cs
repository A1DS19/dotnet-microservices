using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

#region Builder Configuration
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAPIServices(builder.Configuration);

#endregion

#region Services Configuration
var app = builder.Build();

app.Run();

#endregion
