using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SharpEdu;

class Program
{
    static void Main()
    {
    }

    class DotNetCoreLifeCycle_
    {
        /*        
            Жизненный цикл .Net Core приложения

            - Создание нового проекта с помощью шаблонов .NET Core (например, через команду dotnet new)
            - Настройка зависимостей, конфигураций и служб в файле Startup.cs и других конфигурационных файлах
            - Запуск приложения
            - Приложение загружает необходимые сборки и зависимости
            - Создание хоста
              Создается экземпляр IHost, который управляет жизненным циклом приложения
              Это может быть WebHost для веб-приложений или Host для консольных приложений
            - Приложение считывает конфигурации из различных источников (файлы, переменные окружения, секреты) и настраивает службы
            - Все необходимые службы (например, контроллеры, сервисы, репозитории) регистрируются в контейнере зависимостей
            - При запуске веб-приложения сервер (например, Kestrel) начинает прослушивать входящие HTTP-запросы
            - Сервер обрабатывает входящие запросы, создавая экземпляры контроллеров и вызывая соответствующие методы для обработки запросов
            - Запрос проходит через middleware, которое может выполнять аутентификацию, логирование, обработку ошибок
            - Запрос направляется к соответствующему контроллеру и его методу, который обрабатывает бизнес-логику
            - Контроллер формирует ответ (например, HTML, JSON или другой формат) и возвращает его обратно через middleware
            - Ответ отправляется клиенту через сервер
            - Когда приложение завершается (например, при получении сигнала остановки), оно очищает ресурсы,
              такие как закрытие соединений с БД, освобождение памяти
            - Хост завершает работу, вызывая соответствующие методы для завершения всех зарегистрированных служб
        */
    }

    class General_
    {
        /*
            const                   инициализация при обьявлении,                    неявно статические, нельзя помечать как static
            readonly                инициализация в конструкторе или при объявлении, неявно статические, можно  помечать как static
            partial                 позволяет разбить класс на несколько частей
        
            ref                     передача аргумента по ссылке, инициализировать до вызова метода
            out                                                   можно не инициализировать до вызова метода
        
            TCP                     отправляет сообщение, пока не получит подтверждение о доставке или не будет превышено число попыток
            UDP                     не гарантирует доставку, но более быстрый и подходит для широковещательной передачи
        
            protected               доступ в текущем и производных классах, производные классы могут быть в других сборках
            internal                доступ в той же сборке
            protected internal      доступ в текущей сборке и из производных классов
            private protected       доступ в текущем и производных классах в той же сборке
        
            Если sealed применить к:
            - классу - запрещаем наследоваться от класса
            - override-методу - запрещаем переопределение

            override можно применять к виртуальным и абстрактным методам

            Стек ограничен по размеру (2 Гб), но быстрее
            Куча медленнее, но это почти вся оперативная память

            Дебаг:
            - F5 - до следующей точки останова
            - F10 - на следующий шаг
            - F11 - зайти внутрь метода

            Псевдоним:
            using SCG = System.Collections.Generic;

            Немедленное выполнение - операция выполнися в точке, где объявлен запрос
            Отложенное выполнение  - результат запроса зависит от источника данных в момент выполнения запроса, а не при его определении

            Статические классы могут содержать статические поля, св-ва и методы    
            Статические св-ва/методы хранят состояние всего класса, а не отдельного объекта, обращение по имени класса
            Статические методы могут обращаться только к статическим членам класса
        
            Статические конструкторы:
            - не имеют модификаторов доступа и не принимают параметры
            - нельзя использовать слово this для ссылки на текущий объект класса
            - вызовутся автоматически при первом создании объекта класса или первом обращении к статическому члену
            - нужны для инициализации статических данных
            - выполняются один раз

            Значимые типы:                                   Разрядность в битах  Диапазон представления чисел
            bool     логический
            byte     8-разрядный целочисленный без знака     8   ---------------  0 - 255
            char     символьный                              16  ---------------  0 - 65.535
            decimal  десятичный (для финансовых расчетов)    128 ---------------  1Е-28 - 7,9Е+28
            double   с плавающей точкой двойной точности     64  ---------------  5Е-324 - 1,7Е+308
            float    с плавающей точкой одинарной точности   32  ---------------  5Е-45 - 3,4Е+38
            int      целочисленный                           32  ---------------  -2.147.483.648 - 2.147.483.647
            long     длинный целочисленный                   64  ---------------  -9.223.372.036.854.775.808 - 9.223.372.036.854.775.807
            sbyte    8-разрядный целочисленный со знаком     16  ---------------  -128-127
            short    короткий целочисленный                  16  ---------------  -32.768 - 32.767
            uint     целочисленный без знака                 32  ---------------  0 - 4.294.967.295
            ulong    длинный целочисленный без знака         64  ---------------  0 - 18.446.744.073.709.551.615
            ushort   короткий целочисленный без знака        16  ---------------  0 - 65.535

            Ссылочные типы:
            - object
            - string
            - class
            - interface
            - delegate

            Если переменная представляет значимый тип, то в стеке будет храниться непосредсвенное значение
        
            Ссылочные типы хранятся в куче, а при создании объекта ссылочного типа в стек помещается ссылка на адрес в куче
        
            KISS - Keep It Simple, Stupid
            DRY  - Don’t Repeat Yourself
        */

        General_() { }                      // Вызов конструктора по умолчанию
        General_(string data) : this() { }  // this вызывает конструктор по умолчанию,
                                            // base вызывает конструктор родительского класса

        enum MyEnum : int
        {
            a = 0,
            b = 5,
            c,
        }
        //Console.WriteLine((int)MyEnum.c);

        Double[,] myDoubles = new Double[10, 20];
        string[,,] myStrings = new string[5, 3, 10];

        /*
            int? x = null;          Переменная может принмать null
            int y = x ?? 5;         Если левый операнд is not null, то вернется он, иначе - правый

            int i = 5;              Упаковка нужна, чтобы работать со значимым типом, как с ссылочным,
            object obj = (object)i; чтобы при передаче в метод не создавался дубликат, а передавался адрес
            short a = 5;            Распаковка должна производиться в тип, из которого производилась упаковка
            object o = a;           Упаковка значимого типа в ссылочный
            short b = (short)o;     Распаковка  
        */

        int LocalFunction(int a, int b) // Локальная функция
        {
            int LocalFunction(int c, int d) { return c + d; }
            return LocalFunction(a, b);
        }

        string _name;                   // Свойства могут быть виртуальными, их можно переопределять    
        string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    class String_
    {
        // Строки - это ссылочный тип

        public void F1()
        {
            // Интерполяция строк
            string name = "Alice";
            int age = 30;
            string result1 = $"Hello, my name is {name} and I am {age} years old";


            // Интернирование строк
            // Позволяет экономить память и улучшать производительность
            // Строки с одинаковыми значениями хранятся в одном и том же месте в памяти
            // Если создаем несколько строк с одинаковым содержимым, они будут ссылаться на один и тот же объект в памяти
            // При интернировании строки с одинаковыми значениями будут равны не только по содержимому, но и по ссылке
            string str1 = "Hello";
            string str2 = "Hello";
            bool result2 = ReferenceEquals(str1, str2); // true
        }
    }

    class BigO_
    {
        /*
            Big O(N)    Описывает скорость роста времени выполнения бесконечного алгоритма

            O(N^2 + B)  'B' не учитывается, мы ничего не знаем об этом значении
        
            O(A + B)    Если выполняется одна ф-я, а затем другая, то общая сложность равна сумме - они не влияют друг на друга
        
            O(A * B)    Если внутри одной ф-ии выполняется другая или есть вложенные циклы, то общая сложность равна произведению
                        При увеличении времении выполнения вложенной ф-ии, увеличивается время родительской
       
            O(0)        На вход не передаются данные, либо алгоритм их не обрабатывает
        
            O(1)        Время обработки не меняется с изменением входного объема данных
                        В ф-ии нет циклов и рекурсии, всегда выполняется фиксированное число шагов
        
            O(logN)     На каждой итерации берется половина элементов, как при бинарном поиске в отсортированном массиве
        
            O(N)        На скольлько возрастает объем входных данных, на столько возрастает время обработки
                        Входной аргумент определяет число шагов цикла/рекурсии
                        Алгоритм, описываемый как O(2N), нужно описывать без констант как O(N)
                        O(N + logN) = O(N), т.к. N > logN
            O(NlogN)
        
            O(N^A)      Есть вложенные циклы, каждый выполняет от 0 до N шагов
                        Алгоритм O(N^2 + N) описывать O(N^2)
                        O(N^2) - сложность пузырьковой сортировки
        
            O(A^N)      O(5 * 2^N + 10 * N^1000) = (2^N), т.к. степень растет быстрее всего
        
            O(N!)
        
            O(N^N)
        */
    }

    class Switch_
    {
        public string MyProperty { get; set; }

        static void Main_()
        {
            object a = "5"; // Тип object 

            // v1
            switch (a)
            {
                case string s: // Преобразование object в string
                        // ...
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
            var switchV3 = (a:1, b:"11") switch
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

    class Collections_
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

                List<string> myItems = new List<string> { "Item1", "Item2" };

                ProcessItems(myItems); // Можно передать List в метод, принимающий IList

                AddItem(myItems, "Item3");
            }

        #endregion

        // Дерево, возврат всех листьев
        public class Tree
        {
            public class TreeData
            {
                public int Id { get; set; }

                public TreeData[]? Child { get; set; }
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

    class Struct_
    {
        struct Structure_
        {
            // Могут наследоваться только от интерфейса
            // Их нельзя наследовать
            // Могут быть readonly, но все поля тоже должны быть readonly

            public string name;
            public int age;

            // Ошибка - нельзя инициализировать поля при объявлении
            //public string gender = "Male";

            // Если определяем конструктор - он должен инициализировать все поля
            public Structure_(string name, int age)
            {
                this.name = name;
                this.age = age;
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"Name: {name} Age: {age}");
            }
        }

        static void Main_()
        {
            Structure_ tom;     // Необязательно вызывать конструктор для создания объекта
                                // Надо проинициализировать все поля перед получением их значений 
            tom.name = "Tom";
            tom.age = 34;
            tom.DisplayInfo();

            var john = new Structure_("John", 37);
            john.DisplayInfo();

            var bob = new Structure_();  // Можем использовать конструктор без параметров, при вызове
                                         // которого полям будет присвоено значение по умолчанию
            bob.DisplayInfo();
        }
    }

    class Record_
    {
        // Ссылочный тип, могут представлять неизменяемый immutable тип
        // Такие типы более безопасны, если данные объекта не должны изменяться

        // Есть
        // - record классы, для них слово class можно не писать
        // - record структуры

        public record class MyRecordClass_
        {
            // init делает св-во неизменяемым
            // set тоже можно использовать, но тогда св-во будет изменяемым
            public string Name { get; init; }

            public int Age { get; init; }

            public MyRecordClass_(string name, int age)
            {
                Name = name;
                Age = age;
            }

            // Деконструктор позволяет разложить объект на кортеж значений
            public void Deconstruct(out string name, out int age)
            {
                (name, age) = (Name, Age);
            }
        }

        public record struct MyRecordStruct_
        {
            public string Name { get; init; }

            public MyRecordStruct_(string name)
            {
                Name = name;
            }
        }

        public class UserClass
        {
            public string Name { get; init; }

            public UserClass(string name)
            {
                Name = name;
            }
        }

        public record UserRecord
        {
            public string Name { get; init; }

            public UserRecord(string name)
            {
                Name = name;
            }
        }

        // Как и обыччные классы, record-классы могут наследоваться
        public record Person(string Name, int Age);

        public record Employee(string Name, int Age, string Company)
            : Person(Name, Age);

        static void Main_()
        {
            // В record св-во с init нельзя изменять
            var myRecordClass_ = new MyRecordClass_("Tom", 37);
            //myRecordClass_.Name = "Bob"; // ошибка

            // records поддерживают инициализацию с помощью оператора with
            // Он позволяет создать одну record на основе другой
            var tom = new MyRecordClass_("Tom", 37);
            var sam = tom with { Name = "Sam" };

            // Если хотим скопировать все св-ва - оставляем пустые скобки
            var joe = tom with { };

            // Использование премуществ кортжей и деконструктора, определенного выше
            var person = new MyRecordClass_("Tom", 37);
            var (personName, personAge) = person;
        }
    }

    class OperatorOverload_
    {
        /*
            Для перегрузки определеним в классе, для объектов которого хотим определить оператор, метод, содержащий:
            - модификаторы public static - будет использоваться всеми объектами класса
            - название возвращаемого типа
            - вместо названия метода идет слово operator и сам оператор
            - в скобках перечисляются параметры - один из них должен представлять класс или структуру, в котором определяется оператор            
              В примере перегруженные операторы проводятся над двумя объектами, поэтому для каждой перегрузки будет два параметра
        */

        int Value { get; set; }

        public static OperatorOverload_ operator + (OperatorOverload_ c1, OperatorOverload_ c2) 
        {
            return new OperatorOverload_
            {
                Value = c1.Value + c2.Value
            };
        }

        public static bool operator > (OperatorOverload_ c1, OperatorOverload_ c2)
        { 
            return c1.Value > c2.Value; 
        }

        public static bool operator < (OperatorOverload_ c1, OperatorOverload_ c2)
        { 
            return c1.Value < c2.Value; 
        }

        static void Main_()
        {
            var c1 = new OperatorOverload_ { Value = 23 };
            var c2 = new OperatorOverload_ { Value = 45 };

            bool result = c1 > c2;
            Console.WriteLine(result);

            var c3 = c1 + c2;
            Console.WriteLine(c3.Value);
        }
    }

    class InterfaceLink_
    {
        // Интерфейсы могут хранить свойства и методы
        interface IMyInterface
        {
            void F1();
        }

        class A : IMyInterface
        {
            public void F1()
            {
                Console.WriteLine("F1");
            }

            public void F2()
            {
                Console.WriteLine("F2");
            }
        }

        static void Main_()
        {
            var objA = new A();

            IMyInterface inter = objA;

            inter.F1();

            //inter.F2(); // Ошибка
        }
    }

    class IEnumerable_
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

    class IQueryable_
    {        
        /*
            Работает с коллекциями, которые хранятся в БД
            Строит запросы, которые будут выполнены на стороне БД и вернут только нужные данные
            Улучшает производительность при работе с большими коллекциями
            Преобразуется в SQL с WHERE - сразу отфильтровывает на сервере:

                IQueryable<Phone> phoneIQuer = db.Phones;
                var phones = phoneIQuer.Where(p => p.Id > id).ToList();
                SELECT Id, Name FROM dbo.Phones WHERE Id > 3
        */

        class Product
        {
            public int Price { get; set; }

            public string? Name { get; set; }
        }

        class MyDbContext : IDisposable
        {
            public IQueryable<Product>? Products { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public void F2()
        {
            using (var context = new MyDbContext())
            {
                IQueryable<Product> products = context.Products.Where(p => p.Price > 10);

                foreach (Product product in products)
                {
                    Console.WriteLine(product.Name);
                }
            }
        }
    }

    class IEnumerator_
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

    class SOLID_
    {
        // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял одну задачу
        // Open Closed              методы класса должны быть открыты для расширения, но закрыты для изменения
        // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
        // Interface Segregation    не создавать интерфейсы с большим числом методов
        // Dependency Invertion     зависимости кода должны строиться от абстракции

        #region Liskov Substitution

            // Класс FrozenDeposit нарушает принцип подстановки Лисков
            // Объекты подкласса должны быть заменяемыми объектами базового класса без изменения желаемых свойств программы

            public abstract class Deposit
            {
                public decimal Balance { get; protected set; }

                public virtual void Credit(decimal amount)
                {
                    Balance += amount;
                }
            }

            public class FrozenDeposit : Deposit
            {
                public override void Credit(decimal amount)
                {
                    throw new Exception("This deposit does not support filling up");
                }
            }

        #endregion

        #region DependencyInvertion

            interface IDependencyInvertion
            {
                void F1();
            }

            class A : IDependencyInvertion
            {
                public void F1() { Console.WriteLine("A"); }
            }

            class B : IDependencyInvertion
            {
                public void F1() { Console.WriteLine("B"); }
            }

            class C
            {
                private readonly IDependencyInvertion _di;

                public C(IDependencyInvertion di)
                {
                    _di = di;
                }

                public void F2() { _di.F1(); }
            }

            static void Main_()
            {
                IDependencyInvertion dependencyInvertion = new A(); // A меняем на B при необходимости
                dependencyInvertion.F1();

                C c = new C(dependencyInvertion);
                c.F2();
            }

        #endregion
    }

    class AbstractAndStaticClass_
    {
        // Статический класс:
        // - нужен для группировки логически связанных членов
        // - от него нельзя наследоваться
        // - не может реализовывать интерфейс
        static class A
        {
            static public void F1()
            {
                Console.WriteLine("A3");
            }
        }

        // Абстрактный класс может иметь
        // - переменные
        // - абстрактные | методы
        // - абстрактные | свойства
        // - конструкторы
        // - индексаторы
        // - события

        // Абстрактные методы и св-ва:
        // - могут быть лишь в абстрактных классах
        // - не могут иметь тело
        // - должны быть переопределены наследником

        // Абстрактные члены не должны иметь модификатор private
        // Исключение - если дочерний класс тоже абстрактный

        abstract class B
        {
            public abstract string Name { get; set; }

            public void F2()
            {
                Console.WriteLine("B3");
            }

            public abstract void F3();
        }

        class ProgramAbstractAndStaticClass : B
        {
            string name = "";

            public override string Name
            {
                get { return "Mr/Ms. " + name; }

                set { name = value; }
            }

            static void Main_()
            {
                A.F1();

                B objB = new ProgramAbstractAndStaticClass();

                objB.F2();

                objB.F3();
            }

            public override void F3()
            {
                Console.WriteLine("Program");
            }
        }
    }

    class MultipleInheritance_
    {
        // Множественное наследование запрещено, но его можно реализовать через интерфейсы
        interface IInterface
        {
            void F2();
        }

        class A
        {
            public void F1()
            {
                Console.WriteLine("F1");
            }
        }

        class B : IInterface
        {
            public void F2()
            {
                Console.WriteLine("F2");
            }
        }

        class C : A, IInterface
        {
            B objB = new B();

            public void F2()
            {
                objB.F2();
            }
        }

        static void Main_()
        {
            var objC = new C();

            objC.F1();

            objC.F2();
        }
    }

    class Polymorphism_
    {
        class BaseClass
        {
            // virtual
            public virtual void F1()
            {
                Console.WriteLine("Вызов базового класса");
            }
        }

        class DerivedClass_Override : BaseClass
        {
            // override
            public override void F1()
            {
                Console.WriteLine("Вызов дочернего класса");
            }
        }
                
        class DerivedClass_New : BaseClass
        {
            // new
            public new void F1()
            {
                Console.WriteLine("Вызов дочернего класса");
            }
        }

        static void Main_()
        {
            BaseClass obj1 = new BaseClass();
            obj1.F1();  // Вызов базового класса



            DerivedClass_Override obj2 = new DerivedClass_Override();
            obj2.F1();  // Вызов дочернего класса

            BaseClass obj3 = new DerivedClass_Override();
            obj3.F1();  // Вызов дочернего класса



            DerivedClass_New obj4 = new DerivedClass_New();
            obj4.F1();  // Вызов дочернего класса

            BaseClass obj5 = new DerivedClass_New();
            obj5.F1();  // Вызов базового класса
        }
    }

    class Generics_
    {
        class A<T>
        {
            public T x;

            public T param { get; set; }

            public A()
            {
            }

            public A(T _x)
            {
                x = _x;
            }
        }

        class B<T, Z>
        {
            public T x;

            public Z y;

            public T param1 { get; set; }

            public Z param2 { get; set; }

            public B(T _x, Z _y)
            {
                x = _x;
                y = _y;
            }
        }

        class C<T> : A<T>
        {
            T x;
            public C(T _x) : base(_x)
            {
                x = _x;
            }
        }

        // where означает, что тип T может быть только типа C, либо быть любым его наследником
        // where T : new() - у класса должен быть публичный конструктор без параметров
        class D<T, Z> where T : A<int>
                      where Z : new()
        {
            public T x;

            public Z y;

            public T param1 { get; set; }

            public Z param2 { get; set; }

            public D(T _x, Z _y)
            {
                x = _x;
                y = _y;
            }

            public T F1()
            {
                return x; // шаблонный тип можно возвращать
            }
        }

        delegate R Del<R, T>(T val);

        static int Displ(int val)
        {
            Console.WriteLine(val);

            return 0;
        }

        static void Main_()
        {
            var del = new Del<int, int>(Displ);
            del(1);

            var a1 = new A<int>(5);
            var a2 = new B<bool, string>(true, "false");
        }
    }

    class Yield_
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

    class Cortege_
    {
        static (int, int) GetValues()
        {
            var result = (1, 3);
            return result;
        }

        static (int number, string name, int year) F2()
        {
            return (1, "Dima", 1990);
        }

        static void Main_()
        {
            var tuple = (count: 5, sum: 10);
            Console.WriteLine(tuple.count + " " + tuple.sum);

            tuple = GetValues();
            Console.WriteLine(tuple.count + " " + tuple.sum);

            var tuple2 = F2();
            Console.WriteLine(tuple2.number + tuple2.name + tuple2.year);

            var tupleDictionary = new Dictionary<(int, int), string>
            {
                { (1, 2), "string1" },
                { (3, 4), "string2" }
            };

            var result = tupleDictionary[(1, 2)];
            Console.WriteLine(result);
        }
    }

    class LINQ_
    {
        // В LINQ оператор Select проецирует каждый элемент в новую форму
        // Он используется для преобразования элементов последовательности,
        // например, для выбора определённых св-в объектов или создания новых объектов на основе существующих

        class A
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        static void Main_()
        {
            IEnumerable<A> collA = new List<A>()
            {
                new A { Id = 2, Name = "Dima" },
                new A { Id = 4, Name = "Champion" },
                new A { Id = 1, Name = "Developer" },
            };

            // Запрос не выполнится, пока не будет вызвана ф-я FirstOrDefault(), ToList() и т.п.
            var query1 = collA.Where(z => z.Name.StartsWith('D')).OrderBy(z => z.Id);

            var query2 = from i in collA
                         where i.Id > 1
                         orderby i.Name
                         select i;

            foreach (var i in query2)
            {
                Console.WriteLine(i.Id + " " + i.Name);
            }
        }
    }

    class MemoryClean_
    {
        /*
            Сборщик мусора очищает пространство, занимаемое ссылочными типами - управляемыми объектами
            Есть неуправляемые - соединение с БД, открытие файлов на чтение/запись,
            они относятся к внешним объектам, которыми CLR не может управлять

            Освобождение неуправляемых ресурсов выполняют:
            - финализатор
            - реализация интерфейса IDisposable в блоках try/finally или using

            Финализатор вызывается перед сборкой мусора
            Программа может завершиться до того, как произойдет сборка мусора, поэтому финализатор может быть не вызван
            или будет вызван, но не успеет полностью отработать

            Во время сборки мусора выполнение всех потоков приостанавливается

            ОЧЕРЕДЬ ФИНАЛИЗАЦИИ
            Гарантирует, что финализаторы (методы Finalize) объектов будут вызваны, когда эти объекты становятся недоступными
            Можно определить финализатор для класса, переопределив метод Finalize
            Финализатор позволяет очистить ресурсы перед уничтожением объектов сборщиком мусора
            Когда на объект больше нет ссылок, он помещается в очередь финализации, если у него есть финализатор
            Сборщик мусора не сразу освобождает память, занятую этим объектом, а сначала вызывает его финализатор
            Объекты с финализаторами попадают в очередь финализации
            Сборщик мусора периодически проверяет эту очередь и вызывает финализаторы для объектов в ней
            После вызова финализатора объект удаляется из памяти
            Использование финализаторов может повлиять на производительность приложения,
            так как объекты с финализаторами требуют дополнительных затрат на обработку — они остаются в памяти дольше, чем объекты без финализаторов
            Это может привести к задержкам в сборке мусора
            В большинстве случаев рекомендуется избегать использования финализаторов
            Вместо этого лучше реализовать интерфейс IDisposable и использовать метод Dispose
        */

        class A
        {
            // Финализатор, не рекомендуется использовать
            ~A()
            {
                Console.WriteLine("Destructor");
            }
        }

        // IDisposable объявляет единственный метод Dispose,
        // освобождающий неуправляемые ресурсы - вызывает финализатор немедленно
        public class B : IDisposable
        {
            public void Dispose()
            {
                Console.WriteLine("Dispose");
            }
        }

        // try finally гарантирует, что в случае исключения Dispose освободит ресурсы
        // Можно использовать using, вызывающий Dispose неявно
        static void Main_()
        {
            B objB = default;

            try
            {
                objB = new B();
            }
            finally
            {
                if (objB is not null)
                {
                    objB.Dispose();
                }
            }
        }
    }

    class Exceptions_
    {
        // Исключение не обязательно должно быть обработано в классе, где оно произошло
        // Можно создать класс для обработки определенных исключений

        // throw    возвращает весь стек вызовов
        // throw ex обрезает стек

        class ExceptionHandler
        {
            public static void Handle(Exception e)
            {
                if (e.GetBaseException().GetType() == typeof(ArgumentException))
                {
                    Console.WriteLine("You caught ArgumentException");
                }
                else
                {
                    throw e;
                }
            }
        }

        static class ExceptionThrower
        {
            public static void TriggerException(bool isTrigger)
            {
                throw new ArgumentException();
            }
        }

        static void Main_()
        {
            try // Программа выведет 1 2 3 4 6
            {
                try // Сначала выполнятся try catch finally данного уровня, даже если возникнет исключение
                {
                    Console.WriteLine("1");
                    throw new NullReferenceException();
                }
                catch
                {
                    Console.WriteLine("2");
                    throw;  // Если убрать эту строку, программа выведет 1 2 3 6
                            // Здесь throw вверх то-же исключение, что было поймано выше - NullReferenceException
                            // throw без аргументов можно вызвать только из блока catch
                }
                // Вызовется в любом случае
                finally
                {
                    Console.WriteLine("3");
                }
            }
            // Если в try произошло исключение, то вызовется соответствующий блок catch
            catch (NullReferenceException ex)
            {
                Console.WriteLine("4");
            }
            catch (Exception ex)
            {
                Console.WriteLine("5");
            }
            finally // Вызовется в любом случае
            {
                Console.WriteLine("6");
                throw new NullReferenceException(); // Необработанное исключение
            }
        }
    }

    class Delegates_
    {
        // На делегаты можно подписать один и более методов, они вызовутся при вызове делегата
        // Их можно суммировать
        // Сигнатуры делегата и его методов должны совпадать (количество и типы аргументов)
        // Если на делегат подписано несколько методов, возвращающих значение, то через делегат получим значение последнего метода

        delegate void Del1();
        delegate int Del2(int a, int b);

        // Action
        // Принимает 0 .. 16 аргументов
        // Ничего не возвращает

        #region Пример Action

            void PrintMessage_1(string message)
            {
                Console.WriteLine(message);
            }

            void PrintMessage_2(string message)
            {
                Console.WriteLine(message);
            }

            void UseAction()
            {
                Action<string> delAction = PrintMessage_1;
                delAction += PrintMessage_2;
                delAction("111");  
            }

        #endregion
        
        // Predicate
        // Принимает один аргумент
        // Возвращает bool

        #region Пример Predicate

            bool IsEven(int number)
            {
                return number % 2 == 0;
            }

            void UsePredicate()
            {
                Predicate<int> isEven = IsEven;
                bool result = isEven(4); // true
            }

        #endregion

        // Func
        // Принимает 0 .. 16 аргументов
        // Возвращает значение

        #region Пример Func

            int Add(int a, int b)
            {
                return a + b;
            }

            void UseFunc()
            {
                Func<int, int, int> add = Add;
                int sum = add(3, 4); // 7
            }

        #endregion
    }

    class Events_
    {
        /*
            Позволяют объектам уведомлять другие объекты, что что-то произошло
            События основаны на делегатах и предоставляют способ подписки на уведомления
            Когда событие происходит, оно вызывает все подрисанные на него методы

            1. Делегат: Определяет сигнатуру метода, который будет вызываться при возникновении события

            2. Событие: Объявляется с использованием делегата и используется для вызова методов подписчиков

            3. Подписчик: Метод, который подписывается на событие и будет вызван, когда событие произойдет


            Пример:
            1. Определяем делегат ClickEventHandler, который принимает два параметра: отправитель события и объект EventArgs
            2. На основе делегата объявляем событие Clicked
            3. Метод OnClick генерирует событие Clicked, если на него есть подписчики
            4. В методе Main создаем экземпляр Button, подписываемся на событие Clicked и указываем метод-обработчик Button_Clicked
            5. Метод Button_Clicked вызывается, когда происходит событие Clicked
        */

        public class Button
        {
            public delegate void ClickEventHandler(object sender, EventArgs e);

            public event ClickEventHandler? Clicked;

            public void OnClick()
            {
                // Проверяем, есть ли подписчики на событие
                if (Clicked is not null)
                {
                    // Генерируем событие
                    Clicked(this, EventArgs.Empty);
                }
            }
        }

        public class Program
        {
            static void Main_()
            {
                var button = new Button();

                // Подписка на событие
                button.Clicked += Button_Clicked;

                // Имитация нажатия кнопки
                button.OnClick();
            }

            private static void Button_Clicked(object sender, EventArgs e)
            {
                Console.WriteLine("Кнопка была нажата!");
            }
        }
    }

    class BearerToken_
    {
        static void Main_()
        {
            var randomNumber = new Random(42);
            var oneTimeKey = new byte[32];
            var tokenHandler = new JwtSecurityTokenHandler();
            randomNumber.NextBytes(oneTimeKey);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(oneTimeKey),
                SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateJwtSecurityToken(
                issuer:             "wx_api",
                audience:           "wx_b2c_subscription",
                subject:            new ClaimsIdentity(),
                notBefore:          DateTime.UtcNow,
                expires:            DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials);

            token.Payload["OwnerId"] = new Guid("FCD2EFC9-518E-419F-A1AA-61E220932A98");
            var bearerToker = "Bearer " + tokenHandler.WriteToken(token);
        }
    }

    class Type_
    {
        /*
            Можно узнать имя типа, его пространство имен, базовый тип
            С помощью метода Activator.CreateInstance можно создавать экземпляры типов динамически
            Можно получать информацию об атрибутах, примененных к типу

            Класс Type предоставляет информацию о типах данных, таких как:
            - классы
            - структуры
            - интерфейсы
            - перечисления
            - массивы

            Позволяет получить информацию о:
            - свойствах
            - методах
            - конструкторах
            - других членах типа
            - его атрибутах и базовых типах            
        */

        // Выведм информацию о классе Program, включая его методы и созданный экземпляр
        static void Main_()
        {
            // Используем typeof(Program), чтобы получить объект Type, представляющий класс Program
            Type type = typeof(Program);

            // Выводим информацию о типе
            Console.WriteLine($"Имя типа:               {type.Name}");
            Console.WriteLine($"Полное имя типа:        {type.FullName}");
            Console.WriteLine($"Пространство имен:      {type.Namespace}");
            Console.WriteLine($"Базовый тип:            {type.BaseType}");

            // Создание экземпляра класса динамически
            var instance = Activator.CreateInstance(type);

            Console.WriteLine($"Создан экземпляр типа:  {instance?.GetType().Name}");

            // Получим все методы класса Program и выведем их имена
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                Console.WriteLine(method.Name);
            }
        }
    }

    class IsAsTypeof_
    {
        // is
        // Проверяет, является-ли объект экземпляром указанного типа или его производным
        // Возвращает true/false

        // as
        // Преобразует объект к указанному типу или его производному:
        // - если объект может быть преобразован к указанному типу, возвращается ссылкы на преобразованный объект
        // - иначе возвращает null

        class A { }

        class B : A { }

        class C { }

        static void UseIs()
        {
            var objA = new A();
            var objB = new B();
            var objC = new C();

            if (objA is A)      { /* true */ }

            if (objB is A)      { /* true */ }

            if (objA is object) { /* true */ }

            if (objA is B)      { /* ERROR */ }

            if (objC is A)      { /* ERROR */ }
        }

        static void UseAs()
        {
            var objA = new A();
            var objB = new B();

            if (objA is B)
            {
                objB = (B)objA;
            }
            else
            {
                Console.WriteLine("Error");
            }

            // as делает описанный выше код в один шаг
            objB = objA as B;

            if (objB is not null)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        static void UseTypeof()
        {
            // Type содержит свойства и методы, для получения информации о типе
            Type type = typeof(StreamReader);
            Console.WriteLine(type.FullName);

            if (type.IsClass)
            {
                Console.WriteLine("Is class");
            }
            if (type.IsAbstract)
            {
                Console.WriteLine("Is abstract class");
            }
            else
            {
                Console.WriteLine("Is concrete class");
            }
        }

        static void Main_()
        {
            UseIs();
            UseAs();
            UseTypeof();
        }
    }

    class Equals_
    {
        /*
            Позволяет определить, равны-ли два объекта по содержимому, а не по адресу в памяти
            По умолчанию сравнивает ссылки объектов
            Но можно переопределить, чтобы сравнивать объекты по значению их полей
        */

        public record RecordA(int Value, string Value2);

        public class Person
        {
            // Оба числа 1 приводятся к типу object, и сравниваются как ссылки
            // false
            bool a = (object)1 == (object)1;

            // Метод Equals сравнивает значения, а не ссылки
            // true
            bool b = 1.Equals(1);

            // Благодаря интернированию строк, это один и тот-же участок в памяти
            // true
            bool c = (object)"1" == (object)"1";

            // Для record метод Equals переопределен и сравнивает значения всех свойств, а не ссылки
            // true
            bool d = new RecordA(1, "TEST").Equals(new RecordA(1, "TEST"));



            public string? Name { get; set; }

            public int Age { get; set; }

            public override bool Equals(object obj)
            {
                // Проверяем, что объект не равен null и является экземпляром Person
                if (obj is Person other)
                {
                    return this.Name == other.Name && this.Age == other.Age;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }

        public class Program
        {
            static void Main_()
            {
                var person1 = new Person { Name = "Alice", Age = 30 };
                var person2 = new Person { Name = "Alice", Age = 30 };
                var person3 = new Person { Name = "Bob", Age = 25 };

                Console.WriteLine(person1.Equals(person2)); // true
                Console.WriteLine(person1.Equals(person3)); // false
            }
        }
    }

    class LazyLoading_
    {
        /*
            Используется для оптимизации производительности при работе с большими данными
            Объекты и данные загружаются только по мере необходимости, что снижает время загрузки
            Если объект не используется, он не будет загружен в память
            Можно избежать ненужной инициализации объектов, что упрощает управление зависимостями
        */

        public class DataLoader
        {
            // Используем Lazy<string> для хранения данных
            private Lazy<string> _data;

            public string Data => _data.Value;

            public DataLoader()
            {
                // Объект Lazy<string> инициализируется функцией, загружающей данные
                _data = new Lazy<string>(() => LoadData());
            }

            private string LoadData()
            {
                Console.WriteLine("Загрузка данных...");

                // Симуляция долгой операции чтения из БД
                Thread.Sleep(2000);

                return "Данные загружены";
            }

            static void Main_()
            {
                // Создаем экземпляр DataLoader, но данные еще не загружаются до первого обращения к св-ву Data
                var dataLoader = new DataLoader();

                Console.WriteLine("Объект DataLoader создан.");

                Console.WriteLine("Данные еще не загружены.");

                // При первом обращении к св - ву Data вызывается _data.Value, что приводит к выполнению функции загрузки данных
                // Если данные уже были загружены, св-во возвращает уже загруженные данные без повторной загрузки
                Console.WriteLine(dataLoader.Data);

                // Повторный доступ к данным не вызывает повторной загрузки
                Console.WriteLine(dataLoader.Data);
            }
        }
    }

    class Transaction_
    {
        /*
            public bool F1()
            {
                using (var transaction = _dbContext.GetDatabase().BeginTransaction())
                {
                    try
                    {
                        //...

                        bool result = _dbContext.SaveChanges() > 0;
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Категория id={id} не была : {e}");
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        */
    }

    class FluentValidation_
    {
        public void F1()
        {
            /*
                RuleFor(ps => ps.IncludePosMonth).InclusiveBetween(1, 12)
                    .WithMessage("Количество месяцев больще должно быть пустым или в диапазоне 1-12");

                RuleFor(step => step)
                    .Must(step => !string.IsNullOrWhiteSpace(step.Header))
                    .WithMessage("Поле обязательное для заполнения: \"Заголовок\"");

                RuleFor(category => category)
                    .Must(category => category?.Id != 0)
                    .WithMessage("Поле обязательное для заполнения: \"Id\"");

                RuleFor(category => category)
                    .Must(category => category?.CategoryForAdd?.LoyaltyId != 0)
                    .WithMessage("Поле обязательное для заполнения: \"ID\"");

                RuleFor(promotion => promotion)
                    .Must(promotion => promotion.ClientsFile is not null && promotion.Id.HasValue)
                    .When(promotion => promotion.ClientSegmentations is not null)
                    .WithMessage("Необходимо прикрепить файл с клиентами");
            */
        }
    }

    class HttpClient_
    {
        // Получение данных с веб-сайта через HttpClient
        static async Task Main_()
        {
            var httpClient = new HttpClient();

            var message = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://google.ru/")
            };

            HttpResponseMessage result = httpClient.Send(message);

            var data = await result.Content.ReadAsStringAsync();
        }
    }

    class SQLInjection
    {
        /*
            Код имеет уязвимость к SQL-инъекциям, потому что значение dealId
            вставляется непосредственно в строку SQL-запроса без какой-либо обработки или параметризации

            Для защиты от SQL-инъекций следует использовать параметризованные запросы


            private async Task<Client> F1_Error(string dealId)
            {
                using var connection = _connectionFactory.Create();

                // Неправильно
                //var selectCommandText = @"SELECT c.FirstName, c.SecondName, c.*
                //                          FROM clients as c   
                //                          INNER JOIN client_deal as cd on cd.ClientId = c.Id   
                //                          WHERE cd.DealId = @DealId";

                var selectCommandText = $@"SELECT c.FirstName, c.SecondName, c.*
                                           FROM clients as c   
                                           INNER JOIN client_deal as cd on cd.ClientId = c.Id   
                                           WHERE cd.DealId = {dealId}";

                var selectSqlCommand = new SqlCommand(selectCommandText, connection);

                // Неправильно
                //var result = await connection.ExecuteAsync(readQuery);

                // Правильно
                selectSqlCommand.Parameters.AddWithValue("@DealId", dealId);
                var result = await connection.ExecuteAsync(selectSqlCommand);

                return result;
            }
        */
    }
}

static class ExtensionMethods_
{
    // Имеют слово this перед первым аргументом
    // Могут быть только статическими и только в статических классах
    // Запрещаются параметры по умолчанию
    // Можно перегружать
    // Первый аргумент не может быть помечен словами ref, out, остальные могут
    // Не имеют доступ к private и protected полям расширяемого класса

    static void Extention(this string value) 
    { 
        Console.WriteLine(value); 
    }

    static void Extention(this string value1, string value2) 
    { 
        Console.WriteLine(value1 + value2); 
    }

    class ProgramExtensionMethods
    {
        static void Main_()
        {
            string text = "1";

            text.Extention();

            text.Extention("2");

            "1".Extention("2");
        }
    }
}
