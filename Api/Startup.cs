using Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    // Компоненты middleware конфигурируются методами расширения
    // Run, Map, Use объекта IApplicationBuilder, которые передается в Configure()
    // Для создания компонентов middleware используется делегат RequestDelegate,
    // который выполняет действие и принимает контекст запроса

    // Все вызовы app.UseXXX представляют добавление компонентов middleware для обработки запроса
    // Получается конвейер обработки:
    // - компонент обработки ошибок Diagnostics добавляется через app.UseDeveloperExceptionPage()
    // - компонент маршрутизации EndpointRoutingMiddleware добавляется через app.UseRouting()
    // - компонент EndpointMiddleware отправляет ответ, если запрос пришел по маршруту "/", то есть
    //   пользователь обратился к корню приложения - добавляется через метод app.UseEndpoints()

    // Важен порядок определения компонентов, иначе приложение не будет работать
    // Например, ниже сначала добавляются компоненты маршрутизации app.UseRouting(),
    // потом компонент обработки запроса по определенному маршруту app.UseEndpoints()
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        // Необязательный метод для реистрации сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            //const string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
            //services.AddDbContext<UsersContext>(options => options.UseSqlServer(con));

            services.AddDbContext<DbContextSetUp>(opt => opt.UseInMemoryDatabase("DatabaseName"));
            services.AddControllers();

            services.AddSwaggerGen(); // Подключаем сваггер
        }

        // Обязательный метод для настройки того, как приложение будет обрабатывать запрос
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage(); // В случае ошибки, выводим информацию о ней

                // Для добавления сваггера, через NuGet устанавливаем Swashbuckle и Swashbuckle.AspNetCore
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Title");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();               // Включаем маршрутизацию, чтобы приложение соотносило запросы с маршрутами
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>   // Устанавливаем адреса, которые будут обрабатываться
            {
                endpoints.MapControllers();

                // Для запросов по маршруту http://localhost:61922/ будет выводиться текст
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello");
                });
            });
        }
    }
}
