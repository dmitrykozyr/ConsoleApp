using System.Collections;

namespace Education.General.Collections;

public class Collections_
{
    public void Main_()
    {
        // List

            // Хранит однотипные объекты
            // Внутри это массив, можно обращаться к элементам по индексу
            var list_1 = new List<int> { 0, 1 };

            // Эквивалентно предыдущей записи
            var list_2 = new List<int>() { 0, 1 };
            list_2[0] = 1;

        // LinkedList - двусвязный список

            // Каждый элемент хранит ссылку на следующий и предыдущий элемент
            var linkedList = new LinkedList<int>();
            linkedList.AddLast(1);

            linkedList.AddFirst(2);

            linkedList.AddAfter(linkedList.Last, 3);

        // Dictionary

            // В словаре нельзя создать два поля с одинаковым ключем
            // Ключ преобразуется в хеш, по которому поиск идет за O(1)
            var dictionary = new Dictionary<int, string>
            {
                 { 0, "0" },
                 { 1, "1" }
            };

        // Hashtable

            // Похоже на словарь, но ключи и значения приводятся к object,
            // что увеличивает расход памяти, но поддерживает многопоточное чтение
            var hashtable = new Hashtable()
            {
                 { 0, "00" },
                 { 1, "11" },
                 { 2, "22" },
            };

            var count = hashtable.Count;

            hashtable.Add(3, "33");

            hashtable.Remove(2);

        // HashSet

            // Хранит уникальную коллекцию в неотсортированном виде
            var hashSet = new HashSet<int>();
            hashSet.Add(2);
            hashSet.Add(3);
            hashSet.Add(1);
            hashSet.Add(1); // Не добавится, т.к. значение уже есть в коллекции
                            // Будет 2 3 1

        // ArrayList

            // Позволяет хранить разнотипные объекты
            var arrayList = new ArrayList { Capacity = 50 };
            arrayList.Add(1);

            Console.WriteLine(arrayList[0]);

            arrayList.RemoveAt(0);

            arrayList.Reverse();

            arrayList.Remove(4);

            arrayList.Sort();

            arrayList.Clear();

        // SortedList

            // Коллекция отсортирована по ключу
            var sortedList = new SortedList
            {
                { 0, "0" },
                { 1, "1" },
                { 2, "2" },
            };

            sortedList.Add(3, "3");

            sortedList.Clear();

        // Stack

            // Первый пришел - последний ушел
            var stack = new Stack();

            stack.Push("string");

            stack.Push(4);

            // Удалить элемент
            stack.Pop();

            stack.Clear();

        // Queue

            // Первый пришел - первый ушел
            var queue = new Queue();

            queue.Enqueue("string");

            queue.Enqueue(5);

            // Возвращает элемент из начала очереди
            queue.Dequeue();

        // IList

            // Позволяет создать метод или класс, который может работать с разными типами коллекций
            void IList_()
            {
                var list = new List<string> { "Item_1", "Item_2" };

                // Можно передать List в метод, принимающий IList
                ProcessItems(list);

                AddItem(list, "Item_3");


                // Метод может принимать любой объект, реализующий IList<string>
                void ProcessItems(IList<string> items)
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine(item);
                    }
                }

                void AddItem(List<string> items, string newItem)
                {
                    items.Add(newItem);
                }
            }
    }
}