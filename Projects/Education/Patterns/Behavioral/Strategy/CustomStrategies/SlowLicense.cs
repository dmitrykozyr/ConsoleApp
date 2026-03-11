using Education.Patterns.Behavioral.Strategy.Interfaces;

namespace Education.Patterns.Behavioral.Strategy.CustomStrategies;

public class SlowLicense : IStrategy
{
    public void Start(int moneyAmount)
    {
        Console.WriteLine($"Денег мало ({moneyAmount}) - сделай права медленно");
    }
}
