using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

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

        // Необязательный метод для регистрации сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            // После регистрации сервиса, вместо объектов интерфейса IMessageSender будут передаваться экземпляры класса EmailMessageSender
            // Теперь сервисы можно использовать в любой части приложения
            
            // - Transient  При каждом обращении к сервису создается новый объект сервиса, подходит для сервисов, которые не хранят данных о состоянии
            // - Scoped     Для каждого запроса создается свой объект сервиса
            //              Если в течение одного запроса есть несколько обращений к одному сервису,
            //              то при всех этих обращениях будет использоваться один и тот же объект сервиса
            // - Singleton  Объект сервиса создается один раз при первом обращении к нему
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<SmsMessageSender>(); // Интерфейс здесь необязателен

            //const string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
            //services.AddDbContext<UsersContext>(options => options.UseSqlServer(con));

            services.AddDbContext<DbContextSetUp>(opt => opt.UseInMemoryDatabase("DatabaseName"));
            services.AddControllers();

            services.AddSwaggerGen(); // Подключаем сваггер
        }

        // Обязательный метод для настройки того, как приложение будет обрабатывать запрос        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware настраивается здесь с помощью методов расширений Run, Map и Use объекта IApplicationBuilder
            // Вызовы app.Use___ - это добавление компонентов middleware, их порядок играет большую роль
            // Компоненты Middleware по умолчанию:
            // - Authentication
            // - Cookie Policy          отслеживает согласие пользователя на хранение информации в куках
            // - CORS                   поддержка кроссдоменных запросов
            // - Diagnostics            предоставляет страницы статусных кодов, функционал обработки исключений, страницу исключений разработчика
            // - Forwarded Headers      перенаправляет зголовки запроса
            // - Health Check           проверяет работоспособность приложения
            // - HTTP Method Override   позволяет входящему POST - запросу переопределить метод
            // - HTTPS Redirection      перенаправляет все запросы HTTP на HTTPS
            // - HTTP Strict Transport Security для безопасности добавляет специальный заголовок ответа
            // - MVC                    обеспечивает функционал MVC
            // - Request Localization   обеспечивает поддержку локализации
            // - Response Caching       позволяет кэшировать результаты запросов
            // - Response Compression   обеспечивает сжатие ответа клиенту
            // - URL Rewrite            предоставляет функциональность URL Rewriting
            // - Endpoint Routing       предоставляет механизм маршрутизации
            // - Session                предоставляет поддержку сессий
            // - Static Files           предоставляет поддержку обработки статических файлов
            // - WebSockets             добавляет поддержку протокола WebSockets

            // Метод Configure выполняется один раз при создании объекта класса Startup,
            // а компоненты middleware создаются один раз и живут в течение всего жизненного цикла приложения,
            // они вызываются после каждого HTTP - запроса

            // Методы Middleware Use, Run, Map
            // Use - добавление компонентов middleware в конвейер
            // Run - замыкающий метод добавления компонентов middleware в конвейер
            
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

            // Последовательность работы Middleware:
            // Определена переменная x = 2, последующие вызовы middleware увеличивают ее значение в два раза
            // 1. Вызов компонента app.Use
            // 2. Увеличение переменной x в два раза: x = x * 2, х равно 4
            // 3. Вызов await next.Invoke(), управление переходит следующему компоненту в конвейере - app.Run
            // 4. Увеличение переменной x в два раза: x = x * 2, х равно 8
            // 5. Метод app.Run закончил работу, управление возвращается к app.Use, начинает выполняться код после await next.Invoke()
            // 6. Увеличение переменной x в два раза: x = x * 2, х равно 16
            // 7. Отправка ответа клиенту с помощью вызова await context.Response.WriteAsync($"Result: {x}")
            int x = 2;
            app.Use(async (context, next) =>
            {
                x = x * 2;              // 2 * 2 = 4
                await next.Invoke();    // вызов app.Run
                x = x * 2;              // 8 * 2 = 16
                await context.Response.WriteAsync($"Result: {x}");
            });

            app.Run(async (context) =>
            {
                x = x * 2;              //  4 * 2 = 8
                await Task.FromResult(0);
            });

            // Метод Map (и методы расширения MapXXX()) применяется для сопоставления пути запроса с определенным делегатом,
            // который будет обрабатывать запрос по этому пути
            app.Map("/index", Index);

            // Теперь обращения http://localhost:xxxx/index будут обрабатываться методом Index
            // Остальные запросы будут обработаны делегатом из app.Run()
            static void Index(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Index");
                });
            }

            // Похожим образом работает MapWhen() - принимает делегат Func<HttpContext, bool> и обрабатывает запрос, 
            // если передаваемая ф-я возвращает true
            app.MapWhen(context => {

                return context.Request.Query.ContainsKey("id") &&
                        context.Request.Query["id"] == "5";
            }, HandleId);

            // Добавляем созданный нами Middleware, который принимает токен, но здесь мы его не передаем
            // Передаем его через строку запроса localhost::64405/?token=12345678
            app.UseMiddleware<TokenMiddleware>();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Good bye, World...");
            });
        }

        static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("id is equal to 5");
            });
        }
    }
}
