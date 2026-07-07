var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();



var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => new
{
    Message     = "Hello from Kubernetes!",
    Hostname    = Environment.MachineName,  // »м€ пода - при нескольких репликах видно балансировку
                                            //  огда запущено несколько копий программы, по этому полю видно, кака€ именно копи€ ответила
                                            // “ак провер€ют, что запросы распредел€ютс€ между копи€ми
    Time = DateTime.UtcNow,
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
