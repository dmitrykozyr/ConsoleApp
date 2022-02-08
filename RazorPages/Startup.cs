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
            // �������� ��������������
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .AddRazorPagesOptions(options => // �������� ��������������
                    {
                        options.Conventions.AuthorizeFolder("/Admin");
                        options.Conventions.AuthorizeFolder("/Account");

                        // �� ��� �������� ����� ����� ��� �����������
                        options.Conventions.AllowAnonymousToPage("/Account/Login");
                    });

            // ����������� Dependeny Injection
            services.AddTransient<IRecipesService, RecipesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();    // �������� ��������������

            app.UseStaticFiles();       // ����� ��������� �������� � ����� wwwroot
            app.UseMvcWithDefaultRoute();
        }
    }
}
