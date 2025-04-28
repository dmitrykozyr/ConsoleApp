using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMessageSender, EmailMessageSender>();
builder.Services.AddTransient<SmsMessageSender>();

builder.Services.AddDbContext<DbContextSetUp>(options => options.UseInMemoryDatabase("DatabaseName"));
builder.Services.AddTransient<DbContextSetUp>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Amazing Swagger", Version = "v1" }));


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
 