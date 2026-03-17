namespace Education.General.Types;

public class IsAsTypeof_
{
    // is
    // Проверяет, является-ли объект экземпляром указанного типа или его производным
    // Возвращает true/false

    // as
    // Преобразует объект к указанному типу или его производному:
    // - если объект может быть преобразован к указанному типу, возвращается ссылкы на преобразованный объект
    // - иначе возвращает null

    class A { }

    class B : A { }

    class C { }

    static void UseIs()
    {
        var objA = new A();
        var objB = new B();
        var objC = new C();

        if (objA is A) { /* true */ }

        if (objB is A) { /* true */ }

        if (objA is object) { /* true */ }

        if (objA is B) { /* ERROR */ }

        if (objC is A) { /* ERROR */ }
    }

    static void UseAs()
    {
        var objA = new A();
        var objB = new B();

        if (objA is B)
        {
            objB = (B)objA;
        }
        else
        {
            Console.WriteLine("Error");
        }

        // as делает описанный выше код в один шаг
        objB = objA as B;

        if (objB is not null)
        {
            Console.WriteLine("Success");
        }
        else
        {
            Console.WriteLine("Error");
        }
    }

    static void UseTypeof()
    {
        // Type содержит свойства и методы, для получения информации о типе
        Type type = typeof(StreamReader);
        Console.WriteLine(type.FullName);

        if (type.IsClass)
        {
            Console.WriteLine("Is class");
        }
        else if (type.IsAbstract)
        {
            Console.WriteLine("Is abstract class");
        }
        else
        {
            Console.WriteLine("Is concrete class");
        }
    }

    static void Main_()
    {
        UseIs();
        UseAs();
        UseTypeof();
    }
}
