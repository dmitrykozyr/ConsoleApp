var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();



var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => new
{
    Message     = "Hello from Kubernetes!",
    Hostname    = Environment.MachineName,
    Time        = DateTime.UtcNow,
    AppName     = Environment.GetEnvironmentVariable("APP_NAME"),
    LogLevel    = Environment.GetEnvironmentVariable("LOG_LEVEL")
});

app.MapGet("/api/items", () => new[]
{
    new { Id = 1, Name = "Item One" },
    new { Id = 2, Name = "Item Two" },
    new { Id = 3, Name = "Item Three" }
});

app.Run();
