using HealthChecks_.Interfaces;

namespace HealthChecks_.ServicesToCheck;

public class DatabaseService : IHealthCheckable
{
    public string ServiceName => nameof(DatabaseService);

    public bool CheckHealth()
    {
        // Логика проверки подключения к БД
        return true;
    }
}
