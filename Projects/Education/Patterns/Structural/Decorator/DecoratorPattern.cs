using Education.Patterns.Structural.Decorator.CustomDecorators;
using Education.Patterns.Structural.Decorator.Interfaces;
using Education.Patterns.Structural.Decorator.ObjectsToDecorate;

namespace Education.Patterns.Structural.Decorator;

public class DecoratorPattern
{
    public void Start()
    {
        ICoffee coffee;

        coffee = new SimpleCoffee();
        coffee = new MilkDecorator(coffee);
        coffee = new SugarDecorator(coffee);

        Console.WriteLine($"{coffee.GetDescription()} | Цена: {coffee.GetCost()}");
    }
}
