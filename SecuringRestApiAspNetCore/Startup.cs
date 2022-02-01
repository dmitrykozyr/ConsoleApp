using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecuringRestApiAspNetCore.DatabaseContext;
using SecuringRestApiAspNetCore.Filters;
using SecuringRestApiAspNetCore.Infrastructure;
using SecuringRestApiAspNetCore.Models;
using SecuringRestApiAspNetCore.Services;
using System;

namespace SecuringRestApiAspNetCore
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
            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
                options.Filters.Add<LinkRewritingFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddControllers();

            // Добавил это
            services.AddRouting(options => options.LowercaseUrls = true);

            // Подключение сваггера
            services.AddSwaggerGen();

            // Настройка версионирования
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(0, 1);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp", policy => policy.AllowAnyOrigin());
            });

            // Подтянуть конфгурацию из appsettings.json
            services.Configure<HotelInfo>(Configuration.GetSection("Info"));

            // Use InMemory DB
            services.AddDbContext<HotelApiDbContext>(options => options.UseInMemoryDatabase("kozyrdb"));

            services.AddScoped<IRoomService, DefaultRoomService>();

            // Подключение AutoMapper
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

            // Authentication and authorization
            AddIdentityCoreServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Добавил для секьюрности
                app.UseHsts();
            }

            // Это ненужно, если выше прописано options.Filters.Add<RequireHttpsOrCloseAttribute>()
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Подключение сваггера
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            // Указываем то имя, которое определили выше
            app.UseCors("AllowMyApp");
        }

        private void AddIdentityCoreServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>();
            builder = new IdentityBuilder(
                builder.UserType,
                typeof(UserRoleEntity),
                builder.Services);

            builder.AddRoles<UserRoleEntity>()
                .AddEntityFrameworkStores<HotelApiDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<UserEntity>>();
        }
    }
}
