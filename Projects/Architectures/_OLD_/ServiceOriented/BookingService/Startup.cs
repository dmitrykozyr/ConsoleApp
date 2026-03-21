using BookingService.DataAccess;
using BookingService.DataAccess.Interfaces;
using BookingService.ESB;
using BookingService.ESB.Interfaces;

namespace BookingService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddTransient<IBookingsRepository, BookingsRepository>();
        services.AddTransient<IESBroxy, ESBProxy>();
        services.AddAutoMapper(typeof(AutoMapperProfile));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
