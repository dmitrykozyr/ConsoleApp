namespace Domain.Interfaces;

public interface IRedisService
{
    Task<string?> GetCache(string key, CancellationToken cancellationToken = default);

    Task PutCache(string key, string value, CancellationToken cancellationToken = default);
}
