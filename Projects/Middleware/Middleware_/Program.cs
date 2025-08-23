/*
    Есть три способа настройки приложений:

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

var builder = WebApplication.CreateBuilder();

// Здесь добавляем сервисы в DI контейнер

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// MIDDLEWARE:

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // В случае ошибки выводим информацию о ней
    app.UseDeveloperExceptionPage();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Title");
    });
}

app.UseHttpsRedirection();

// Включаем маршрутизацию, чтобы приложение соотносило запросы с маршрутами
app.UseRouting();

app.UseAuthorization();

// Устанавливаем адреса, которые будут обрабатываться
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    // Для запросов по маршруту http://localhost:XXXXX/ будет выводиться текст
    endpoints.MapGet("/", async context => await context.Response.WriteAsync($"Hello"));
});

app.MapControllers();

app.Run();
