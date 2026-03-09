namespace Timer.Infrastructure.Options;

public class DatabaseOptions
{
    public string? ConnectionString { get; init; }

    public string? SqlCommandTimeout { get; init; }
}
