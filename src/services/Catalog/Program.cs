var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/catalog", () => new { Name = "Catalog API", Version = "1.0" });

app.Run();
