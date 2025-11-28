using Microsoft.FeatureManagement;
using Presentation.Extensions;
using Serilog;

// WebHost.CreateDefaultBuilder()
/*
    Создание веб-приложений на ASP.NET Core
    Создает и настраивает IWebHostBuilder, отвечающий за настройку веб-сервера (например, Kestrel) и конфигурацию для веб-приложений
    Автоматически настраивает параметры:
    – чтение конфигурации из appsettings.json, переменных окружения и аргументов командной строки
    – настройка логирования
    – настройка Kestrel и других серверов
    Используется для приложений ASP.NET Core до версии 6
*/

// Host.CreateDefaultBuilder()
/*
    Создание общих приложений .NET (не обязательно веб)
    Создает и настраивает IHostBuilder, который можно использовать для консольных и веб-приложений
    Автоматически настраивает конфигурацию и логирование, но более универсален
    Подходит для приложений, которые могут не включать веб-сервер
    Рекомендуется использовать для создания фоновых служб, консольных и ASP.NET Core приложений, начиная с версии 3.0
*/

// WebApplication.CreateBuilder()
/*
    Введен в ASP.NET Core 6 как упрощенный способ создания веб-приложений
    Создает WebApplicationBuilder, объединяеющий возможности Host и WebHost в одном подходе
    Позволяет добавлять службы, маршруты и middleware
    Автоматически включает конфигурацию и логирование, позволяет легко добавлять компоненты приложения в одном месте
    Рекомендуется использовать для новых проектов ASP.NET Core, так как этот подход более современный и упрощает процесс настройки приложения
    Предлагает более высокоуровневый API для настройки приложения, комбинируя функциональность обоих предыдущих методов
*/

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;

//! Подключил, установил пакет и настроил в appsettings, а как использовать?
builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

serviceCollection.AddSwaggerGen();
serviceCollection.AddHttpClient();
serviceCollection.AddControllers();
serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication();
serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddControllersWithViews();
serviceCollection.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

serviceCollection.AddQuartzExtension();
serviceCollection.AddRedisExtensions(builder);
serviceCollection.AddOptionsExtensions();
serviceCollection.AddServicesExtensions();
serviceCollection.AddRepositoriesExtensions();
serviceCollection.AddOpenTelemetryExtension(builder);
IConfiguration configuration = serviceCollection.AddConfigurationExtension(builder);
serviceCollection.AddDbExtensions(configuration);


WebApplication app = builder.Build();

serviceCollection.AddVaultExtensions(app, builder, configuration);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "FileDownload",
    pattern: "{controller=Home}/{action=FileDownload}/{id?}");

app.Run();
