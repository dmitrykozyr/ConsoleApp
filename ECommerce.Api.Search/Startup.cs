using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;

namespace ECommerce.Api.Search
{
    // �������� �� �������� ��� -> Set startup project -> �������� ������ ������� ��� Startup,
    // ����� ��� ��� ����������� ��� ������ ���������

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICustomersService, CustomersService>();

            // ��������� ����������� �������� � �������� Orders �� HTTP
            services.AddHttpClient("OrdersService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Orders"]);
            });

            // � NuGet ������������� Microsoft.Extensions.Http.Polly, ����� �
            // WaitAndRetryAsync ����� ���� �������, ������� ��� �������� ���������
            // ������ � ������ ������� � ��������� ����� ���������
            services.AddHttpClient("ProductsService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Products"]);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            services.AddHttpClient("CustomersService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Customers"]);
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
