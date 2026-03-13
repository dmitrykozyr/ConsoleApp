using Education.Patterns.Structural.Composite.Abstractions;

namespace Education.Patterns.Structural.Composite;

public class ConcreteGift : Gift
{
    public ConcreteGift(string name, int price)
     : base(name, price)
    {
    }

    public override int TotalPrice()
    {
        Console.WriteLine($"{Name}, стоимость {Price}");

        return Price;
    }
}
