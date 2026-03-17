namespace Education.General;

public class MultipleInheritance_
{
    // Множественное наследование запрещено, но его можно реализовать через интерфейсы
    interface IInterface
    {
        void F2();
    }

    class A
    {
        public void F1()
        {
            Console.WriteLine("F1");
        }
    }

    class B : IInterface
    {
        public void F2()
        {
            Console.WriteLine("F2");
        }
    }

    class C : A, IInterface
    {
        B objB = new B();

        public void F2()
        {
            objB.F2();
        }
    }

    static void Main_()
    {
        var objC = new C();

        objC.F1();
        objC.F2();
    }
}
