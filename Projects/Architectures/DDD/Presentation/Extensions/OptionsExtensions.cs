using Domain.Models.Options;

namespace Presentation.Extensions;

public static class OptionsExtensions
{
    public static void AddOptionsExtensions(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        // IOptions             Обновляет информацию о конфигурации один раз при старте приложения
        // IOptionsSnapshot     Обновляет информацию о конфигурации при каждом запросе и не изменяет ее во время запроса
        // IOptionsMonitor      Обновляет информацию о конфигурации при каждом обращении к конфигурации
        builder.Services.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
        builder.Services.ConfigureOptions<ApplicationOptionsSetup<GeneralOptions>>();
        builder.Services.ConfigureOptions<ApplicationOptionsSetup<LoginOptions>>();
        builder.Services.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();
        builder.Services.ConfigureOptions<ApplicationOptionsSetup<RedisOptions>>();
    }
}
