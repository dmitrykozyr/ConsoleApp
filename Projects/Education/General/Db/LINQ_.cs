namespace Education.General.Db;

public class LINQ_
{
    // В LINQ оператор Select проецирует каждый элемент в новую форму
    // Он используется для преобразования элементов последовательности,
    // например, для выбора определенных св-в объектов или создания новых объектов на основе существующих
    public class A
    {
        public int Id { get; init; }

        public string? Name { get; init; }
    }

    public void Main_()
    {
        IEnumerable<A> list = new List<A>()
        {
            new A { Id = 2, Name = "Dima" },
            new A { Id = 4, Name = "Champion" },
            new A { Id = 1, Name = "Developer" },
        };

        // Method Syntax
        // Запрос не выполнится, пока не будет вызвана ф-я FirstOrDefault(), ToList()
        var query_1 = list
            .Where(z => z.Name?.StartsWith('D') == true)
            .OrderBy(z => z.Id);

        // Query Syntax
        var query_2 =
            from i in list
            where i.Id > 1
            orderby i.Name
            select i;

        foreach (var i in query_2)
        {
            Console.WriteLine($"{i.Id} {i.Name}");
        }
    }
}
