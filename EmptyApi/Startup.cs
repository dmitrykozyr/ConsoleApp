using EmptyApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace EmptyApi
{
    public class Startup
    {
        // Компоненты middleware конфигурируются с помощью методов расширений
        // Run, Map и Use объекта IApplicationBuilder, который передается в Configure()
        // Для создания компонентов middleware используется делегат RequestDelegate,
        // который выполняет некоторое действие и принимает контекст запроса

        // Все вызовы типа app.UseXXX представляют собой добавление компонентов middleware
        // для обработки запроса
        // Получается следующий конвейер обработки:
        // - компонент обработки ошибок Diagnostics добавляется через app.UseDeveloperExceptionPage()
        // - компонент маршрутизации EndpointRoutingMiddleware добавляется через app.UseRouting()
        // - компонент EndpointMiddleware отправляет ответ, если запрос пришел по маршруту "/", то есть
        // пользователь обратился к корню приложения - добавляется через метод app.UseEndpoints()
        // Важен порядок определения компонентов
        // Например, в этом методе сначала добавляются компоненты для встраивания механизма
        // маршрутизации app.UseRouting(), а потом компонент обработки запроса по определенному
        // маршруту app.UseEndpoints()
        // Если изменим порядок - приложение нормально работать не будет
        public delegate Task RequestDelegate(HttpContext context);

        IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Необязательный метод для реистрации сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            const string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(con));
            services.AddControllers();
        }

        // Обязательный метод для настройки того, как приложение будет обрабатывать запрос
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Выводим информацию об ошибке, при наличии ошибки
                app.UseDeveloperExceptionPage();
            }

            // Добавляем возможность маршрутизации, благодаря чему приложение
            // может соотносить запросы с определенными маршрутами
            app.UseRouting();

            // Устанавливаем адреса, которые будут обрабатываться
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Для всех запросов по маршруту '/' (http://localhost:6865) в ответ будет выводиться название приложения
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Application Name: {_env.ApplicationName}");
                });
            });
        }
    }
}
