using CommunityToolkit.Diagnostics;
using Domain.Models.Options;

namespace Presentation.Extensions;

public static class RedisExtensions
{
    public static void AddRedisExtensions(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        var redisOptions = builder.Configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();

        Guard.IsNotNull(redisOptions);

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisOptions.ConnectionString;
        });
    }
}
