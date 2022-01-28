using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecuringRestApiAspNetCore.Filters;

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
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddControllers();

            // ������� ���
            services.AddRouting(options => options.LowercaseUrls = true);

            // ����������� ��������
            services.AddSwaggerGen();

            // ��������� ���������������
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // ������� ��� �����������
                app.UseHsts();
            }

            // ��� �������, ���� ���� ��������� options.Filters.Add<RequireHttpsOrCloseAttribute>()
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ����������� ��������
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            // ��������� �� ���, ������� ���������� ����
            app.UseCors("AllowMyApp");
        }
    }
}
