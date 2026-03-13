using Education.Patterns.Structural.Proxy.CustomProxies;
using Education.Patterns.Structural.Proxy.Interfaces;

namespace Education.Patterns.Structural.Proxy;

public class ProxyPattern
{
    public void Start()
    {
        // Клиент думает, что работает с обычным сервисом
        IDataService service = new ProxyDataService();

        Console.WriteLine("Первый запрос - создаст объект и пойдет в БД");
        service.GetData(1);

        Console.WriteLine();

        Console.WriteLine("Второй запрос того же Id - возьмет из кэша");
        service.GetData(1);

        Console.WriteLine();

        Console.WriteLine("Третий запрос нового Id - пойдет в БД");
        service.GetData(2);
    }
}
