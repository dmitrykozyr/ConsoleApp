using Education.Patterns.Structural.Decorator.Abstractions;
using Education.Patterns.Structural.Decorator.Interfaces;

namespace Education.Patterns.Structural.Decorator.CustomDecorators;

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee)
     : base(coffee)
    {
    }

    public override string GetDescription() => _coffee.GetDescription() + ", молоко";

    public override double GetCost() => _coffee.GetCost() + 30.0;
}
