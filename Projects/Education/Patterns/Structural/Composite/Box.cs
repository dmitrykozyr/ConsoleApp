using Education.Patterns.Structural.Composite.Abstractions;
using Education.Patterns.Structural.Composite.Interfaces;

namespace Education.Patterns.Structural.Composite;

public class Box : Gift, IGiftOperations
{
    private List<Gift> _gifts = new List<Gift>();

    public Box(string name, int price)
     : base(name, price)
    {
    }

    public void AddToBox(Gift gift)
    {
        _gifts.Add(gift);
    }

    public override int TotalPrice()
    {
        int totalPrice = 0;

        Console.WriteLine($"{Name} содержит: ");

        foreach (Gift gift in _gifts)
        {
            // Если gift — игрушка, она просто вернет свою цену
            // Если gift — коробка, она внутри себя рекурсивно запустит цикл по своим подаркам
            totalPrice += gift.TotalPrice();
        }

        return totalPrice;
    }
}
