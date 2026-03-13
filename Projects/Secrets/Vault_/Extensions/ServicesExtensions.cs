using Vault_.HttpClient_;
using Vault_.Logging_;

namespace Vault_.Extensions;

public static class ServicesExtensions
{
    public static void AddServicesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ILogging, Logging>();
        serviceCollection.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));
    }
}
