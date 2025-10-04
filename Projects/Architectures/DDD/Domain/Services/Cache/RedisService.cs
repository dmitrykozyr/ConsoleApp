using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Domain.Services.Cache;

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

            /*
            string cacheKey = $"Cache_{key}";

            // Пытаемся получить данные из кеша
            var cachedItem = await _distributedCache.GetStringAsync(cacheKey);
            if (cachedItem != null)
            {
                var result = JsonConvert.DeserializeObject<Item>(cachedItem);

                return result?.Name;
            }

            // Если данных в кеше нет - берем их из БД, которую симулируем
            var item = await FetchItemFromDatabase(int.Parse(key));

            // Кладем данные в кеш
            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonConvert.SerializeObject(item),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
            );

            return item.ToString();
            */
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

    /*
    public async Task<T?> GetUser(int id)
    {
        T? user;
        // пытаемся получить данные из кэша по id
        var userString = await _distributedCache.GetStringAsync(id.ToString());

        //десериализируем из строки в объект User
        if (userString != null)
        {
            user = JsonSerializer.Deserialize<T>(userString);
        }

        // если данные не найдены в кэше
        if (user == null)
        {
            // обращаемся к базе данных
            user = await db.Users.FindAsync(id);

            // если пользователь найден, то добавляем в кэш
            if (user != null)
            {
                Console.WriteLine($"{user.Name} извлечен из базы данных");

                // сериализуем данные в строку в формате json
                userString = JsonSerializer.Serialize(user);

                // сохраняем строковое представление объекта в формате json в кэш на 2 минуты

                var tmp = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                };

                await _distributedCache.SetStringAsync(user.Id.ToString(), userString, tmp);
            }
        }
        else
        {
            Console.WriteLine($"{user.Name} извлечен из кэша");
        }

        //return user;

        return default;
    }
    */
}
