using Timer.Infrastructure.Options;

namespace Timer.Presentation.Extensions;

public static class OptionsExtensions
{
    public static void AddOptionsExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
    }
}
