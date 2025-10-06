using Microsoft.FeatureManagement;
using Presentation.Extensions;

// “ри способа настройки приложений
/*
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

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

// ћетоды расширени€
builder.Services.AddQuartzExtension(builder);
builder.Services.AddRedisExtensions(builder);
builder.Services.AddOptionsExtensions(builder);
builder.Services.AddServicesExtensions(builder);
builder.Services.AddRepositoriesExtensions(builder);
IConfiguration configuration = builder.Services.AddConfigurationExtension(builder);


WebApplication app = builder.Build();

// —ервисы, которым нужен WebApplication
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
