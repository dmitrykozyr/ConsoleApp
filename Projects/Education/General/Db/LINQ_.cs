namespace Education.General.Db;

public class LINQ_
{
    // В LINQ оператор Select проецирует каждый элемент в новую форму
    // Он используется для преобразования элементов последовательности,
    // например, для выбора определённых св-в объектов или создания новых объектов на основе существующих

    class A
    {
        public int Id { get; init; }

        public string? Name { get; init; }
    }

    static void Main_()
    {
        IEnumerable<A> collA = new List<A>()
            {
                new A { Id = 2, Name = "Dima" },
                new A { Id = 4, Name = "Champion" },
                new A { Id = 1, Name = "Developer" },
            };

        // Запрос не выполнится, пока не будет вызвана ф-я FirstOrDefault(), ToList() и т.п.
        var query1 = collA.Where(z => z.Name.StartsWith('D')).OrderBy(z => z.Id);

        var query2 = from i in collA
                     where i.Id > 1
                     orderby i.Name
                     select i;

        foreach (var i in query2)
        {
            Console.WriteLine(i.Id + " " + i.Name);
        }
    }
}
