using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;
using Vault_.HttpClient_;
using Vault_.Logging_;
using Vault_.Models;
using Vault_.Options;
using Vault_.VaultSetup;

namespace Vault_.Extensions;

public static class VaultExtensions
{
    public static void AddVaultExtensions(this IServiceCollection serviceCollection, WebApplication app, WebApplicationBuilder builder, IConfiguration configuration)
    {
        using (var scope = app.Services.CreateScope())
        {
            var logging = scope.ServiceProvider.GetService<ILogging>();
            var httpClientData = scope.ServiceProvider.GetService<IHttpClientData<VaultSecrets>>();
            var vaultOptions = scope.ServiceProvider.GetService<IOptions<VaultOptions>>();

            Guard.IsNotNull(logging);
            Guard.IsNotNull(httpClientData);
            Guard.IsNotNull(vaultOptions);

            builder.Configuration.AddVault(vaultOptions, logging, configuration, httpClientData);
        }
    }
}
