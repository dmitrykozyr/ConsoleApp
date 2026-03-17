using HealthChecks_.Interfaces;
using HealthChecks_.Services;
using HealthChecks_.ServicesToCheck;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHealthService, HealthService>();

builder.Services.AddSingleton<IHealthCheckable, DatabaseService>();
builder.Services.AddSingleton<IHealthCheckable, CacheService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
