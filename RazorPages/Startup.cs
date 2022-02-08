using Microsoft.AspNetCore.Authentication.Cookies;
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
            // Включаем аутентификацию
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .AddRazorPagesOptions(options => // Включаем аутентификацию
                    {
                        options.Conventions.AuthorizeFolder("/Admin");
                        options.Conventions.AuthorizeFolder("/Account");

                        // На эту страницу можно зайти без авторизации
                        options.Conventions.AllowAnonymousToPage("/Account/Login");
                    });

            // Настраиваем Dependeny Injection
            services.AddTransient<IRecipesService, RecipesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();    // Включаем аутентификацию

            app.UseStaticFiles();       // Чтобы программа смотрела в папку wwwroot
            app.UseMvcWithDefaultRoute();
        }
    }
}
