using Microsoft.Extensions.Options;
using Vault_.HttpClient_;
using Vault_.Logging_;
using Vault_.Models;
using Vault_.Options;

namespace Vault_.VaultSetup;

public class VaultConfigurationSource : IConfigurationSource
{
    private readonly ILogging _logging;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientData<VaultSecrets> _httpClientData;
    private readonly IOptions<VaultOptions> VaultOptions;

    public VaultConfigurationSource(
        IOptions<VaultOptions> vaultOptions,
        IConfiguration configuration,
        ILogging logging,
        IHttpClientData<VaultSecrets> httpClientData)
    {
        _logging = logging;
        _configuration = configuration;
        _httpClientData = httpClientData;
        VaultOptions = vaultOptions;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider(VaultOptions, _configuration, _logging, _httpClientData);
    }
}
