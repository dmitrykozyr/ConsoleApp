using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Services.Cache;

/*
    Подключение к Redis CLI через Docker
    (your_redis_container_name - имя или ID контейнера):
  
       docker exec -it your_redis_container_name redis-cli
 
    Если контейнер называется redis, команда будет выглядеть так:

       docker exec -it redis redis-cli

    Для подключения к Redis на определенном порту или с использованием определенного хоста,
    можно указать их в команде

       docker exec -it redis redis-cli -h <host> -p <port>

    Проверка работы Redis, в ответ должно прийти PONG:

	    ping

    Выйти из докера в консоли
	
	    quit
*/

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
