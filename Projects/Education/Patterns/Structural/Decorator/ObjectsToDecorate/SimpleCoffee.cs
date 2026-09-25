using Education.Patterns.Structural.Decorator.Interfaces;

namespace Education.Patterns.Structural.Decorator.ObjectsToDecorate;

public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Черный кофе";

    public double GetCost() => 100.0;
}
