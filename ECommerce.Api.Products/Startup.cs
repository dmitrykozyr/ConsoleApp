using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Создание проекта: ASP.NET Core Web Application
// Имя ECommerce.Api.Products
// Выбираем API, убираем галочку Configure for HTTPS

namespace ECommerce.Api.Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddScoped<IProductsProvider, ProductsProvider>();  // Какой класс, наследуемый от IProductsProvider, будет использоваться, когда инжектим интерфейс
            services.AddAutoMapper(typeof(Startup));                    // Добавляем автомаппер
            services.AddDbContext<ProductsDbContext>(options =>         // Регистрируем наш DbContext
            {
                options.UseInMemoryDatabase("Products");                // Задаем название InMemory БД
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
