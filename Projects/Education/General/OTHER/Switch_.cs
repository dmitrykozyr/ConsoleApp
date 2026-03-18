namespace Education.General;

public class SwitchData
{
    public string? MyProperty { get; init; }
}

public class Switch_
{
    public void Main_()
    {
        object valueToCompare = "5";
        var switch_1 = valueToCompare switch
        {
            "1"                     => "Вернуть результат 1",
            "2" or "3"              => "Вернуть результат 2, 3",
            _                       => throw new NotImplementedException() // default
        };


        var switch_2 = (a: 1, b: "1") switch
        {
            (1, "1")                => "Вернуть результат 1",
            (2, "1") or (2, "2")    => "Вернуть результат 2",
            (3, "3")                => "Вернуть результат 3",
            (_, "4")                => "Вернуть результат 4", // первое значение пропущено
            (5, not "5")            => "Вернуть результат 5", // второе значение не равно 5
            _                       => throw new NotImplementedException()
        };


        var switchData = new SwitchData();
        var switch_3 = switchData switch
        {
            { MyProperty: "1" }     => "Результат 1",
            { MyProperty: "2" }     => "Результат 2",
            null                    => "Объект не создан", // Обработка null
            _                       => throw new NotImplementedException()
        };


        int age = 25;
        var switch_4 = age switch
        {
            < 18                    => "Ребенок",
            >= 18 and <= 65         => "Взрослый",
            _                       => "Пенсионер"
        };
    }
}
