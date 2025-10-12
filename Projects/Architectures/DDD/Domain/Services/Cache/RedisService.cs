using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Domain.Services.Cache;

//! Redis не работает
public class RedisService : IRedisService
{
    private readonly IDistributedCache _distributedCache;

    public RedisService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<string?> GetCache(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            string? result = "";

            string keyCache = $"cacheMember-{key}";

            // Пытаемся получить данные из кеша
            string? cachedMember = await _distributedCache.GetStringAsync(keyCache, cancellationToken);
            if (!string.IsNullOrEmpty(cachedMember))
            {
                result = JsonConvert.DeserializeObject<string>(cachedMember);
            }
            else
            {
                // Если данных в кеше нет - берем их из БД, которую симулируем
                var dataFromDb = "data from DB";

                // Кладем данные в кеш
                //! Кладем, только если ответ из БД не пустой

                if (!string.IsNullOrEmpty(dataFromDb))
                {
                    string value = "Hello, Redis!";

                    await _distributedCache.SetStringAsync(keyCache, JsonConvert.SerializeObject(value), cancellationToken);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task PutCache(string key, string value, CancellationToken cancellationToken = default)
    {
        try
        {
            string jsonMember = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, value, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
