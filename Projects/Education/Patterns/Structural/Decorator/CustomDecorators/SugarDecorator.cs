using Education.Patterns.Structural.Decorator.Abstractions;
using Education.Patterns.Structural.Decorator.Interfaces;

namespace Education.Patterns.Structural.Decorator.CustomDecorators;

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee)
     : base(coffee)
    {
    }

    public override string GetDescription() => _coffee.GetDescription() + ", сахар";

    public override double GetCost() => _coffee.GetCost() + 10.0;
}
