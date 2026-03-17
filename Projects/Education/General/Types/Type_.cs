namespace Education.General.Types;

public class Type_
{
    /*
        Можно узнать имя типа, его пространство имен, базовый тип
        С помощью метода Activator.CreateInstance можно создавать экземпляры типов динамически
        Можно получать информацию об атрибутах, примененных к типу

        Класс Type предоставляет информацию о типах данных, таких как:
        - классы
        - структуры
        - интерфейсы
        - перечисления
        - массивы

        Позволяет получить информацию о:
        - свойствах
        - методах
        - конструкторах
        - других членах типа
        - его атрибутах и базовых типах            
    */

    // Выведм информацию о классе Program, включая его методы и созданный экземпляр
    static void Main_()
    {
        // Используем typeof(Program), чтобы получить объект Type, представляющий класс Program
        Type type = typeof(Program);

        // Выводим информацию о типе
        Console.WriteLine($"Имя типа:               {type.Name}");
        Console.WriteLine($"Полное имя типа:        {type.FullName}");
        Console.WriteLine($"Пространство имен:      {type.Namespace}");
        Console.WriteLine($"Базовый тип:            {type.BaseType}");

        // Создание экземпляра класса динамически
        var instance = Activator.CreateInstance(type);

        Console.WriteLine($"Создан экземпляр типа:  {instance?.GetType().Name}");

        // Получим все методы класса Program и выведем их имена
        var methods = type.GetMethods();

        foreach (var method in methods)
        {
            Console.WriteLine(method.Name);
        }
    }
}
