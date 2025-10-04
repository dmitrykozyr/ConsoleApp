namespace Presentation.Extensions;

public static class RedisExtensions
{
    public static void AddRedisExtensions(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            string? connection = builder.Configuration.GetConnectionString("Redis");

            options.Configuration = connection;
        });
    }
}
