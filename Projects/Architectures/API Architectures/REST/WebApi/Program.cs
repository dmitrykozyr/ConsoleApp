using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interfaces;

#region Регистрация сервисов, время жизни объектов

/*
    После регистрации сервиса, вместо объектов интерфейса IMessageSender
    будут передаваться экземпляры класса EmailMessageSender
    Теперь сервисы можно использовать в любой части приложения

    - Transient  При каждом обращении к сервису создается новый объект сервиса
                 Подходит для сервисов, которые не хранят данные об состоянии

    - Scoped     Для каждого запроса создается объект сервиса
                 Если в течение одного запроса есть несколько обращений к сервису -
                 будет использован один и тот же объект

    - Singleton  Объект сервиса создается один раз при первом обращении к нему
*/

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
// Интерфейс необязателен, но служит для Dependency Inversion
builder.Services.AddTransient<IMessageSender, EmailMessageSender>();
builder.Services.AddTransient<SmsMessageSender>();

builder.Services.AddDbContext<DbContextSetUp>(options => options.UseInMemoryDatabase("DatabaseName"));
builder.Services.AddTransient<DbContextSetUp>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Amazing Swagger", Version = "v1" });
});

#endregion

#region Настройка конфигурации
/*
    static IHostBuilder CreateHostBuilder(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((appConfiguration) =>
            {
                // Подключение файла конфигурации JSON
                appConfiguration.AddJsonFile("settings.json", false, true);

                // Подключение файла конфигурации XML
                appConfiguration.AddXmlFile("settings.xml", false, true);
            })
            .ConfigureDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .UseNLog();
    }

    // Примеры, как получить данные из конфигурации в другом месте программы
    public string GetConfigJSON()
    {
        return _confuguration["settings:SomeConfiguration"];
    }

    public string GetConfigXML()
    {
        return _confuguration["SomeConfiguration1"];
    }
*/
#endregion

#region Middleware

/*
    Конвейер обработки Middleware конфигурируется методами Use, Run, Map, важен порядок:
    Use - добавление компонентов middleware в конвейер
    Run - замыкающий метод добавления компонентов middleware в конвейер
    Map - применяется для сопоставления пути запроса с делегатом, который будет обрабатывать запрос по этому пути

    Компоненты Middleware по умолчанию:
    - Authentication
    - Cookie Policy          отслеживает согласие пользователя на хранение информации в куках
    - CORS                   поддержка кроссдоменных запросов
    - Diagnostics            предоставляет страницы статус кодов, функционал обработки исключений
    - Forwarded Headers      перенаправляет зголовки запроса
    - Health Check           проверяет работоспособность приложения
    - HTTP Method Override   позволяет входящему POST - запросу переопределить метод
    - HTTPS Redirection      перенаправляет все запросы HTTP на HTTPS
    - HTTP Strict Transport Security     для безопасности добавляет специальный заголовок ответа
    - MVC                    обеспечивает функционал MVC
    - Request Localization   обеспечивает поддержку локализации
    - Response Caching       позволяет кэшировать результаты запросов
    - Response Compression   обеспечивает сжатие ответа клиенту
    - URL Rewrite            предоставляет функциональность URL Rewriting
    - Endpoint Routing       предоставляет механизм маршрутизации
    - Session                предоставляет поддержку сессий
    - Static Files           предоставляет поддержку обработки статических файлов
    - WebSockets             добавляет поддержку протокола WebSockets

    Метод Configure выполняется один раз при создании объекта класса Startup,
    а компоненты middleware создаются один раз и живут в течение всего жизненного цикла приложения,
    вызываются после каждого HTTP - запроса
*/

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    // В случае ошибки, выводим информацию о ней
    app.UseSwagger();                   // Для сваггера устанавливаем Swashbuckle.AspNetCore
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

    // Для запросов по маршруту http://localhost:XXXXX/ будет выводиться текст
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync($"Hello");
    });
});
app.MapControllers();

app.Run();

#endregion
