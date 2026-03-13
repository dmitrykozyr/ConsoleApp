using Education.Patterns.Behavioral.Observer.Interfaces;

namespace Education.Patterns.Behavioral.Observer;

public class OnlineStore : IStore
{
    private readonly List<ISubscriber> _customers = new();

    public void Suscribe(ISubscriber customer)
    {
        _customers.Add(customer);
    }

    public void Unsubscribe(ISubscriber customer)
    {
        _customers.Remove(customer);
    }

    public void Notify(string productName)
    {
        Console.WriteLine($"Рассылка: {productName} в наличии");

        // Итерируемся по копии списка, чтобы избежать ошибок при отписке в процессе
        foreach (var customer in _customers.ToArray())
        {
            customer.Update(productName);
        }
    }

    public void Restock(string item)
    {
        Notify(item);
    }
}
