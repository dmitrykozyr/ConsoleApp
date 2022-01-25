using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiNetCore.DatabaseContext;

namespace WebApiNetCore
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
            // Указываем имя БД
            services.AddDbContext<ShopContext>(options => options.UseInMemoryDatabase("Shop"));
            services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        // Если в URL запроса невалиджные данные (строка вместо числа),
                        // то с true не будет возвращаться код 400 BadRequest
                        options.SuppressModelStateInvalidFilter = true;
                    });
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
}
