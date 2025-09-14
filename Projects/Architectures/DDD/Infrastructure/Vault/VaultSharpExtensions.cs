using Domain.Interfaces;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Vault;

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
