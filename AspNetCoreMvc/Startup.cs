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

            // ПКМ на проекте -> Properties -> Debug
            // Если для переменной окружения ASPNETCORE_ENVIRONMENT вместо 'Development'
            // прописать 'Production', то код ниже не будет выполняться
            // EnableDeveloperExceptions - это тоже переменная окружения, которую там-же добавили
            //if (_configuration["EnableDeveloperExceptions"] == "True")

            // Или можно добавить переменную в конфиг в appsettings.json
            if (_configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions"))
            {
                app.UseDeveloperExceptionPage();
            }

            // Вывести ошибку, если адрес страницы содержит слово invalid
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
