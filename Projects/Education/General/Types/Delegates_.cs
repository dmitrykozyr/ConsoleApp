namespace Education.General.Types;

public class Delegates_
{
    // На делегаты можно подписать один и более методов, они вызовутся при вызове делегата
    // Их можно суммировать
    // Сигнатуры делегата и его методов должны совпадать (количество и типы аргументов)
    // Если на делегат подписано несколько методов, возвращающих значение, то через делегат получим значение последнего метода

    #region Пример работы с делегатом

        delegate int Del1(int a, int b);

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

        static void Main_()
        {
            Del1 del1;

            del1 = F1;
            del1 += F2;

            // Вернет:
            // Метод F1: {a}, {b}
            // Метод F2: {a}, {b}
            var result = del1(1, 2);

            // Вернет: 2 - результат последнего метода
            Console.WriteLine(result);

            Del1 del2;
            del2 = F2;
            del2(3, 4);

            // Делегаты можно суммировать и вычетать, порядок имеет значение
            Console.WriteLine("Суммирование делегатов:");
            Del1 del3 = del2 + del1;
            del3(5, 6);
        }

    #endregion

    // void Action(0 .. 16)

        void UseAction()
        {
            Action<string> delAction = PrintMessage_1;
            delAction += PrintMessage_2;
            delAction("111");
        }

        void PrintMessage_1(string message)
        {
            Console.WriteLine(message);
        }

        void PrintMessage_2(string message)
        {
            Console.WriteLine(message);
        }


    // bool Predicate(1)

        void UsePredicate()
        {
            Predicate<int> isEven = IsEven;
            bool result = isEven(4);
        }

        bool IsEven(int number)
        {
            return number % 2 == 0;
        }


    // value Func(0 .. 16)

        void UseFunc()
        {
            Func<int, int, int> add = Add;
            int sum = add(3, 4);
        }

        int Add(int a, int b)
        {
            return a + b;
        }
}
