using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(
        "fixed",
        options =>
        {
            // Means on the span of 10 seconds, only 5 requests are allowed
            options.Window = TimeSpan.FromSeconds(10);
            options.PermitLimit = 5;
        }
    );
});

var app = builder.Build();

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
