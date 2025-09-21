using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Login;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Domain.Services;
using Domain.Services.API;
using Domain.Services.Login;
using Infrastructure.HttpClient_;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;
using Infrastructure.Vault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Presentation;
using Presentation.Extensions;

// Три способа настройки приложений
/*
    1. WebHost.CreateDefaultBuilder()

        • Создание веб-приложений на ASP.NET Core
        • Создает и настраивает IWebHostBuilder, отвечающий за настройку веб-сервера (например, Kestrel) и конфигурацию для веб-приложений
        • Автоматически настраивает параметры:
          – чтение конфигурации из appsettings.json, переменных окружения и аргументов командной строки
          – настройка логирования
          – настройка Kestrel и других серверов
        • Используется для приложений ASP.NET Core до версии 6

    2. Host.CreateDefaultBuilder()

        • Создание общих приложений .NET (не обязательно веб)
        • Создает и настраивает IHostBuilder, который можно использовать для консольных и веб-приложений
        • Автоматически настраивает конфигурацию и логирование, но более универсален
        • Подходит для приложений, которые могут не включать веб-сервер
        • Рекомендуется использовать для создания фоновых служб, консольных и ASP.NET Core приложений, начиная с версии 3.0

    3. WebApplication.CreateBuilder()

        • Введен в ASP.NET Core 6 как упрощенный способ создания веб-приложений
        • Создает WebApplicationBuilder, объединяеющий возможности Host и WebHost в одном подходе
        • Позволяет добавлять службы, маршруты и middleware
        • Автоматически включает конфигурацию и логирование, позволяет легко добавлять компоненты приложения в одном месте
        • Рекомендуется использовать для новых проектов ASP.NET Core, так как этот подход более современный и упрощает процесс настройки приложения
        • Предлагает более высокоуровневый API для настройки приложения, комбинируя функциональность обоих предыдущих методов
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient(); //! Настроить для использования IHttpClientFactory
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Extensions
IConfiguration configuration = builder.Services.AddConfigurationExtension(builder);
builder.Services.AddRedisExtension(builder);

// IOptions             Обновляет информацию о конфигурации один раз при старте приложения
// IOptionsSnapshot     Обновляет информацию о конфигурации при каждом запросе и не изменяет ее во время запроса
// IOptionsMonitor      Обновляет информацию о конфигурации при каждом обращении к конфигурации
builder.Services.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<GeneralOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<LoginOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<RedisOptions>>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDbConStrService, DbConStrService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISqlService, SqlService>();
builder.Services.AddScoped<IProvider, Provider>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();


#region Middleware

var app = builder.Build();

#region HashiConf Vault

using (var scope = app.Services.CreateScope())
{
    var logging = scope.ServiceProvider.GetService<ILogging>();
    var httpClientData = scope.ServiceProvider.GetService<IHttpClientData<VaultSecrets>>();
    var vaultOptions = scope.ServiceProvider.GetService<IOptions<VaultOptions>>();

    Guard.IsNotNull(logging);
    Guard.IsNotNull(httpClientData);
    Guard.IsNotNull(vaultOptions);

    builder.Configuration.AddVault(vaultOptions, logging, configuration, httpClientData);
}

#endregion

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

#endregion
