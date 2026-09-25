namespace Domain.Models.Options;

public class DatabaseOptions
{
    public string? ConnectionString { get; init; }

    public string? DbPassword { get; init; }

    public string? ConnStrBuffer { get; init; }

    public string? DbPasswordBuffer { get; init; }

    public int SqlCommandTimeout { get; init; }
}
