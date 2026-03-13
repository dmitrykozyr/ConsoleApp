namespace Education.Patterns.Structural.Composite.Abstractions;

public abstract class Gift
{
    public string Name { get; }

    public int Price { get; init; }

    protected Gift(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public abstract int TotalPrice();
}
