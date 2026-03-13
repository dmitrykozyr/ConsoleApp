using Microsoft.Extensions.Options;
using Vault_.HttpClient_;
using Vault_.Logging_;
using Vault_.Models;
using Vault_.Options;

namespace Vault_.VaultSetup;

public static class VaultSharpExtensions
{
    public static IConfigurationBuilder AddVault(this IConfigurationBuilder configurationBuilder,
        IOptions<VaultOptions> vaultOptions,
        ILogging logging,
        IConfiguration configuration,
        IHttpClientData<VaultSecrets> httpClientData)
    {
        var vaultConfigSource = new VaultConfigurationSource(vaultOptions, configuration, logging, httpClientData);

        configurationBuilder.Add(vaultConfigSource);

        return configurationBuilder;
    }
}
