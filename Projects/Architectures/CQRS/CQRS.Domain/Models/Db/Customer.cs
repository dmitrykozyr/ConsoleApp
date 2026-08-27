namespace CQRS.Domain.Models.Db;

public class Customer
{
    public long Id { get; init; }

    public string Name { get; init; } = null!;

    public string Address { get; private set; } = null!;

    private Customer()
    {
    }

    public static Customer Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be empty", nameof(address));
        }

        var result = new Customer
        {
            Id = 0,
            Name = name,
            Address = address
        };

        return result;
    }

    public void ChangeAddress(string newAddress)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
        {
            throw new ArgumentException("Address cannot be empty", nameof(newAddress));
        }

        Address = newAddress;
    }
}
