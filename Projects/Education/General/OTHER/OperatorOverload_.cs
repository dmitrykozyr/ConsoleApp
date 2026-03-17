namespace Education.General;

public class OperatorOverload_
{
    /*
        Для перегрузки определеним в классе, для объектов которого хотим определить оператор, метод, содержащий:
        - модификаторы public static - будет использоваться всеми объектами класса
        - название возвращаемого типа
        - вместо названия метода идет слово operator и сам оператор
        - в скобках перечисляются параметры - один из них должен представлять класс или структуру, в котором определяется оператор            
          В примере перегруженные операторы проводятся над двумя объектами, поэтому для каждой перегрузки будет два параметра
    */

    int Value { get; init; }

    public static OperatorOverload_ operator +(OperatorOverload_ c1, OperatorOverload_ c2)
    {
        return new OperatorOverload_
        {
            Value = c1.Value + c2.Value
        };
    }

    public static bool operator >(OperatorOverload_ c1, OperatorOverload_ c2)
    {
        return c1.Value > c2.Value;
    }

    public static bool operator <(OperatorOverload_ c1, OperatorOverload_ c2)
    {
        return c1.Value < c2.Value;
    }

    static void Main_()
    {
        var c1 = new OperatorOverload_ { Value = 23 };
        var c2 = new OperatorOverload_ { Value = 45 };

        bool result = c1 > c2;
        Console.WriteLine(result);

        var c3 = c1 + c2;
        Console.WriteLine(c3.Value);
    }
}
