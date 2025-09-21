namespace Domain.Interfaces.Cache;

public interface IMemoryCacheService
{
    string? GetCache(string key);

    bool PutCache(string key, string value);
}
