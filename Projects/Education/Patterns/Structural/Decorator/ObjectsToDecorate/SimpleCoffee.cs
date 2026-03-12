using Education.Patterns.Structural.Decorator.Interfaces;

namespace Education.Patterns.Structural.Decorator.ObjectsToDecorate;

public class SimpleCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Черный кофе";
    }

    public double GetCost()
    {
        // Базовая цена
        return 100.0;
    }
}
