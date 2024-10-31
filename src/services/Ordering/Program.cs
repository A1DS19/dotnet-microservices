#region Builder Configuration
var builder = WebApplication.CreateBuilder(args);

#endregion

#region Services Configuration
var app = builder.Build();

app.Run();

#endregion
