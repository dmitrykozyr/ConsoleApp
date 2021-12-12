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

// �������� �������: ASP.NET Core Web Application
// ��� ECommerce.Api.Products
// �������� API, ������� ������� Configure for HTTPS

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
            services.AddScoped<IProductsProvider, ProductsProvider>();  // ����� �����, ����������� �� IProductsProvider, ����� ��������������, ����� �������� ���������
            services.AddAutoMapper(typeof(Startup));                    // ��������� ����������
            services.AddDbContext<ProductsDbContext>(options =>         // ������������ ��� DbContext
            {
                options.UseInMemoryDatabase("Products");                // ������ �������� InMemory ��
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
