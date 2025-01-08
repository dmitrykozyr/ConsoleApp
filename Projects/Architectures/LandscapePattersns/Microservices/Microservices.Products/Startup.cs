using Microservices.Products.DB;
using Microservices.Products.Interfaces;
using Microservices.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Products;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IProductsProvider, ProductsProvider>();
        services.AddAutoMapper(typeof(Startup));
        services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseInMemoryDatabase("Products");
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
