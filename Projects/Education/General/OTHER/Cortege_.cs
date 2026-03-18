namespace Education.General;

public class Cortege_
{
    public void Main_()
    {
        var tuple_1 = (count: 5, sum: 10);
        var tuple_2 = GetValues();
        var tuple_3 = GetPersonInfo();
        var tuple_4 = GetTupleFromDictionary();

        // Можно сразу распаковывать кортеж в отдельные переменные
        var (number, name_1, year) = GetPersonInfo();

        // Берем только имя
        var (_, name_2, _) = GetPersonInfo();

        Console.WriteLine($"{tuple_1.count} {tuple_1.sum}");
        Console.WriteLine($"{tuple_2.Item1} {tuple_2.Item2}");
        Console.WriteLine($"{tuple_3.number} {tuple_3.name} {tuple_3.year}");
        Console.WriteLine(tuple_4);
    }

    static (int, int) GetValues()
    {
        var result = (1, 3);

        return result;
    }

    static (int number, string name, int year) GetPersonInfo()
    {
        return (1, "Joe", 2001);
    }

    static string GetTupleFromDictionary()
    {
        var tuples = new Dictionary<(int, int), string>
        {
            { (1, 2), "string1" },
            { (3, 4), "string2" }
        };

        string result = tuples[(1, 2)];

        return result;
    }
}
