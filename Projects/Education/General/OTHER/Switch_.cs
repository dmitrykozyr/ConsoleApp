namespace Education.General;

public class Switch_
{
    public string? MyProperty { get; init; }

    static void Main_()
    {
        object a = "5"; // Тип object 

        // v1
        switch (a)
        {
            case string s: // Преобразование object в string
                break;
            case Switch_ sw when sw.MyProperty == "1":
                // Если на вход передается объект класса Switch_
                // со значением переменной MyProperty == "1"
                break;
        }

        // v2
        var switchV2 = a switch
        {
            "1" => "Вернуть результат 1",
            "2" => "Вернуть результат 2",
            "3" or "4" => "Вернуть результат 3",
            _ => throw new NotImplementedException() // default
        };

        // v3 с кортежами
        var switchV3 = (a: 1, b: "11") switch
        {
            (1, "11") => "Вернуть результат 1",
            (2, "11") or (2, "22") => "Вернуть результат 2",
            (3, "33") => "Вернуть результат 3",
            (_, "44") => "Вернуть результат 4",         // первое значение пропущено
            (5, not "55") => "Вернуть результат 5",     // второе значение не равно 5
            _ => throw new NotImplementedException()    // default
        };

        // v4
        var c = new Switch_();
        var switchV4 = c switch
        {
            { MyProperty: "1" } => "Вернуть результат 1",
            { MyProperty: "2" } => "Вернуть результат 2",
            _ => throw new NotImplementedException() // default
        };
    }
}
