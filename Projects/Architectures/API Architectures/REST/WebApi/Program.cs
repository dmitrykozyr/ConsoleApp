using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interfaces;

// После регистрации сервиса, вместо объектов интерфейса IMessageSender будут передаваться экземпляры класса EmailMessageSender
// Теперь сервисы можно использовать в любой части приложения

// - Transient  При каждом обращении к сервису создается новый объект сервиса
//              Подходит для сервисов, которые не хранят данных о состоянии
// - Scoped     Для каждого запроса создается объект сервиса
//              Если в течение одного запроса есть несколько обращений к сервису,
//              то при всех этих обращениях будет использоваться один и тот же объект сервиса
// - Singleton  Объект сервиса создается один раз при первом обращении к нему

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMessageSender, EmailMessageSender>();
builder.Services.AddTransient<SmsMessageSender>(); // Интерфейс здесь необязателен
//const string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
//builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(con));
builder.Services.AddDbContext<DbContextSetUp>(options => options.UseInMemoryDatabase("DatabaseName"));
builder.Services.AddTransient<WebApi.Models.DbContextSetUp>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Amazing Swagger", Version = "v1" });
}); // Подключаем сваггер

// Middleware конфигурируется здесь методами расширения Use, Run, Map (app.UseXXX) объекта IApplicationBuilder
// Use - добавление компонентов middleware в конвейер
// Run - замыкающий метод добавления компонентов middleware в конвейер
// Map - применяется для сопоставления пути запроса с определенным делегатом, который будет обрабатывать запрос по этому пути

// Важен порядок определения компонентов

// Все вызовы представляют добавление компонентов middleware для обработки запроса
// Получается конвейер обработки:
// - app.UseDeveloperExceptionPage()    добавляем компонент обработки ошибок
// - app.UseRouting()                   добавляем компонент маршрутизации
// - app.UseEndpoints()                 добавляем компонент, отправляющий ответ, если запрос пришел по маршруту "/",
//                                      то есть пользователь обратился к корню приложения

// Компоненты Middleware по умолчанию:
// - Authentication
// - Cookie Policy          отслеживает согласие пользователя на хранение информации в куках
// - CORS                   поддержка кроссдоменных запросов
// - Diagnostics            предоставляет страницы статус кодов, функционал обработки исключений
// - Forwarded Headers      перенаправляет зголовки запроса
// - Health Check           проверяет работоспособность приложения
// - HTTP Method Override   позволяет входящему POST - запросу переопределить метод
// - HTTPS Redirection      перенаправляет все запросы HTTP на HTTPS
// - HTTP Strict Transport Security     для безопасности добавляет специальный заголовок ответа
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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    // В случае ошибки, выводим информацию о ней
    app.UseSwagger(); // Для добавления сваггера, через NuGet устанавливаем Swashbuckle и Swashbuckle.AspNetCore
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Title");
    });
}
app.UseHttpsRedirection();
app.UseRouting();                       // Включаем маршрутизацию, чтобы приложение соотносило запросы с маршрутами
app.UseAuthorization();
app.UseEndpoints(endpoints =>           // Устанавливаем адреса, которые будут обрабатываться
{
    endpoints.MapControllers();

    // Для запросов по маршруту http://localhost:61922/ будет выводиться текст
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync($"Hello");
    });
});
app.MapControllers();

//MiddlewareExample(app);

app.Run();

// Последовательность работы Middleware
static void MiddlewareExample(WebApplication app)
{
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

    // Теперь обращения http://localhost:xxxx/index будут обрабатываться методом Index
    // Остальные запросы будут обработаны делегатом из app.Run()
    app.Map("/index", Index);

    static void Index(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync("Index");
        });
    }

    // MapWhen() принимает делегат Func<HttpContext, bool> и обрабатывает запрос, если передаваемая ф-я возвращает true
    app.MapWhen(context =>
    {
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
