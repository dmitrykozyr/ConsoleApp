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

    //!class Abstract
}
