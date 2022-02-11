using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error.html");

            // ��� �� ������� -> Properties -> Debug
            // ���� ��� ���������� ��������� ASPNETCORE_ENVIRONMENT ������ 'Development'
            // ��������� 'Production', �� ��� ���� �� ����� �����������
            // EnableDeveloperExceptions - ��� ���� ���������� ���������, ������� ���-�� ��������
            //if (_configuration["EnableDeveloperExceptions"] == "True")

            // ��� ����� �������� ���������� � ������ � appsettings.json
            if (_configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions"))
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

            app.UseFileServer();
        }
    }
}
