using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMvc
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(z => new FeatureToggles
            {
                DeveloperExceptions = _configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");

            // ��� �� ������� -> Properties -> Debug
            // ���� ��� ���������� ��������� ASPNETCORE_ENVIRONMENT ������ 'Development'
            // ��������� 'Production', �� ��� ���� �� ����� �����������
            // EnableDeveloperExceptions - ��� ���� ���������� ���������, ������� ���-�� ��������
            //if (_configuration["EnableDeveloperExceptions"] == "True")

            // ��� ����� �������� ���������� � ������ � appsettings.json
            //if (_configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions"))

            // ��� ������������ ����� FeatureToggles 
            if (features.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            // ������� ������, ���� ����� �������� �������� ����� invalid
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new System.Exception("ERROR!");

                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseFileServer();
        }
    }
}
