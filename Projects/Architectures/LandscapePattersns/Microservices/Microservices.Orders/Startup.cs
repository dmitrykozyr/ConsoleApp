using Microservices.Orders.DB;
using Microservices.Orders.Interfaces;
using Microservices.Orders.Providers;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Orders;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<OrdersDbContext>(options =>
        {
            options.UseInMemoryDatabase("Customers");
        });

        services.AddScoped<IOrdersProvider, OrdersProvider>();

        services.AddAutoMapper(typeof(Startup));

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
