namespace Education.Patterns.Behavioral.TemplateMethod;

public abstract class CaffeineBeverage
{
    public void PrepareRecipe()
    {
        // Задаем неизменяемый скелет алгоритма, который гарантирует,
        // что мы не нальем воду в чашку раньше, чем вскипятим её
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }

    // Общие шаги для всех напитков
    private void BoilWater() => Console.WriteLine("Кипячение воды...");
    private void PourInCup() => Console.WriteLine("Наливание в чашку...");

    // Шаги, которые каждый напиток делает по-своему
    protected abstract void Brew();
    protected abstract void AddCondiments();
}
