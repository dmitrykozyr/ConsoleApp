using Microsoft.FeatureManagement;
using Presentation.Extensions;

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

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

builder.Services.AddQuartzExtension(builder);
builder.Services.AddRedisExtensions(builder);
builder.Services.AddOptionsExtensions(builder);
builder.Services.AddServicesExtensions(builder);
builder.Services.AddRepositoriesExtensions(builder);
builder.Services.AddOpenTelemetryExtension(builder);
IConfiguration configuration = builder.Services.AddConfigurationExtension(builder);


WebApplication app = builder.Build();

builder.Services.AddVaultExtensions(app, builder, configuration);

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
