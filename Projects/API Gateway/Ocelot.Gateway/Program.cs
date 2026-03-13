using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

// В консольном приложении нет lauhchSettings
// и так можно указать, на каком порту запускать приложение
builder.WebHost.UseUrls("http://localhost:5007", "https://localhost:5008");

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();


WebApplication app = builder.Build();

app.UseHttpsRedirection();

await app.UseOcelot();
await app.RunAsync();
