namespace Education.General;

public class Equals_
{
    /*
        Позволяет определить, равны-ли два объекта по содержимому, а не по адресу в памяти
        По умолчанию сравнивает ссылки объектов
        Но можно переопределить, чтобы сравнивать объекты по значению их полей
    */

    public record RecordA(int Value, string Value2);

    public class Person
    {
        // Оба числа 1 приводятся к типу object, и сравниваются как ссылки
        // false
        bool a = (object)1 == (object)1;

        // Метод Equals сравнивает значения, а не ссылки
        // true
        bool b = 1.Equals(1);

        // Благодаря интернированию строк, это один и тот-же участок в памяти
        // true
        bool c = (object)"1" == (object)"1";

        // Для record метод Equals переопределен и сравнивает значения всех свойств, а не ссылки
        // true
        bool d = new RecordA(1, "TEST").Equals(new RecordA(1, "TEST"));

        public string? Name { get; init; }

        public int Age { get; init; }

        public override bool Equals(object? obj)
        {
            // Проверяем, что объект не равен null и является экземпляром Person
            if (obj is Person other)
            {
                return this.Name == other.Name && this.Age == other.Age;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age);
        }
    }

    public class Program
    {
        static void Main_()
        {
            var person1 = new Person { Name = "Alice", Age = 30 };
            var person2 = new Person { Name = "Alice", Age = 30 };
            var person3 = new Person { Name = "Bob", Age = 25 };

            Console.WriteLine(person1.Equals(person2)); // true
            Console.WriteLine(person1.Equals(person3)); // false
        }
    }
}
