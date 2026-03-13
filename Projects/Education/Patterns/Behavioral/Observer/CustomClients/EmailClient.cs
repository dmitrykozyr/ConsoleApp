using Education.Patterns.Behavioral.Observer.Interfaces;

namespace Education.Patterns.Behavioral.Observer.CustomClients;

public class EmailClient : ISubscriber
{
    public void Update(string item)
    {
        Console.WriteLine($"Email: Отправлено письмо об товаре {item}");
    }
}
