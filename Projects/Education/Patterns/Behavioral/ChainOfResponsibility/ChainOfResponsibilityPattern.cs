using Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

namespace Education.Patterns.Behavioral.ChainOfResponsibility;

public class ChainOfResponsibilityPattern
{
    public void Start()
    {
        var dog = new DogHandler();
        var monkey = new MonkeyHandler();
        var squirrel = new SquirrelHandler();

        // Создаем цепочку
        dog.SetNext(monkey).SetNext(squirrel);

        // Обходим ее, начиная с собаки
        // Необязательно начинать с нее, можно начать с любого элемента в цепочке
        Client.ClientCode(dog);
    }
}
