using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.RateLimiting;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Конфигурация Yarp
builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Конфигурация балансировщика
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("FixedWindow", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Request.Path,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                Window = TimeSpan.FromSeconds(10)
            }));
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Конфигурация Yarp
app.MapReverseProxy();

// Конфигурация балансировщика
app.UseRateLimiter();

await app.RunAsync();
