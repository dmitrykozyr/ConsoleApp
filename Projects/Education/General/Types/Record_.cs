namespace Education.General.Types;

public class Record_
{
    // Ссылочный тип, могут представлять неизменяемый immutable тип
    // Такие типы более безопасны, если данные объекта не должны изменяться

    // Есть:
    // - record классы, для них слово class можно не писать
    // - record структуры

    public record class MyRecordClass_
    {
        // init делает свойство неизменяемым
        // set тоже можно использовать
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

        public MyRecordStruct_(string name) => Name = name;
    }

    public class UserClass
    {
        public string Name { get; init; }

        public UserClass(string name) => Name = name;
    }

    public record UserRecord
    {
        public string Name { get; init; }

        public UserRecord(string name) => Name = name;
    }

    // record-классы могут наследоваться
    // Под капотом создаются свойства { get; init; }, конструктор и деконструктор
    public record Person(string Name, int Age);

    public record Employee(string Name, int Age, string Company)
        : Person(Name, Age);

    public void Main_()
    {
        var myRecordClass = new MyRecordClass_("Tom", 37);

        // Свойства с init нельзя изменять, тут будет ошибка
        //myRecordClass_.Name = "Bob";

        // records поддерживают инициализацию с помощью оператора with,
        // что позволяет создать одну record на основе другой
        var tom = new MyRecordClass_("Tom", 37);
        var sam = tom with { Name = "Sam" };

        // Если хотим скопировать все свойства - оставляем пустые скобки
        var joe = tom with
        {
        };

        // class сравнивается по ссылке
        // record сравнивается по значениям всех полей, поэтому тут будет true
        var person_1 = new Person("Tom", 37);
        var person_2 = new Person("Joe", 25);
        Console.WriteLine(person_1 == person_2);

        // Использование премуществ кортжей и деконструктора, определенного выше
        var (personName, personAge) = person_1;
    }
}
