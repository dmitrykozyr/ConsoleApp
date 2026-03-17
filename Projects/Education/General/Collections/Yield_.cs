namespace Education.General.Collections;

public class Yield_
{
    /*        
        yield return:
        - возвращает последовательности значений по одному за раз
        - позволяет экономить память и время, т.к. элементы генерируются по мере необходимости, а не все сразу
        - полезен при работе с большими коллекциями

        Когда вызывается метод, содержащий yield return, он не выполняется полностью,
        а возвращает объект IEnumerable или IEnumerator       

        При каждом вызове метода MoveNext() на этом объекте выполнение продолжается с места,
        где было остановлено, и возвращается следующее значение

        yield break завершает итератор
    */

    class Program
    {
        static void Main_()
        {
            foreach (var number in GetNumbers())
            {
                Console.WriteLine(number);
            }
        }

        static IEnumerable<int> GetNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    // Завершаем итерацию
                    yield break;
                }

                // Возвращаем текущее значение i
                yield return i;
            }
        }
    }
}
