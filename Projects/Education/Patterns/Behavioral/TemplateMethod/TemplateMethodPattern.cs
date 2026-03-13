using Education.Patterns.Behavioral.TemplateMethod.Abstractions;
using Education.Patterns.Behavioral.TemplateMethod.Drinks;

namespace Education.Patterns.Behavioral.TemplateMethod;

public class TemplateMethodPattern
{
    public void Start()
    {
        Console.WriteLine("--- Готовим Чай ---");
        CaffeineBeverage tea = new Tea();
        tea.PrepareRecipe();

        Console.WriteLine("\n--- Готовим Кофе ---");
        CaffeineBeverage coffee = new Coffee();
        coffee.PrepareRecipe();
    }
}
