using Education.Patterns.Behavioral.Strategy.CustomStrategies;

namespace Education.Patterns.Behavioral.Strategy;

// Говорим "Хочу права, денег мало" - получим права через месяц
// Говорим "Хочу права, денег много" - получим права завтра
// Что делал человек - мы не знаем, но задаем начальные условия, а он решает, как себя вести
public class StrategyPattern
{
    public void Start()
    {
        var context = new Context();

        var slowStrategy = new SlowLicense();
        context.SetStrategy(slowStrategy);
        context.StartBehavior(100);

        var fastStrategy = new FastLicence();
        context.SetStrategy(fastStrategy);
        context.StartBehavior(5000);
    }
}
