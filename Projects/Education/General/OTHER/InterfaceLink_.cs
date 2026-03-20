namespace Education.General;

public class InterfaceLink_
{
    // Интерфейсы могут хранить:
    // - свойства
    // - методы с определением
    // - static поля
    // - static abstract методы
    public interface IMyInterface
    {
        void F1();

        void F3() => Console.WriteLine("Default F3");
    }

    public class ClassA : IMyInterface
    {
        public void F1() => Console.WriteLine("F1");

        public void F2() => Console.WriteLine("F2");
    }

    public static void Main_()
    {
        var classA = new ClassA();

        IMyInterface interfaceLink = classA;

        interfaceLink.F1();
        //interfaceLink.F2(); // ошибка, в IMyInterface нет такого метода
        interfaceLink.F3();

        // Для вызова F2, имея только ссылку interfaceLink,
        // нужно проверить тип и привести его обратно
        if (interfaceLink is ClassA concreteA)
        {
            concreteA.F2();
        }
    }
}
