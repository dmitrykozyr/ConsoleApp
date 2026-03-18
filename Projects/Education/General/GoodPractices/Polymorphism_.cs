namespace Education.General.GoodPractices;

public class Polymorphism_
{
    class BaseClass
    {
        public virtual void F1() => Console.WriteLine("Вызов базового класса");
    }

    class OverrideClass : BaseClass
    {
        public override void F1() => Console.WriteLine("Вызов override класса");
    }

    class NewClass : BaseClass
    {
        public new void F1() => Console.WriteLine("Вызов new класса");
    }

    public void Main_()
    {
        BaseClass overrideToBase = new OverrideClass();
        BaseClass newToBase = new NewClass();

        // Выведет: Вызов override класса
        overrideToBase.F1();

        // Выведет: Вызов базового класса
        newToBase.F1();
    }
}
