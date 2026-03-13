using Education.Patterns.Structural.Decorator.Interfaces;

namespace Education.Patterns.Structural.Decorator.Abstractions;

public abstract class CoffeeDecorator : ICoffee
{
    // Ссылка на объект, который декорируем
    protected readonly ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription()
    {
        return _coffee.GetDescription();
    }

    public virtual double GetCost()
    {
        return _coffee.GetCost();
    }
}
