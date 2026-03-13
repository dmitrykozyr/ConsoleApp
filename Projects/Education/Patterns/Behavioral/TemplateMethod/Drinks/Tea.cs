using Education.Patterns.Behavioral.TemplateMethod.Abstractions;

namespace Education.Patterns.Behavioral.TemplateMethod.Drinks;

public class Tea : CaffeineBeverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заваривание чайного пакетика");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление лимона");
    }
}
