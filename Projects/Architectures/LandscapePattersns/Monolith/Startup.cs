using Monolith.DataAccess;
using Monolith.DataAccess.Interfaces;
using Monolith.Services;
using Monolith.Services.Interfaces;

namespace Monolith
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
            services.AddRazorPages();
            services.AddTransient<IToursRepository, ToursRepository>();
            services.AddTransient<IBookingsRepository, BookingsRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITravelAgentService, TravelAgentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
