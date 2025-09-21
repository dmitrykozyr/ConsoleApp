using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Services.Cache;
public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetCache(string key)
    {
        try
        {
            _memoryCache.TryGetValue(key, out string? cacheData);

            return cacheData;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool PutCache(string key, string value)
    {
        try
        {
            _memoryCache.Set(key, value);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
