namespace Domain.Models.Db;

public class Order
{
    public long Id { get; private set; }

    public string? Name { get; private set; }

    public DateTime OrderTime { get; private set; }


    // Для EF Core
    private Order()
    {
    }

    public static Order Create(string name, DateTime orderTime)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Name cannot be empty");
        }

        if (orderTime == DateTime.MinValue)
        {
            throw new Exception("Address cannot be empty");
        }

        return new Order
        {
            Id = 0, // Будет установлен при сохранении
            Name = name,
            OrderTime = orderTime
        };
    }

    // Бизнес-логика
    public void ChangeAddress(DateTime newOrderTime)
    {
        if (newOrderTime == DateTime.MinValue)
        {
            throw new Exception("Address cannot be empty");
        }

        OrderTime = newOrderTime;
    }
}
