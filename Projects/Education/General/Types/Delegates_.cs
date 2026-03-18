namespace Education.General.Types;

public class Delegates_
{
    // На делегаты можно подписать один и более методов, они вызовутся при вызове делегата
    // Их можно суммировать
    // Сигнатуры делегата и его методов должны совпадать (количество и типы аргументов)
    // Если на делегат подписано несколько методов, возвращающих значение, то через делегат получим значение последнего метода
    // Если один из методов в списке выбросит исключение, то последующие методы не будут вызваны
    // Если попытаться вычесть метод, которого нет в списке, ошибки не будет, а если вычесть последний метод, переменная станет null

    #region Пример работы с делегатом

    delegate int Delegate(int a, int b);

    public static int F1(int a, int b)
    {
        Console.WriteLine($"Метод F1: {a}, {b}");
        return 1;
    }

    public static int F2(int a, int b)
    {
        Console.WriteLine($"Метод F2: {a}, {b}");
        return 2;
    }

    public void Main_()
    {
        Delegate delegate_1 = F1;
        delegate_1 += F2;

        // Метод F1: {a}, {b}
        // Метод F2: {a}, {b}
        int result = delegate_1(1, 2);

        // Вернет: 2 - результат последнего метода
        Console.WriteLine(result);


        Delegate delegate_2 = F2;

        // Вызов делегата
        delegate_2 += F1;
        delegate_2(3, 4);

        // Делегаты можно суммировать и вычетать, порядок имеет значение
        Console.WriteLine("Суммирование делегатов:");

        Delegate delegate_3 = delegate_2 + delegate_1;

        // Вызов делегата
        delegate_3(5, 6);
    }

    #endregion

    // void Action(0..16)

    public void UseAction()
    {
        Action<string> delAction = PrintMessage_1;
        delAction += PrintMessage_2;
        delAction("111");
    }

    public void PrintMessage_1(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintMessage_2(string message)
    {
        Console.WriteLine(message);
    }


    // bool Predicate(1)

    public void UsePredicate()
    {
        Predicate<int> isEven = IsEven;
        bool result = isEven(4);
    }

    bool IsEven(int number)
    {
        return number % 2 == 0;
    }


    // value Func(0..16)

    public void UseFunc()
    {
        Func<int, int, int> add = Add;
        int sum = add(3, 4);
    }

    public int Add(int a, int b)
    {
        return a + b;
    }
}
