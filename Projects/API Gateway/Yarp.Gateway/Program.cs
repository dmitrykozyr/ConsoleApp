using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://localhost:5009", "https://localhost:5010");

builder.Services
 .AddReverseProxy()
 .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapReverseProxy();

await app.RunAsync();
