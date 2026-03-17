using HealthChecks_.Interfaces;

namespace HealthChecks_.ServicesToCheck;

public class CacheService : IHealthCheckable
{
    public string ServiceName => nameof(CacheService);

    public bool CheckHealth()
    {
        return true;
    }
}
