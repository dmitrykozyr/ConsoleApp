namespace Education.General.Types;

public class IsAsTypeof_
{
    /*
        is
            Проверяет, является-ли объект экземпляром указанного типа или его производным
            Возвращает true/false

        as
            Преобразует объект к указанному типу или его производному:
            - если объект может быть преобразован к указанному типу,
              возвращается ссылкы на преобразованный объект
            - иначе возвращает null
    */

    class A { }
    class B : A { }
    class C { }

    public void UseIs()
    {
        object objA = new A();
        object objB = new B();
        object objC = new C();

        bool check_1 = objA is A;       // true
        bool check_2 = objB is A;       // true
        bool check_3 = objA is object;  // true
        bool check_4 = objA is B;       // false - базовый класс не является производным
        bool check_5 = objC is A;       // false - несвязанные иерархии

        // Pattern Matching
        // Проверяем тип и сразу создаем переменную 'b', если проверка прошла
        // Переменная 'b' уже типизирована как B, кастить (B)objB не нужно
        if (objB is B b)
        {
            Console.WriteLine($"Это объект типа {nameof(B)}");
        }
    }

    public void UseAs()
    {
        object objA = new A();

        // as возвращает null, если приведение невозможно
        B? b = objA as B;

        if (b is not null)
        {
            Console.WriteLine("Успешное приведение");
        }
        else
        {
            Console.WriteLine("Преобразование через 'as' вернуло null");
        }
    }

    public void UseTypeofAndGetType()
    {
        // typeof работает с именем типа, который известен при компиляции
        Type type_1 = typeof(StreamReader);

        // GetType() работает с экземплером и узнает реальный тип в рантайме
        A instance = new B();

        // Вернет тип B, хотя переменная объявлена как A
        Type type_2 = instance.GetType();

        Console.WriteLine($"Static type: {type_1.Name}");
        Console.WriteLine($"Runtime type: {type_2.Name}");

        if (type_1.IsClass)
        {
            // Абстрактный класс — это тоже класс, поэтому проверяем внутри
            if (type_1.IsAbstract)
            {
                Console.WriteLine($"{type_1.Name} абстрактный класс");
            }
            else
            {
                Console.WriteLine($"{type_1.Name} конкретный класс");
            }
        }
    }
}
