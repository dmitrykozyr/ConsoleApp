using Education.Patterns.Behavioral.Visitor.Animals;
using Education.Patterns.Behavioral.Visitor.Interfaces;
using Education.Patterns.Behavioral.Visitor.Visitors;

namespace Education.Patterns.Behavioral.Visitor;

public class VisitorPattern
{
    // Паттерн позволяет добавить новую операцию группе существующих классов, не меняя их код
    // В зоопарке есть Лев и Обезьяна
    // Мы хотим добавить им действия «Издать звук» и «Покормить»
    // Чтобы не писать эти методы в каждом классе животного, создадим «Посетителя»
    public void Start()
    {
        var animals = new List<IAnimal>
        {
            new Lion(),
            new Monkey()
        };

        var soundVisitor = new SoundVisitor();
        var feedVisitor = new FeedVisitor();

        foreach (var animal in animals)
        {
            animal.Accept(soundVisitor);
        }

        foreach (var animal in animals)
        {
            animal.Accept(feedVisitor);
        }
    }
}
