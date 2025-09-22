using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Domain.Services.Cache;

public class RedisService<T> : IRedisService
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

            return cachedMember;

            //if (!string.IsNullOrEmpty(cachedMember))
            //{
            //    // Если в кеше нашли значение - десереализуем его и возвращаем
            //    result = JsonConvert.DeserializeObject<string>(
            //        cachedMember ?? "",
            //        new JsonSerializerSettings
            //        {
            //            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            //        });
            //}
            //else
            //{
            //    // Если в кеше пусто - идем в БД
            //    //!result = await _decorated.GetById(key, cancellationToken);

            //    // Если нашли значение в БД - обновляем кеш
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        await PutCache(key, result, cancellationToken);
            //    }
            //}

            return null;
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
            //string jsonMember = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, value, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<T?> GetUser(int id)
    {
        //T? user;
        //// пытаемся получить данные из кэша по id
        //var userString = await _distributedCache.GetStringAsync(id.ToString());

        ////десериализируем из строки в объект User
        //if (userString != null)
        //{
        //    user = JsonSerializer.Deserialize<T>(userString);
        //}

        //// если данные не найдены в кэше
        //if (user == null)
        //{
        //    // обращаемся к базе данных
        //    user = await db.Users.FindAsync(id);

        //    // если пользователь найден, то добавляем в кэш
        //    if (user != null)
        //    {
        //        Console.WriteLine($"{user.Name} извлечен из базы данных");

        //        // сериализуем данные в строку в формате json
        //        userString = JsonSerializer.Serialize(user);

        //        // сохраняем строковое представление объекта в формате json в кэш на 2 минуты

        //        var tmp = new DistributedCacheEntryOptions
        //        {
        //            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        //        };

        //        await _distributedCache.SetStringAsync(user.Id.ToString(), userString, tmp);
        //    }
        //}
        //else
        //{
        //    Console.WriteLine($"{user.Name} извлечен из кэша");
        //}

        //return user;

        return default;
    }
}
