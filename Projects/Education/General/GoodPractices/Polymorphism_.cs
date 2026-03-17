namespace Education.General.GoodPractices;

public class Polymorphism_
{
    class BaseClass
    {
        // virtual
        public virtual void F1()
        {
            Console.WriteLine("Вызов базового класса");
        }
    }

    class DerivedClass_Override : BaseClass
    {
        // override
        public override void F1()
        {
            Console.WriteLine("Вызов дочернего класса");
        }
    }

    class DerivedClass_New : BaseClass
    {
        // new
        public new void F1()
        {
            Console.WriteLine("Вызов дочернего класса");
        }
    }

    static void Main_()
    {
        BaseClass obj1 = new BaseClass();
        obj1.F1();  // Вызов базового класса



        DerivedClass_Override obj2 = new DerivedClass_Override();
        obj2.F1();  // Вызов дочернего класса

        BaseClass obj3 = new DerivedClass_Override();
        obj3.F1();  // Вызов дочернего класса



        DerivedClass_New obj4 = new DerivedClass_New();
        obj4.F1();  // Вызов дочернего класса

        BaseClass obj5 = new DerivedClass_New();
        obj5.F1();  // Вызов базового класса
    }
}
