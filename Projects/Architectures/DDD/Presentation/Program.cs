using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Domain.Services;
using Domain.Services.API;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;
using Infrastructure.Vault;
using Microsoft.Extensions.Options;

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

//!
//apiVersion
//IOptions
// keycloak
// authorization
// authentication

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Services
builder.Services.AddScoped<IFileService, FileService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();

// IOptions
builder.Services.AddScoped<IOptions<VaultOptions>>();


#region ConfigurationBuilder

string environment = $"appsettings.{builder.Environment.EnvironmentName}.json";
builder.Configuration.AddJsonFile(environment, optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables(prefix: "VAULT_");
var cb = new ConfigurationBuilder();
cb.SetBasePath(Directory.GetCurrentDirectory());
cb.AddJsonFile(environment, optional: true, reloadOnChange: true);
cb.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
IConfiguration configuration = cb.Build();

//! Ёто нужно?
if (builder.Configuration.GetSection("VaultOptions")["Role"] is not null)
{
    var serviceProvider = builder.Services.BuildServiceProvider();

    var options = serviceProvider.GetRequiredService<IOptions<VaultOptions>>();
    var logging = serviceProvider.GetRequiredService<ILogging>();
    var httpClientData = serviceProvider.GetRequiredService<IHttpClientData<VaultSecrets>>();

    Guard.IsNotNull(options);
    Guard.IsNotNull(logging);
    Guard.IsNotNull(httpClientData);

    builder.Configuration.AddVault(
        //!
        //options =>
        //{
        //    IConfigurationSection? vaultOptions = builder.Configuration.GetSection("VaultOptions");

        //    options.Address     = vaultOptions["Address"];
        //    options.Role        = vaultOptions["Role"];
        //    options.MountPath   = vaultOptions["MountPath"];
        //    options.SecretType  = vaultOptions["SecretType"];
        //    options.Secret      = vaultOptions["Secret"];
        //},
        options,
        logging,
        configuration,
        httpClientData
    );
}

#endregion


#region Middleware

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); //! что это?
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
