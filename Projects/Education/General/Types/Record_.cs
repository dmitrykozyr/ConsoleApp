namespace Education.General.Types;

public class Record_
{
    // Ссылочный тип, могут представлять неизменяемый immutable тип
    // Такие типы более безопасны, если данные объекта не должны изменяться

    // Есть
    // - record классы, для них слово class можно не писать
    // - record структуры

    public record class MyRecordClass_
    {
        // init делает св-во неизменяемым
        // set тоже можно использовать, но тогда св-во будет изменяемым
        public string Name { get; init; }

        public int Age { get; init; }

        public MyRecordClass_(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Деконструктор позволяет разложить объект на кортеж значений
        public void Deconstruct(out string name, out int age)
        {
            (name, age) = (Name, Age);
        }
    }

    public record struct MyRecordStruct_
    {
        public string Name { get; init; }

        public MyRecordStruct_(string name)
        {
            Name = name;
        }
    }

    public class UserClass
    {
        public string Name { get; init; }

        public UserClass(string name)
        {
            Name = name;
        }
    }

    public record UserRecord
    {
        public string Name { get; init; }

        public UserRecord(string name)
        {
            Name = name;
        }
    }

    // Как и обыччные классы, record-классы могут наследоваться
    public record Person(string Name, int Age);

    public record Employee(string Name, int Age, string Company)
        : Person(Name, Age);

    static void Main_()
    {
        // В record св-во с init нельзя изменять
        var myRecordClass_ = new MyRecordClass_("Tom", 37);
        //myRecordClass_.Name = "Bob"; // ошибка

        // records поддерживают инициализацию с помощью оператора with
        // Он позволяет создать одну record на основе другой
        var tom = new MyRecordClass_("Tom", 37);
        var sam = tom with { Name = "Sam" };

        // Если хотим скопировать все св-ва - оставляем пустые скобки
        var joe = tom with { };

        // Использование премуществ кортжей и деконструктора, определенного выше
        var person = new MyRecordClass_("Tom", 37);
        var (personName, personAge) = person;
    }
}
