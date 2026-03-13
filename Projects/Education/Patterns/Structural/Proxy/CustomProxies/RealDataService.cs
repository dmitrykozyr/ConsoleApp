using Education.Patterns.Structural.Proxy.Interfaces;

namespace Education.Patterns.Structural.Proxy.CustomProxies;

public class RealDataService : IDataService
{
    public string GetData(int id)
    {
        Console.WriteLine($"[RealDataService] Загрузка данных для {id}");

        return $"Данные объекта {id}";
    }
}
