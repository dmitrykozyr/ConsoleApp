using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SharpEdu;

class Program
{
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

        Double[,]  myDoubles = new Double[10, 20];
        String[,,] myStrings = new String[5, 3, 10];

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
                // что увеличивает расход памяти, но поддерживается многопоточное чтение
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
                // Последняя еденица не добавится, т.к. она уже есть в коллекции
                // Будет 2 3 1
                var hashSet = new HashSet<int>();
                hashSet.Add(2);
                hashSet.Add(3);
                hashSet.Add(1);
                hashSet.Add(1);

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
                var sortedList = new SortedList()
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
                stack.Pop();       // Удалить элемент
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
                Это интерфейс, определяющий стандартные операции для работы с коллекциями:
                - добавление
                - удаление
                - доступ к элементам по индексу

                Определяет только базовые методы,
                - Add
                - Remove
                - Insert
                - IndexOf
                - и свойства Count, Item[index]

                Используется, вы хотим создать метод или класс, который может работать с различными типами коллекций

            List
                Это конкретная реализация интерфейса IList
                Представляет собой изменяемый массив, предоставляющий возможность хранить элементы и управлять ими
            
                Включает в себя дополнительные методы
                - Sort
                - Reverse
                - и другие

                Используется, если нужно работать с конкретной реализацией коллекции и использовать ее специфические методы
        */

        // Пример
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
                public TreeData[] Child { get; set; }
            }

            static void ShowLeafIds(TreeData root)
            {
                if (root == null)
                {
                    return;
                }

                if (root.Child == null || root.Child.Length == 0)
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

            static void Main()
            {
                // Инициализация дерева
                /*
                            1
                        2       3
                      4   5   6
                */

                TreeData data = new TreeData
                {
                    Id = 1,
                    Child = new TreeData[]
                    {
                        new TreeData
                        {
                            Id = 2,
                            Child = new TreeData[]
                            {
                                new TreeData { Id = 4 },
                                new TreeData { Id = 5 }
                            }
                        },
                        new TreeData
                        {
                            Id = 3,
                            Child = new TreeData[]
                            {
                                new TreeData { Id = 6 }
                            }
                        }
                    }
                };

                ShowLeafIds(data); // 4 5 6
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
            public string Name { get; init; } // init делает св-во неизменяемым
                                              // set тоже можно использовать, но тогда св-во будет изменяемым
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
            // 1
            // В record св-во с init нельзя изменять
            var myRecordClass_ = new MyRecordClass_("Tom", 37);
            //myRecordClass_.Name = "Bob"; // ошибка

            // 2
            // В отличие от классов, в record идет сравнение по значению
            var class1 = new UserClass("Tom");
            var class2 = new UserClass("Tom");
            Console.WriteLine(class1.Equals(class2));   // false

            var record1 = new UserRecord("Tom");
            var record2 = new UserRecord("Tom");
            Console.WriteLine(record1.Equals(record2)); // true

            // 3
            // records поддерживают инициализацию с помощью оператора with
            // Он позволяет создать одну record на основе другой
            var tom = new MyRecordClass_("Tom", 37);
            var sam = tom with { Name = "Sam" };
            var joe = tom with { };                     // Если хотим скопировать все св-ва - оставляем пустые скобки
            Console.WriteLine($"{sam.Name} - {sam.Age}"); // Sam - 37

            // 4
            // Использование премуществ кортжей и деконструктора, определенного выше
            var person = new MyRecordClass_("Tom", 37);
            Console.WriteLine(person.Name);         // Tom
            var (personName, personAge) = person;
            Console.WriteLine(personAge);           // 37
            Console.WriteLine(personName);          // Tom
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

    class IEnumerable_IQueryable_
    {
        /*
            IEnumerable
            
                Можно перебирать поочередно

                Вытаскивает данные из коллекции в память и затем выполняет запрос на них
                Может привести к проблемам производительности при работе с большими коллекциями

                Преобразуется в SQL без WHERE - отбирается вся коллекция, а потом фильтруется на клиенте:

                    IEnumerable<Phone> phoneIEnum = db.Phones;
                    var phones = phoneIEnum.Where(p => p.Id > id).ToList();
                    SELECT Id, Name FROM dbo.Phones
        */

        public void F1()
            {
                var numbers = new List<int> { 1, 2, 3, 4, 5 };

                IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);

                foreach (int number in evenNumbers)
                {
                    Console.WriteLine(number);
                }
            }

        /*
            IQueryable

                Можно запросить у БД и получить только нужные данные

                Работает с коллекциями, которые хранятся в БД
                Cтроит запросы, которые будут выполнены на стороне БД и вернут только нужные данные
                Улучшает производительность при работе с большими коллекциями

                Преобразуется в SQL с WHERE - сразу отфильтровывает на сервере:

                    IQueryable<Phone> phoneIQuer = db.Phones;
                    var phones = phoneIQuer.Where(p => p.Id > id).ToList();
                    SELECT Id, Name FROM dbo.Phones WHERE Id > 3
        */

            class MyDbContext : IDisposable
            {
                public IQueryable<Product>? Products { get; set; }

                public void Dispose()
                {
                    throw new NotImplementedException();
                }
            }

            class Product
            {
                public int Price { get; set; }

                public string? Name { get; set; }
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

    class SOLID_
    {
        // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял одну задачу
        // Open Closed              методы класса должны быть открыты для расширения, но закрыты для изменения
        // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
        // Interface Segregation    не создавать интерфейсы с большим числом методов
        // Dependency Invertion     зависимости кода должны строиться от абстракции

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
        // Итератор возвращает все члены коллекции
        // Нужен для сокрытия коллекции и способа ее обхода, ведь разные коллекции обходятся по разному
        class A : IEnumerable
        {
            int[] arr = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

            public IEnumerator GetEnumerator()
            {
                foreach (var i in arr)
                {
                    // yield возвращает элементы большой коллекции поэлементно
                    yield return arr[i];
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
            Большинство объектов относятся к управляемым и очищаются сборщиком мусора,
            но есть неуправляемые

            Освобождение неуправляемых ресурсов выполняет
            - Финализатор
            - Реализация интерфейса IDisposable в блоках try/finally или using

            Финализатор вызывается перед сборкой мусора
            Программа может завершиться до того, как произойдет сборка мусора,
            поэтому финализатор может быть не вызван
            Или будет вызван, но не успеет полностью отработать

            Во время сборки мусора выполнение всех потоков приостанавливается
        */
        class A
        {
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
            B objB = null;

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
                            // Здесь throw перебрасывает исключение, пойманное в catch так, как будто бы catch его не поймал,
                            // поэтому бросается то же исключение, что было поймано выше - NullReferenceException
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

            try
            {
                ExceptionThrower.TriggerException(true);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }
        }
    }

    class Delegates_
    {
        // На делегаты можно подписать один и более методов, они вызовутся при вызове делегата
        // Делегаты можно суммировать
        // Сигнатуры делегата и его методов должны совпадать (количество и типы аргументов)
        // Если на делегат подписано несколько методов, возвращающих значение, то через делегат получим значение последнего метода
        delegate void Del();

        // Анонимные методы позволяют присвоить делегату метод, не обЪявляя его
        delegate int Del2(int a, int b);

        // Лямбда-операторы - в них не нужно указывать тип, эта информация есть внутри делегата
        // Короткая запись анонимного метода, присваемого экземпляру класса-делегата
        delegate int Del3(int val);

        delegate void Del4();

        Action action = F1;         // ничего не возвращает
        Predicate<int> predicate;   // принимает минимум один аргумент
        Func<int, string> func;     // принимает от 1 до 16 аргументов и возвращает bool значение

        static class A
        {
            public static void Display1()
            {
                Console.WriteLine("1");
            }

            public static void Display2()
            {
                Console.WriteLine("2");
            }
        }

        static void F1()
        {
            Console.WriteLine("F1");
        }

        static void F2()
        {
            Console.WriteLine("F2");
        }

        static void Main_()
        {
            var del_1 = new Del(A.Display1);
            var del_2 = new Del(A.Display2);
            var del_3 = del_1 + del_2;
            del_3(); // 1 2

            // Создаем экземпляр делегата и сообщаем ему анонимный метод
            Del2 del2 = delegate (int a, int b)
            {
                return a + b;
            };

            Console.WriteLine(del2(1, 2));

            Del3 del3; // 3 варианта одного и того-же:
            del3 = delegate (int val)
            {
                Console.WriteLine("0");
                return val * 2;
            };

            del3 = (val) =>
            {
                Console.WriteLine("0");
                return val * 2;
            };

            del3 = val => val * 2;
            Console.WriteLine(del3(5));

            Del4 del4 = F1;
            del4();         // F1
            del4 += F2;
            del4();         // F1 F2
            del4 += F1;
            del4 -= F1;     // удалить последний добавленный метод
        }
    }

    class Events_
    {
        delegate void del();

        // В отличие от делегатов, события более защищены от непреднамеренных изменений,
        // тогда как делегат может быть случайно обнулен, если использовать '=' вместо '+='
        // Событие можно использовать лишь внутри класса, в котором оно определено
        delegate void MyDel();

        event MyDel TankIsEmpty; // Объявляем событие для нашего типа делегата

        // Метод вызывает событие
        void F1()
        {
            TankIsEmpty();
        }

        static void F2()
        {
            Console.WriteLine("1");
        }

        static void F3()
        {
            Console.WriteLine("0");
        }

        class A
        {
            public event del ev = null; // Создаем событие

            // Здесь можно проверять, кто вызывает событие
            public void InvokeEvent()
            {
                ev.Invoke();
            }
        }

        class B
        {
            public del ev = null;
            // add remove
            // Как в свойствах, здесь можно включить дополнительные проверки
            public event del Event
            {
                add 
                {
                    ev += value;
                }
                remove
                {
                    ev -= value;
                }
            }

            public void InvokeEvent()
            {
                ev.Invoke();
            }
        }

        static void Main_()
        {
            var events = new Events_();
            events.TankIsEmpty += F2;   // Подписка на событие
            events.TankIsEmpty += F3;
            events.F1();

            var objA = new A();
                                        // присваиваем событию делегат, на который подписываем метод
            objA.ev += new del(F3);     // смысл ..
            objA.ev += F2;              // .. одинаковый
            objA.InvokeEvent();         // напрямую запрещено вызывать события через objA.ev.Invoke()

            var objB = new B();
            objB.Event += F2;
            objB.Event += F3;
            objB.Event += delegate
            {
                Console.WriteLine("Anonimnyi method");
            };

            // Отписать анонимный метод нельзя, код ниже не сработает
            objB.Event -= delegate
            {
                Console.WriteLine("Anonimnyi method");
            };

            objB.InvokeEvent();
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

    class IsAsTypeof_
    {
        // is используется для проверки, является ли объект экземпляром указанного типа или его производным
        // Возвращает true или false

        // as используется для преобразования объекта к указанному типу или его производному типу
        // Если объект может быть преобразован к указанному типу,
        // то оператор возвращает ссылку на преобразованный объект, иначе возвращает null

        class A { }
        class B : A { }
        class C { }

        static void UseIs()
        {
            // is проверяет совместимость типов
            A objA = new A();
            B objB = new B();
            C objC = new C();

            // Если true - в переменную tmp запишется objA, приведенный к типу A
            if (objA is A tmp)
            {
                Console.WriteLine($"a is A, {tmp}");
            }

            if (objB is A)
            {
                Console.WriteLine("b is A");
            }

            if (objA is B)
            {
                Console.WriteLine("Error");
            }

            if (objA is object)
            {
                Console.WriteLine("a is object");
            }

            if (objC is A)
            {
                Console.WriteLine("Error");
            }
        }

        static void UseAs()
        {
            // as выполняе преобразование типов во время выполнения и не
            // генерирует исключение, если преобразование не удалось
            // Если удалось - возвращается ссылка на тип
            A objA = new A();

            B objB = new B();

            if (objA is B)
            {
                objB = (B)objA;
            }
            else
            {
                Console.WriteLine("Error");
            }

            // В примере выше выполняем проверку и в случае успеха делаем присвоение,
            // as делает это в один шаг
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

    class HashTables_
    {
        /*
            Добавляем в хеш-таблицу слова Андрей, Артур, Борис, Владимир
            Если мы выбрали ключем первую букву слова, то для Бориса и Владимира поиск по ключу прйдет за O(1),
            т.к. они единственне с таким ключем, а для Андрея и Артура поиск займет O(N)
            То есть если есть одинаковые ключи, то для этого ключа создается отдельный список,
            а в случае с Dictionary все будет одно за другим в одном списке и там всегда O(N)
            
            А      | Б     | В         
            ----------------------------
            Андрей | Борис | Владимир  
            Артур  |       |           
        */
    }

    class Equals_
    {
        class A
        {
            int x = 2;
        }

        // Переопределение Equals
        // Если переопределяем Equals, то нужно переопределить и GetHashCode
        class B
        {
            public int x;
            public B(int _x)
            {
                x = _x;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                B b = (B)obj;

                return (x == b.x);
            }

            public override int GetHashCode()
            {
                return x;
            }
        }

        static void Main_()
        {
            // Слева у нас ссылка, а справа значение, на которое ссылается ссылка
            // == отвечает за сравнениие ссылок
            // Equals отвечает за сравнение значений
            object a = "1";
            object b = a;
            Console.WriteLine(a == b);                      // true
            Console.WriteLine(a.Equals(b));                 // true

            object c = "1";
            object d = "1";
            Console.WriteLine(c == d);                      // true
            Console.WriteLine(c.Equals(d));                 // true

            string str1 = "1";
            string str2 = "1";
            Console.WriteLine(string.Equals(str1, str2));   // true
            Console.WriteLine(str1.Equals(str2));           // true

            string str3 = "1";
            string str4 = "2";
            Console.WriteLine(string.Equals(str3, str4));   // false
            Console.WriteLine(str3.Equals(str4));           // false

            // Здесь false, потому что у объектов разные хеш-коды
            var objA_1 = new A();
            var objA_2 = new A();
            Console.WriteLine(objA_1 == objA_2);            // false
            Console.WriteLine(objA_1.Equals(objA_2));       // false
            Console.WriteLine(objA_1.GetHashCode());
            Console.WriteLine(objA_2.GetHashCode());

            // Здесь true, потому что переопределили метод Equals
            // и у обоих объектов в конструктор передается одинаковое значение
            // Оператор == мы не переопределяли, поэтому false
            var objB_1 = new B(1);
            var objB_2 = new B(1);
            Console.WriteLine(objB_1 == objB_2);            // false
            Console.WriteLine(objB_1.Equals(objB_2));       // true
        }
    }

    class WeakReference_
    {
        /*
            Слабые ссылки (weak references) - это ссылки на объекты, которые не увеличивают
            счетчик ссылок на объект и не предотвращают его удаление из памяти сборщиком мусора
            Используются, когда нужно ссылаться на объект, но не нужно сохранять его в памяти, если не используется
            Могут использоваться в кэше для автоматического удаления неиспользуемых объектов
            или в реализации обработчиков событий, чтобы избежать утечек памяти

            В этом примере создаем объект класса `MyObject` и сохраняем его в слабой ссылке `weakRef`
            Затем проверяем, что объект еще существует, используя метод `TryGetTarget`
            После этого удаляем ссылку на объект, вызываем сборщик мусора и снова проверяем,что объект удален
            В конструкторе и деструкторе класса `MyObject` выводятся сообщения о создании и удалении объекта,
            чтобы мы могли убедиться, что объект действительно был удален из памяти

            Результат работы программы:
            A: Object Created
            Main 1: Object Exists
            Main 2: Object Exists

            При завершении программы сборщик мусора вызывает финализатор объекта,
            который выводит сообщение 'A: Object Deleted'
        */

        class A
        {
            public A()
            {
                Console.WriteLine("A: Object Created");
            }

            ~A()
            {
                Console.WriteLine("A: Object Deleted");
            }
        }

        static void Main_()
        {
            // Создаем объект и сохраняем его в слабой ссылке
            var weakRef = new WeakReference<A>(new A());

            // Проверяем, что объект еще существует
            A obj;
            if (weakRef.TryGetTarget(out obj))
            {
                Console.WriteLine("Main 1: Object Exists");
            }
            else
            {
                Console.WriteLine("Main 1: Object Deleted");
            }

            // Удаляем ссылку на объект
            obj = null;

            // Вызываем сборщик мусора для удаления объекта из памяти
            GC.Collect();

            // Проверяем, что объект был удален
            if (weakRef.TryGetTarget(out obj))
            {
                Console.WriteLine("Main 2: Object Exists");
            }
            else
            {
                Console.WriteLine("Main 2: Object Deleted");
            }
        }
    }

    class LazyLoading_
    {
        /*
            Lazy loading - данные загружаются в момент, когда они нужны
            Позволяет уменьшить нагрузку на приложение и ускорить его работу

            Если есть класс с коллекцией объектов, то при использовании LL
            объекты будут загружаться только при первом обращении к ним,
            а не сразу при загрузке класса
            
            Это позволяет избежать загрузки большого количества данных,
            которые могут не понадобиться

            При обращении к свойству, которое содержит отложенно загружаемые данные,
            проверяется наличие данных и идет их загрузка при необходимости
            
            В примере свойство Posts помечено модификатором virtual,
            что позволяет его переопределить в производных классах
        
            В методе get проверяем, были ли посты уже загружены из базы данных, и если нет - загружаем
        */

        public class B { }

        public class A
        {
            private List<B> _listB;

            public virtual List<B> ListB
            {
                get
                {
                    if (_listB == null)
                    {
                        _listB = LoadPostsFromDatabase();
                    }

                    return _listB;
                }
            }

            private List<B> LoadPostsFromDatabase()
            {
                // Загружаем посты из базы данных
                // var reault = _dbContext.Post.GetAll();
                return null;
            }
        }
    }

    class Transaction_
    {
        public void F1()
        {
            /*using (var transaction = _dbContext.GetDatabase().BeginTransaction())
            {
                try
                {
                    //...

                    bool result = _dbContext.SaveChanges() > 0;
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Категория id={id} не была : {e}");
                    transaction.Rollback();
                    throw;
                }
            }*/
        }
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

    class WeakReferences_
    {
        // Сильные и слабые ссылки
        public class A
        {
            public void F1() { }
        }

        public void Main_()
        {
            // Сильная ссылка
            // Здесь присваиваем эеземпляр переменной и он не будет удален сборщиком мусора,
            // пока существует сильная ссылка
            WeakReferences_.A obj = new A();
            obj.F1();

            // Слабая ссылка
            // Создаем экземпляр, но ничему его не присваиваем,
            // а только вызываем метод через него,
            // поэтому будет сразу удален сборщиком мусора
            new A().F1();
        }
        
    }

    class Net_8
    {
        /*
        */
    }

    class CSharp_11
    {
        /*
        */
    }

    class DebugRules
    {
        // Правила отладки
        /*         
            Understand the system:
            Read the manual, read everything in depth, know the fundamentals, know the road map, understand your tools, and look up the details
            
            Make it fail:
            Do it again, start at the beginning, stimulate the failure, don't simulate the failure,
            find the uncontrolled condition that makes it intermittent, record everything and find the signature of intermittent bugs,
            don't trust statistics too much, know that "that" can happen, and never throw away a debugging tool
            
            Quit thinking and look (get data first, don't just do complicated repairs based on guessing):
            See the failure, see the details, build instrumentation in, add instrumentation on, don't be afraid to dive in,
            watch out for Heisenberg, and guess only to focus the search
            
            Divide and conquer:
            Narrow the search with successive approximation, get the range,
            determine which side of the bug you're on, use easy-to-spot test patterns, start with the bad,
            fix the bugs you know about, and fix the noise first
            
            Change one thing at a time:
            Isolate the key factor, grab the brass bar with both hands (understand what's wrong before fixing),
            change one test at a time, compare it with a good one, and determine what you changed since the last time it worked
            
            Keep an audit trail:
            Write down what you did in what order and what happened as a result, understand that any detail could be the important one,
            correlate events, understand that audit trails for design are also good for testing, and write it down
            
            Check the plug:
            Question your assumptions, start at the beginning, and test the tool
            
            Get a fresh view:
            Ask for fresh insights (just explaining the problem to a mannequin may help!), tap expertise,
            listen to the voice of experience, know that help is all around you, don't be proud,
            report symptoms (not theories), and realize that you don't have to be sure
            
            If you didn't fix it, it ain't fixed:
            Check that it's really fixed, check that it's really your fix that fixed it,
            know that it never just goes away by itself, fix the cause, and fix the process
        */
    }

    class DDD_
    {
        /*
            Концепции Domain-Driven Design (проектирование, ориентированное на домен) включают:

            1. Абстракцию, отражающую бизнес-логику и правила, должна быть понятной для разработчиков и бизнеса
            2. Предлагает разбивать сложные системы на более мелкие, управляемые части, такие как агрегаты, сущности и значения объектов
            3. Определение границ контекста (Bounded Context), что позволяет четко разделять различные части системы и их модели
            4. Создание агрегатов - групп связанных объектов, которые обрабатываются как единое целое, помогают управлять целостностью данных
            5. События домена используются для уведомления о произошедших изменениях в состоянии модели домена, помогает в реализации реактивных систем
            6. Репозитории - шаблон, который используется для доступа к объектам домена, абстрагируя детали хранения данных
            7. Службы домена - это операции, которые не принадлежат конкретной сущности или агрегату, но имеют смысл в контексте бизнес-логики
            8. Ubiquitous Language (Универсальный язык) - общий язык, используемый разработчиками и бизнесом для обсуждения требований и модели домена
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
