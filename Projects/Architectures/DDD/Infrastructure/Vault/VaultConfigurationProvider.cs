using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Infrastructure.Vault;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly IVaultClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogging _logging;
    private readonly IHttpClientData<VaultSecrets> _httpClientData;

    private readonly VaultOptions? VaultOptions;

    public VaultConfigurationProvider(
        IOptions<VaultOptions> vaultOptions,
        IConfiguration configuration,
        ILogging logging,
        IHttpClientData<VaultSecrets> httpClientData)
    {
        VaultOptions = vaultOptions.Value;

        var rootToken = new TokenAuthMethodInfo(VaultOptions.Secret);
        var vaultClientSettings = new VaultClientSettings(VaultOptions.Address, rootToken);
        _client = new VaultClient(vaultClientSettings);

        _configuration = configuration;
        _logging = logging;
        _httpClientData = httpClientData;
    }

    public override void Load()
    {
        LoadAsync().Wait();
    }

    public async Task LoadAsync()
    {
        // Получение секретов из Vault, запущенного в режиме Dev (сервер запущен командой "vault server -dev")
        //await GetSecretsFromVaultInDevMode();

        // Получение секретов из Vault, запущенного в режиме Prod
        await GetSecretsFromVaultInProdMode();
    }

    public async Task GetSecretsFromVaultInDevMode()
    {
        string secretsEngineName    = _configuration.GetSection("Vault:SecretsEngineName").Value ?? "";
        string secretPath           = _configuration.GetSection("Vault:SecretsStorageName").Value ?? "";

        try
        {
            var kvSecret = await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath, null, secretsEngineName);

            if (kvSecret is not null && kvSecret.Data.Data.Any())
            {
                foreach (var data in kvSecret.Data.Data)
                {
                    // Секреты из Vault кладутся в IConfiguration
                    Data.Add(data.Key, data.Value.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _logging.LogToFile($"Ошибка получения секретов из DEV-сервера Vault, {ex.Message}");
        }
    }

    public async Task GetSecretsFromVaultInProdMode()
    {
        try
        {
            Guard.IsNotNull(VaultOptions);

            var secretNames = typeof(SecretNames).GetProperties().ToList();

            foreach (var secretName in secretNames)
            {
                var request = $"{VaultOptions.Address}/v1/secret/{secretName.Name}";

                VaultSecrets? kvSecret = await _httpClientData.GetRequest(request);

                Guard.IsNotNull(kvSecret);
                Guard.IsNotNull(kvSecret.data);

                KeyValuePair<string, string> secret = kvSecret.data.FirstOrDefault();

                // Секреты из Vault кладутся в IConfiguration
                Data.Add(secret.Key, secret.Value.ToString());
            }
        }
        catch (Exception ex)
        {
            _logging.LogToFile($"Ошибка получения секретов из PROD-сервера Vault, {ex.Message}");
        }
    }
}
