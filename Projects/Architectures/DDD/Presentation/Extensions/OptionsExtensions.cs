using Domain.Models.Options;

namespace Presentation.Extensions;

public static class OptionsExtensions
{
    public static void AddOptionsExtensions(this IServiceCollection serviceCollection)
    {
        // IOptions             Обновляет информацию о конфигурации один раз при старте приложения
        // IOptionsSnapshot     Обновляет информацию о конфигурации при каждом запросе и не изменяет ее во время запроса
        // IOptionsMonitor      Обновляет информацию о конфигурации при каждом обращении к конфигурации
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<GeneralOptions>>();
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<LoginOptions>>();
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<RedisOptions>>();
    }
}
