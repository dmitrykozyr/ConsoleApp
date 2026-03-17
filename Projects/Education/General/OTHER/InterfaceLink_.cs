namespace Education.General;

public class InterfaceLink_
{
    // Интерфейсы могут хранить свойства и методы
    interface IMyInterface
    {
        void F1();
    }

    class A : IMyInterface
    {
        public void F1()
        {
            Console.WriteLine("F1");
        }

        public void F2()
        {
            Console.WriteLine("F2");
        }
    }

    static void Main_()
    {
        var objA = new A();

        IMyInterface inter = objA;

        inter.F1();

        //inter.F2(); // Ошибка
    }
}
