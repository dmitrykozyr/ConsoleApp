using HealthChecks_.Interfaces;

namespace HealthChecks_.Services;

public class HealthService : IHealthService
{
    private readonly IEnumerable<IHealthCheckable> _services;

    public HealthService(IEnumerable<IHealthCheckable> services)
    {
        _services = services;
    }

    public bool CheckAllServices()
    {
        bool isHealthy = true;

        // Обходим все сервисы, реализующие IHealthCheckable
        foreach (var service in _services)
        {
            isHealthy = service.CheckHealth();

            Console.WriteLine($"Сервис {service.ServiceName} {(isHealthy ? "работает" : "не работает")}");
        }

        return isHealthy;
    }
}
