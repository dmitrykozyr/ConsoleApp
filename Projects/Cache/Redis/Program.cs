using Redis.Interfaces;
using Redis.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    string? connection = builder.Configuration.GetConnectionString("Redis");

    options.Configuration = connection;
});

builder.Services.AddScoped<IRedisService, RedisService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/GetCache", async (IRedisService redisService) =>
{
    try
    {
        var result = await redisService.GetCache("key 1");

        return Results.Ok(result);

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
}).WithOpenApi();

app.MapGet("/PutCache", async (IRedisService redisService) =>
{
    try
    {
        await redisService.PutCache("key 1", "222");

        return Results.Ok();

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
}).WithOpenApi();

app.Run();
