using Education.Patterns.Behavioral.TemplateMethod.Abstractions;

namespace Education.Patterns.Behavioral.TemplateMethod.Drinks;

public class Coffee : CaffeineBeverage
{
    protected override void Brew()
    {
        Console.WriteLine("Пропускание воды через молотый кофе");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление сахара и молока");
    }
}
