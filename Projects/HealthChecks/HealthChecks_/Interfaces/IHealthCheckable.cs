namespace HealthChecks_.Interfaces;

public interface IHealthCheckable
{
    string ServiceName { get; }

    bool CheckHealth();
}
