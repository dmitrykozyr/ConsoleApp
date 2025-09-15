using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Domain.Services;
using Domain.Services.API;
using Domain.Services.Login;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;
using Infrastructure.Vault;
using Microsoft.Extensions.Options;
using Presentation;

/*
    ≈сть три способа настройки приложений:

    1. WebHost.CreateDefaultBuilder()

        Х —оздание веб-приложений на ASP.NET Core
        Х —оздает и настраивает IWebHostBuilder, отвечающий за настройку веб-сервера (например, Kestrel) и конфигурацию дл€ веб-приложений
        Х јвтоматически настраивает параметры:
          Ц чтение конфигурации из appsettings.json, переменных окружени€ и аргументов командной строки
          Ц настройка логировани€
          Ц настройка Kestrel и других серверов
        Х »спользуетс€ дл€ приложений ASP.NET Core до версии 6

    2. Host.CreateDefaultBuilder()

        Х —оздание общих приложений .NET (не об€зательно веб)
        Х —оздает и настраивает IHostBuilder, который можно использовать дл€ консольных и веб-приложений
        Х јвтоматически настраивает конфигурацию и логирование, но более универсален
        Х ѕодходит дл€ приложений, которые могут не включать веб-сервер
        Х –екомендуетс€ использовать дл€ создани€ фоновых служб, консольных и ASP.NET Core приложений, начина€ с версии 3.0

    3. WebApplication.CreateBuilder()

        Х ¬веден в ASP.NET Core 6 как упрощенный способ создани€ веб-приложений
        Х —оздает WebApplicationBuilder, объедин€еющий возможности Host и WebHost в одном подходе
        Х ѕозвол€ет добавл€ть службы, маршруты и middleware
        Х јвтоматически включает конфигурацию и логирование, позвол€ет легко добавл€ть компоненты приложени€ в одном месте
        Х –екомендуетс€ использовать дл€ новых проектов ASP.NET Core, так как этот подход более современный и упрощает процесс настройки приложени€
        Х ѕредлагает более высокоуровневый API дл€ настройки приложени€, комбиниру€ функциональность обоих предыдущих методов
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddApiVersioning();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

#region ConfigurationBuilder

string environment = $"appsettings.{builder.Environment.EnvironmentName}.json";
builder.Configuration.AddJsonFile(environment, optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables(prefix: "VAULT_");

var cb = new ConfigurationBuilder();
cb.SetBasePath(Directory.GetCurrentDirectory());
cb.AddJsonFile(environment, optional: true, reloadOnChange: true);
cb.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
IConfiguration configuration = cb.Build();

#endregion

// IOption
builder.Services.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<GeneralOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<LoginOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDbConStrService, DbConStrService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();
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
