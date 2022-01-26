using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        // https://localhost:44384/.well-known/openid-configuration
        public void ConfigureServices(IServiceCollection services)
        {
            // Для настройки IdentityServer добавляем это
            var builder = services.AddIdentityServer()
                                    .AddInMemoryApiResources(Config.Apis)
                                    .AddInMemoryClients(Config.Clients)
                                    .AddInMemoryApiScopes(Config.Scopes);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Для настройки IdentityServer добавляем это
            app.UseIdentityServer();
        }
    }
}
