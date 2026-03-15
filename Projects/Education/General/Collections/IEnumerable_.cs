namespace Education.General.Collections;

public class IEnumerable_
{
    /*
        Определяет метод GetEnumerator(), позволяющий перебрать коллекцию поочередно
        Если класс реализует IEnumerable - его экземпляры могут быть использованы в foreach
        Преобразуется в SQL без WHERE - отбирается вся коллекция, а потом фильтруется на клиенте

            IEnumerable<Phone> phoneIEnum = db.Phones;
            var phones = phoneIEnum.Where(p => p.Id > id).ToList();
            SELECT Id, Name FROM dbo.Phones
    */

    public static void Main_()
    {
        IEnumerable<int> numbers = GetNumbers();

        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }

    // При вызове метода с yield return, он не выполняется полностью, а возвращает объект IEnumerable или IEnumerator
    // При каждом вызове MoveNext() на этом объекте, выполнение продолжается с места остановки
    public static IEnumerable<int> GetNumbers()
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
