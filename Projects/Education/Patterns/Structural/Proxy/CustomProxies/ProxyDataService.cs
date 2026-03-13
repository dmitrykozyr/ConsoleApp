using Education.Patterns.Structural.Proxy.Interfaces;

namespace Education.Patterns.Structural.Proxy.CustomProxies;

public class ProxyDataService : IDataService
{
    private RealDataService? _realService;

    private readonly Dictionary<int, string> _cache = new();

    public string GetData(int id)
    {
        Console.WriteLine($"[Proxy] Запрос данных для {id}");

        if (_cache.TryGetValue(id, out var cachedData))
        {
            Console.WriteLine($"[Proxy] Возврат данных из кэша для {id}");

            return cachedData;
        }
        else
        {
            // Ленивая инициализация: создаем сервис только при первом обращении
            _realService ??= new RealDataService();

            // Делегирование работы реальному объекту
            var result = _realService.GetData(id);

            _cache[id] = result;

            return result;
        }
    }
}
