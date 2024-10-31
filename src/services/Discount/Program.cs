using Discount.GRPC.Data;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();
app.UseMigration();
app.MapGrpcService<DiscountService>();

app.Run();
