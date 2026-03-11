
using Education.Patterns.Behavioral.Strategy.Interfaces;

namespace Education.Patterns.Behavioral.Strategy.CustomStrategies;

public class FastLicence : IStrategy
{
    public void Start(int moneyAmount)
    {
        Console.WriteLine($"Денег много ({moneyAmount}) - сделай права быстро");
    }
}
