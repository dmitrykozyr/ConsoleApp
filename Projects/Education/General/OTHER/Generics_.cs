namespace Education.General;

public class Generics_
{
    // Т должен содержать метод CompareTo из интерфейса IComparable
    public class GenericMath<T>
        where T : IComparable<T>
    {
        public T GetMax(T first, T second)
        {
            var result =
                first.CompareTo(second) > 0
                    ? first
                    : second;

            return result;
        }
    }

    // T должен быть классом с пустым конструктором
    public class DataProcessor<T>
        where T : class, new()
    {
        public void Process(T item, Action<T> action)
        {
            Console.WriteLine($"Обработка объекта типа: {typeof(T).Name}");

            // Вызываем делегат
            action(item);
        }

        public TResult ConvertTo<TResult>(T item, Func<T, TResult> converter)
        {
            return converter(item);
        }
    }

    public class User
    {
        public string Name { get; set; } = "";
    }

    public class Program
    {
        public static void Main_()
        {
            var senericMath_int = new GenericMath<int>();
            var genericMath_str = new GenericMath<string>();

            var maxInt = senericMath_int.GetMax(10, 25);
            var maxStr = genericMath_str.GetMax("Apple", "Zebra");


            var processor = new DataProcessor<User>();
            var user = new User { Name = "Alice" };

            processor.Process(user, u =>
            {
                Console.WriteLine($"Hello, {u.Name}");
            });

            processor.ConvertTo(user, u =>
            {
                return u.Name.ToUpper();
            });
        }
    }
}
