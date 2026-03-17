namespace Education.General.Types;

public class AbstractAndStaticClass_
{
    // Статический класс:
    // - нужен для группировки логически связанных членов
    // - от него нельзя наследоваться
    // - не может реализовывать интерфейс
    static class A
    {
        static public void F1()
        {
            Console.WriteLine("A3");
        }
    }

    // Абстрактный класс может иметь
    // - переменные
    // - абстрактные | методы
    // - абстрактные | свойства
    // - конструкторы
    // - индексаторы
    // - события

    // Абстрактные методы и св-ва:
    // - могут быть лишь в абстрактных классах
    // - не могут иметь тело
    // - должны быть переопределены наследником

    // Абстрактные члены не должны иметь модификатор private
    // Исключение - если дочерний класс тоже абстрактный

    abstract class B
    {
        public abstract string Name { get; set; }

        public void F2()
        {
            Console.WriteLine("B3");
        }

        public abstract void F3();
    }

    class ProgramAbstractAndStaticClass : B
    {
        string name = "";

        public override string Name
        {
            get
            {
                return "Mr/Ms. " + name;
            }

            set
            {
                name = value;
            }
        }

        static void Main_()
        {
            A.F1();

            B objB = new ProgramAbstractAndStaticClass();

            objB.F2();

            objB.F3();
        }

        public override void F3()
        {
            Console.WriteLine("Program");
        }
    }
}
