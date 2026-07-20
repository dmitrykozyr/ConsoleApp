using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis.Interfaces;

namespace Redis.Services;

// Создание контейнера на нужном порту
// docker run -d --name redis -p 6379:6379 redis:latest

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

            //! Если в кеше есть данные и их срок жизни еще не истек (проверят по значению из конфига)
            if (!string.IsNullOrEmpty(cachedMember))
            {
                result = JsonConvert.DeserializeObject<string>(cachedMember);
            }
            else
            {
                // Если данных в кеше нет - берем их из БД, которую симулируем
                var dataFromDb = "111";

                // Кладем данные в кеш, если ответ из БД не пустой
                if (!string.IsNullOrEmpty(dataFromDb))
                {
                    await _distributedCache.SetStringAsync(keyCache, JsonConvert.SerializeObject(dataFromDb), cancellationToken);
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
            //! Записывать в БД временную метку записи данных в кеш

            string jsonMember = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, value, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
