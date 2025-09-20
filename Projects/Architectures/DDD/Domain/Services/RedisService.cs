using Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Domain.Services;

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
            string? cachedMember = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrEmpty(cachedMember))
            {
                // Если в кеше нашли значение - десереализуем его и возвращаем
                result = JsonConvert.DeserializeObject<string>(
                    cachedMember ?? "",
                    new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    });
            }
            else
            {
                // Если в кеше пусто - идем в БД
                //result = await _decorated.GetById(key, cancellationToken);

                //// Если нашли значение в БД - обновляем кеш
                //if (!string.IsNullOrEmpty(result))
                //{
                //    await PutCache(key, result, cancellationToken);
                //}
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

            await _distributedCache.SetStringAsync(key, jsonMember, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
