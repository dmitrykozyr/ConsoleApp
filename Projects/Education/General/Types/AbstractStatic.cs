namespace Education.General.Types;

public class AbstractStatic_
{
    /*
        Статический класс:
        - нужен для группировки логически связанных членов
        - от него нельзя наследоваться
        - не может реализовывать интерфейс
        - не может наследоваться от других классов, кроме System.Object

        Абстрактные методы и свойства:
        - могут быть лишь в абстрактных классах
        - не могут иметь тело
        - должны быть переопределены наследником
        - не могут быть private

        Абстрактный класс может иметь:
        - переменные
        - абстрактные методы
        - абстрактные свойства
        - конструкторы
        - индексаторы
        - события
    */

    static class A
    {
        static public void F1() => Console.WriteLine("A3");
    }

    abstract class B
    {
        public abstract string Name { get; set; }

        public void F2() => Console.WriteLine("B3");

        public abstract void F3();
    }

    class AbstractStatic : B
    {
        string name = "";

        public override string Name
        {
            get { return "Mr/Ms. " + name; }

            set { name = value; }
        }

        public void Main_()
        {
            A.F1();

            B objB = new AbstractStatic();

            objB.F2();
            objB.F3();
        }

        public override void F3()
        {
            Console.WriteLine("Program");
        }
    }
}
