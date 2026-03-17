namespace Education.General.Collections;

public class IEnumerable_
{
    /*
        Определяет метод GetEnumerator(), позволяющий перебрать коллекцию поочередно
        Является базовым интерфейсом всех коллекций, которые могут быть перечислены в C#
        Если класс реализует IEnumerable - значит его экземпляры могут быть использованы в конструкции foreach
        Преобразуется в SQL без WHERE - отбирается вся коллекция, а потом фильтруется на клиенте:

            IEnumerable<Phone> phoneIEnum = db.Phones;
            var phones = phoneIEnum.Where(p => p.Id > id).ToList();
            SELECT Id, Name FROM dbo.Phones
    */

    public static IEnumerable<int> GetNumbers()
    {
        for (int i = 0; i < 5; i++)
        {
            // Используем yield для отложенного выполнения
            yield return i;
        }
    }

    public static void Main_()
    {
        IEnumerable<int> numbers = GetNumbers();

        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
