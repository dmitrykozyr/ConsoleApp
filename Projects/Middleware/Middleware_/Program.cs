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
