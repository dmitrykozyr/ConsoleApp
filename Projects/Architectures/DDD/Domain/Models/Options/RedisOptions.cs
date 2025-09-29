namespace Domain.Models.Options;

public class RedisOptions
{
    public string? ConnectionString { get; init; }

    public string? Channel { get; init; }
}
