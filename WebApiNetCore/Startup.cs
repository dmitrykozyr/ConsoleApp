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

            // Без этого кода и кода app.UseCors() внизу, проект SecuringApi
            // будет выдывать ошибку "Something went wrong...", потому что
            // не может обратиться к серису получения продуктов этого проекта
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    // 44359 - это SSL порт из проекта 'Securing Api'
                    builder.WithOrigins("https://localhost:44359")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:44359";
                options.RequireHttpsMetadata = false;
                options.Audience = "hpt-api";
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false
                };
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

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
