using Education.Patterns.Behavioral.Observer.Interfaces;

namespace Education.Patterns.Behavioral.Observer.CustomClients;

public class SMSClient : ISubscriber
{
    public void Update(string item)
    {
        Console.WriteLine($"SMS: Отправлено сообщение об товаре {item}");
    }
}
