using Education.Patterns.Creational.Prototype.ObjectsToClone;

namespace Education.Patterns.Structural.Composite;

// Есть коробка, в которую кладем игрушки и еще одну коробку с игрушкой
// Нужно посчитать стоимость всех игрушек, обойдя эту древовидную структуру
public class CompositePattern
{
    public void Start()
    {
        var bigBox = new Box("Большая коробка", 0);
        var smallBox = new Box("Маленькая коробка", 0);

        var gift_1 = new ConcreteGift("PS5", 100);
        var gift_2 = new ConcreteGift("Телефон", 10);
        var gift_3 = new ConcreteGift("Ноутбук", 1000);

        // Кладем в большую коробку игрушки
        bigBox.AddToBox(gift_1);
        bigBox.AddToBox(gift_2);

        // Кладем в маленькую коробку игрушку
        smallBox.AddToBox(gift_3);

        // Кладем в большую коробку маленькую коробку
        // Метод принимает тип Gift, а я передаю туда ConcreteGift
        // Это работает благодаря полиморфизму, потому что ConcreteGift наследуется от Gift
        bigBox.AddToBox(smallBox);

        // Считаем стоимость всех игрушек в большой коробке
        Console.WriteLine($"Стоимость всех игрушек в большой коробке: {bigBox.TotalPrice()}");
    }
}
