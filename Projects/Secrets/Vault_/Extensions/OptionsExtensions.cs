using Vault_.Options;
using Vault_.Options.Setup;

namespace Vault_.Extensions;

public static class OptionsExtensions
{
    public static void AddOptionsExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();
    }
}
