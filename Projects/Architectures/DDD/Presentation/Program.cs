using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Domain.Services.API;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;

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
// vault
// keycloak
// authorization
// authentication

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// Services
builder.Services.AddScoped<IFileService, FileService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();


// IOptions


#region Middleware

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
