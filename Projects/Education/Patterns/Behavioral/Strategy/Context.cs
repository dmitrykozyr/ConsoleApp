using Education.Patterns.Behavioral.Strategy.Interfaces;

namespace Education.Patterns.Behavioral.Strategy;

public class Context
{
    IStrategy? _strategy;

    public void SetStrategy(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public void StartBehavior(int moneyAmount)
    {
        _strategy?.Start(moneyAmount);
    }
}
