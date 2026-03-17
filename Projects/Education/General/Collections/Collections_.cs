using System.Collections;

namespace Education.General.Collections;

public class Collections_
{
    static void Main_()
    {
        // List

        // Хранит однотипные объекты
        // Внутри это массив, можно обращаться к элементам по индексу
        var list = new List<int>();
        list.Add(0);
        list.Add(1);

        // Эквивалентно предыдущей записи
        var list2 = new List<int>() { 0, 1 };
        list2[0] = 1;

        // LinkedList - двусвязный список

        // Каждый элемент хранит ссылку на следующий и предыдущий элемент
        var numbers = new LinkedList<int>();
        numbers.AddLast(1);
        numbers.AddFirst(2);
        numbers.AddAfter(numbers.Last, 3);

        // Dictionary

        // В словаре нельзя создать два поля с одинаковым ключем
        // Ключ преобразуется в хеш, по которому поиск идет за O(1)
        var dictionary = new Dictionary<int, string>();
        dictionary.Add(0, "0");
        dictionary.Add(1, "1");

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
        stack.Pop(); // Удалить элемент
        stack.Clear();

        // Queue

        // Первый пришел - первый ушел
        var queue = new Queue();
        queue.Enqueue("string");
        queue.Enqueue(5);
        queue.Dequeue(); // Возвращает элемент из начала очереди
    }

    #region IList и List

    /*         
        IList

            Используется, если хотим создать метод или класс, который может работать с различными типами коллекций

            Интерфейс определяет операции для работы с коллекциями:
            - Add
            - Remove
            - Insert
            - IndexOf
            - свойство Count
            - свойство Item[index] для доступа к элементу по индексу

        List

            Конкретная реализация интерфейса IList
            Представляет изменяемый массив, предоставляющий возможность хранить элементы и управлять ими
            Используется, если нужно работать с конкретной реализацией коллекции и использовать ее специфические методы

            Включает дополнительные методы:
            - Sort
            - Reverse
            - и другие
    */

    // Метод ProcessItems может принимать любой объект, реализующий IList<string>, что делает его более универсальным
    public void F1()
    {
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

        var myItems = new List<string> { "Item1", "Item2" };

        ProcessItems(myItems); // Можно передать List в метод, принимающий IList

        AddItem(myItems, "Item3");
    }

    #endregion

    // Дерево, возврат всех листьев
    public class Tree
    {
        public class TreeData
        {
            public int Id { get; init; }

            public TreeData[]? Child { get; init; }
        }

        static void ShowLeafIds(TreeData root)
        {
            if (root is null)
            {
                return;
            }

            if (root.Child is null || root.Child.Length == 0)
            {
                Console.WriteLine(root.Id);
            }
            else
            {
                foreach (var child in root.Child)
                {
                    ShowLeafIds(child);
                }
            }
        }

        static void Main_()
        {
            // Инициализация дерева
            /*
                        1
                    2       3
                  4   5   6
            */

            var treeData = new TreeData
            {
                Id = 1,
                Child = new TreeData[]
                {
                        new TreeData
                        {
                            Id = 2,
                            Child = new TreeData[]
                            {
                                new TreeData
                                {
                                    Id = 4
                                    //Child = null
                                },
                                new TreeData
                                {
                                    Id = 5
                                    //Child = null
                                }
                            }
                        },
                        new TreeData
                        {
                            Id = 3,
                            Child = new TreeData[]
                            {
                                new TreeData
                                {
                                    Id = 6
                                    //Child = null
                                }
                            }
                        }
                }
            };

            ShowLeafIds(treeData); // 4 5 6
        }
    }
}
