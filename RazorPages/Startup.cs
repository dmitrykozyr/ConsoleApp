using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopsyTurvyCakes.Models;

namespace RazorPages
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            // Настраиваем Dependeny Injection
            services.AddTransient<IRecipesService, RecipesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // Чтобы программа смотрела в папку wwwroot
            app.UseMvcWithDefaultRoute();
        }
    }
}
