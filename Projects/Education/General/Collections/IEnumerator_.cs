using System.Collections;

namespace Education.General.Collections;

public class IEnumerator_
{
    /*         
        Позволяет перебирать элементы коллекции
        Предоставляет методы для перемещения по коллекции и доступ к текущему элементу
        Обычно не используется напрямую, а работаем с ним через foreach.
    */

    public static void Main_()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };

        IEnumerator enumerator = numbers.GetEnumerator();

        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
    }
}
