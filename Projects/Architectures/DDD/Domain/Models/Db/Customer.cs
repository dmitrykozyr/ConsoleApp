namespace Domain.Models.Db;

// Сущности Domain должны соответствовать принципам DDD,
// а не быть простыми POCO классами без бизнес-логики
// Должны быть:
// - инкапсуляция бизнес-логики
// - валидация на уровне домена
// - методы для работы с сущностью
// - невозможность создать невалидную сущность

public class Customer
{
    public long Id { get; init; }

    public string? Name { get; init; }

    public string? Address { get; private set; }


    // Для EF Core
    private Customer()
    {
    }

    public static Customer Create(string? name, string? address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Name cannot be empty");
        }
        
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new Exception("Address cannot be empty");
        }

        return new Customer
        {
            Id      = 0, // Будет установлен при сохранении
            Name    = name,
            Address = address
        };
    }

    // Бизнес-логика
    public void ChangeAddress(string newAddress)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
        {
            throw new Exception("Address cannot be empty");
        }

        Address = newAddress;
    }
}
