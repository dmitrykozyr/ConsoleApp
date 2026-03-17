namespace Education.General;

public class Cortege_
{
    static (int, int) GetValues()
    {
        var result = (1, 3);
        return result;
    }

    static (int number, string name, int year) F2()
    {
        return (1, "Dima", 1990);
    }

    static void Main_()
    {
        var tuple = (count: 5, sum: 10);
        Console.WriteLine(tuple.count + " " + tuple.sum);

        tuple = GetValues();
        Console.WriteLine(tuple.count + " " + tuple.sum);

        var tuple2 = F2();
        Console.WriteLine(tuple2.number + tuple2.name + tuple2.year);

        var tupleDictionary = new Dictionary<(int, int), string>
            {
                { (1, 2), "string1" },
                { (3, 4), "string2" }
            };

        var result = tupleDictionary[(1, 2)];
        Console.WriteLine(result);
    }
}
