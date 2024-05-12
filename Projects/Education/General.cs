using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SharpEdu
{
    class Program
    {
        class General_
        {
            /*

            const                   инициализация при обьявлении,                    неявно статические, нельзя помечать как static
            readonly                инициализация в конструкторе или при объявлении, неявно статические, можно  помечать как static
            partial                 позволяет разбить класс на несколько частей
            ref                     передача аргумента по ссылке, инициализировать до вызова метода
            out                     можно не инициализировать до вызова метода    
            TCP                     отправляет сообщение, пока не получит подтверждение о доставке или не будет превышено число попыток
            UDP                     не гарантирует доставку, но более быстрый и подходит для широковещательной передачи
            protected               доступ в текущем и производных классах, производные классы могут быть в других сборках
            internal                доступ в той же сборке
            protected internal      доступ в текущей сборке и из производных классов
            private protected       доступ в текущем и производных классах в той же сборке
            
            Если в базовом классе есть вирт метод, то в дочернем его можно переопределить через override,
            либо через new сказать, что нужно использовать метод дочернего класса
            Если sealed применить к классу              - запрещаем наследоваться от класса
            Если sealed применить к override-методу     - запрещаем переопределение
            override можно применять к виртуальным и абстрактным методам
            Стек ограничен по размеру, но быстрее, а куча медленнее, но это почти вся оперативная память
            Дебаг F5 - до следующей точки останова, F10 - на следующий шаг, F11 - зайти внутрь метода
            using SCG = System.Collections.Generic; псевдоним

            Немедленное выполнение - операция выполнися в точке, где объявлен запрос
            Отложенное выполнение  - результат запроса зависит от источника данных в момент выполнения запроса, а не при его определении

            Статические классы могут содержать только статические поля, свойства и методы    
            Статические св-ва/методы хранят состояние/поведение всего класса, а не отдельного объекта, обращение по имени класса
            Статические методы могут обращаться только к статическим членам класса
            Статические конструкторы:
            - не имеют модификаторов доступа и не принимают параметры
            - нельзя использовать слово this для ссылки на текущий объект класса
            - вызовутся автоматически при первом создании объекта класса или первом обращении к статическому члену
            - нужны для инициализации статических данных
            - выполняются один раз

                                                             Разрядность в битах  Диапазон представления чисел
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

            Val: byte, short, int, long, float, double, decimal, bool, char, enum, struct
            Ref: object, string, class, interface, delegate

            Стек имеет размер 2 Мб

            Если переменная представляет значимый тип,
            то в стеке будет храниться непосредсвенное значение
            
            Ссылочные типы хранятся в куче, при создании объекта ссылочного типа
            в стек помещается ссылка на адрес в куче
            
            KISS - Keep It Simple, Stupid
            DRY  - Don’t Repeat Yourself
             
            */

            General_() { }                      // Вызов конструктора по умолчанию
            General_(string data) : this() { }  // this вызывает конструктор по умолчанию,
                                                // base вызывает конструктор родительского класса

            enum MyEnum : int { a = 0, b = 5, c, }
            //Console.WriteLine((int)MyEnum.c);

            Double[,]  myDoubles = new Double[10, 20];
            String[,,] myStrings = new String[5, 3, 10];

            /*
                int? x = null;          Переменная может принмать null
                int y = x ?? 5;         Если левый операнд != null, то вернется он, иначе - правый

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

        class Collections_
        {
            static void Main_()
            {
                // Хранит однотипные объекты
                // Внутри это массив, можно обращаться к элементам по индексу
                var list = new List<int>();
                list.Add(0);
                list.Add(1);

                // Эквивалентно предыдущей записи
                var list2 = new List<int>() { 0, 1 };
                list2[0] = 1;

                // Двусвязный список, где каждый элемент хранит ссылку на следующий и предыдущий элемент
                var numbers = new LinkedList<int>();
                numbers.AddLast(1);
                numbers.AddFirst(2);
                numbers.AddAfter(numbers.Last, 3);

                // В словаре нельзя создать два поля с одинаковым ключем
                // Ключ преобразуется в хеш, по которому поиск идет за O(1)
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "0");
                dictionary.Add(1, "1");

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

                // Хранит уникальную коллекцию в неотсортированном виде
                // Последняя еденица не добавится, т.к. она уже есть в коллекции
                // Будет 2 3 1
                var hashSet = new HashSet<int>();
                hashSet.Add(2);
                hashSet.Add(3);
                hashSet.Add(1);
                hashSet.Add(1);

                // Позволяет хранить разнотипные объекты
                var arrayList = new ArrayList { Capacity = 50 };
                arrayList.Add(1);
                Console.WriteLine(arrayList[0]);
                arrayList.RemoveAt(0);
                arrayList.Reverse();
                arrayList.Remove(4);
                arrayList.Sort();
                arrayList.Clear();

                // Коллекция отсортирована по ключу
                var sortedList = new SortedList()
                {
                    { 0, "0" },
                    { 1, "1" },
                    { 2, "2" },
                };
                sortedList.Add(3, "3");
                sortedList.Clear();

                // Первый пришел - последний ушел
                var stack = new Stack();
                stack.Push("string");
                stack.Push(4);
                stack.Pop();       // Удалить элемент
                stack.Clear();

                // Первый пришел - первый ушел
                var queue = new Queue();
                queue.Enqueue("string");
                queue.Enqueue(5);
                queue.Dequeue(); // Возвращает элемент из начала очереди
                
                /*
                    Конкурентные коллекции могут быть безопасно использованы из нескольких потоков одновременно:
                    - ConcurrentBag<T> - неупорядоченная коллекция
                    - ConcurrentDictionary<TKey, TValue>
                    - ConcurrentQueue<T>
                    - ConcurrentStack<T>
                
                    Неизменяемые коллекции не могут быть изменены после создания
                    Операции добавления удаления и изменения элементов создают новую коллекцию
                    Используются в многопоточных приложениях, где несколько потоков могут пытаться изменить
                    одну и ту же коллекцию одновременно
                    Также используются, для сохранения состояния коллекции на определенный момент времени,
                    например, для реализации отмены операции или для сохранения состояния приложения
                    - ImmutableList<T>
                    - ImmutableArray<T>
                    - ImmutableDictionary<TKey, TValue>
                    - ImmutableHashSet<T>
                    - ImmutableQueue<T>
                    - ImmutableStack<T>
                */
            }

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
                        return;

                    if (root.Child == null || root.Child.Length == 0)
                        Console.WriteLine(root.Id);
                    else
                        foreach (var child in root.Child)
                            ShowLeafIds(child);
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
                // Могут наследоваться только от интерфейса, а их нельзя наследовать
                // Могут быть readonly, но все поля тоже должны быть readonly
                public string name;
                public int age;
                //public string gender = "Male";        // Ошибка - нельзя инициализировать поля при объявлении
                public Structure_(string name, int age) // Если определяем конструктор - он должен инициализировать все поля
                {
                    this.name = name;
                    this.age = age;
                }

                public void DisplayInfo() { Console.WriteLine($"Name: {name} Age: {age}"); }
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
            // Сылочный тип, могут представлять неизменяемый immutable тип
            // Такие типы более безопасны, когда надо гарантировать, что данные объекта не будут изменяться
            // В .NET в принципе уже есть неизменяемые типы, например, String.

            // Есть
            // - record-классы
            // - record-структуры

            // При определении record-класса слово class можно не писать
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
                public void Deconstruct(out string name, out int age) => (name, age) = (Name, Age);
            }

            public record struct MyRecordStruct_
            {
                public string Name { get; init; }
                public MyRecordStruct_(string name) => Name = name;
            }

            public class UserClass
            {
                public string Name { get; init; }
                public UserClass(string name) => Name = name;
            }

            public record UserRecord
            {
                public string Name { get; init; }

                public UserRecord(string name) => Name = name;
            }

            // Как и обыччные классы, record-классы могут наследоваться
            public record Person(string Name, int Age);
            public record Employee(string Name, int Age, string Company) : Person(Name, Age);

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
                Класс представляет счетчик, значение которого хранится в свойстве Value
                Есть два объекта класса - два счетчика, которые хотим сравнивать
                На данный момент операция == и + для объектов класса недоступны
                Это операции для примитивных типов, но не для классов и структур
                Для перегрузки оператора определеним в классе, для объектов которого хотим определить оператор, специальный метод
                Он должен иметь модификаторы public static, так как будет использоваться для всех объектов класса
                Далее идет название возвращаемого типа
                В результате сложения ожидаем получить новый объект класса, сравнения - bool
                Вместо названия метода идет слово operator и сам оператор
                Далее в скобках перечисляются параметры - один из них должен представлять класс или структуру, в котором определяется оператор
                В примере перегруженные операторы проводятся над двумя объектами, поэтому для каждой перегрузки будет по два параметра
            */
            int Value { get; set; }
            public static OperatorOverload_ operator +(OperatorOverload_ c1, OperatorOverload_ c2) { return new OperatorOverload_ { Value = c1.Value + c2.Value }; }
            public static bool              operator >(OperatorOverload_ c1, OperatorOverload_ c2) { return c1.Value > c2.Value; }
            public static bool              operator <(OperatorOverload_ c1, OperatorOverload_ c2) { return c1.Value < c2.Value; }

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
            interface IMyInterface { void F1(); }

            class A : IMyInterface
            {
                public void F1() { Console.WriteLine("F1"); }
                public void F2() { Console.WriteLine("F2"); }
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
                IEnumerable - коллекция, которую можно перебирать поочередно
                IQueryable  - коллекция, которую можно запросить у БД и получить только нужные данные

                IEnumerable работает с коллекциями, которые находятся в памяти, и выполняет запросы на месте,
                т.е. вытаскивает данные из коллекции в память и затем выполняет запрос на них
                Это может привести к проблемам производительности при работе с большими коллекциями

                    List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                    IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);
                    foreach (int number in evenNumbers)
                    {
                        Console.WriteLine(number);
                    }

                IQueryable работает с коллекциями, которые хранятся в БД
                Cтроит запросы, которые будут выполнены на стороне БД и вернут только нужные данные
                Это улучшает производительность при работе с большими коллекциями

                    using (var context = new MyDbContext())
                    {
                        IQueryable<Product> products = context.Products.Where(p => p.Price > 10);
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.Name);
                        }
                    }

                IEnumerable преобразуется в SQL без where - отбирается вся коллекция, а потом фильтруется на клиенте
                    IEnumerable<Phone> phoneIEnum = db.Phones;
                    var phones = phoneIEnum.Where(p => p.Id > id).ToList();
                    SELECT Id, Name FROM dbo.Phones

                IQueryable преобразуется в SQL с where - сразу отфильтровывает на сервере
                    IQueryable<Phone> phoneIQuer = db.Phones;
                    var phones = phoneIQuer.Where(p => p.Id > id).ToList();
                    SELECT Id, Name FROM dbo.Phones WHERE Id > 3
            */
        }

        class SOLID_
        {
            // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял конкретную задачу
            // Open Closed              методы класса должны быть открыты для расширения, но закрыты для модификации
            // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
            // Interface Segregation    не создавать интерфейсы с большим числом методов
            // Dependency Invertion     зависимости кода должны строиться от абстракции
            interface IDependencyInvertion { void F1(); }

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
                public C(IDependencyInvertion di) { _di = di; }
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
            // Статический класс нужен для группировки логически связанных членов
            // От него нельзя наследоваться и он не может реализовывать интерфейс
            static class A
            {
                static public void F1() { Console.WriteLine("A3"); }
            }

            // Абстрактный класс может иметь переменные, абстрактные | методы, конструкторы, абстрактные | свойства, индексаторы, события
            // Абстрактные методы и св-ва могут быть лишь в абстрактных классах, не могут иметь тело и должны быть переопределены наследником
            // Абстрактные члены не должны иметь модификатор private
            // Исключение - если дочерний класс тоже абстрактный
            abstract class B
            {
                public abstract string Name { get; set; }
                public void F2() { Console.WriteLine("B3"); }
                public abstract void F3();
            }

            class ProgramAbstractAndStaticClass : B
            {
                string name;
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

                public override void F3() { Console.WriteLine("Program"); }
            }
        }

        class MultipleInheritance_
        {
            // Множественное наследование запрещено, но его можно реализовать через интерфейсы
            interface IInterface { void F2(); }

            class A
            {
                public void F1() { Console.WriteLine("F1"); }
            }

            class B : IInterface
            {
                public void F2() { Console.WriteLine("F2"); }
            }

            class C : A, IInterface
            {
                B objB = new B();
                public void F2() { objB.F2(); }
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
                public virtual void F()
                {
                    Console.WriteLine("BaseClass");
                }
            }

            class DerivedClass_Override : BaseClass
            {
                public override void F()
                {
                    Console.WriteLine("DerivedClass");
                }
            }

            class DerivedClass_New : BaseClass
            {
                public new void F()
                {
                    Console.WriteLine("DerivedClass");
                }
            }

            static void Main_()
            {
                BaseClass obj1 = new BaseClass();
                obj1.F(); // BaseClass



                DerivedClass_Override obj2 = new DerivedClass_Override();
                obj2.F(); // DerivedClass

                BaseClass obj3 = new DerivedClass_Override();
                obj3.F(); // DeriverClass



                DerivedClass_New obj4 = new DerivedClass_New();
                obj4.F(); // DerivedClass

                BaseClass obj5 = new DerivedClass_New();
                obj5.F(); // BaseClass
            }
        }

        class TypeConversion_
        {
            class Person
            {
                public string Name { get; set; }
                public Person(string name) { Name = name; }
                public void Display() { Console.WriteLine($"Person {Name}"); }
            }

            class Employee : Person
            {
                public string Company { get; set; }
                public Employee(string name, string company) : base(name) { Company = company; }
            }

            class Client : Person
            {
                public string Bank { get; set; }
                public Client(string name, string bank) : base(name) { Bank = bank; }
            }

            static void Main_()
            {
                // ВОСХОДЯЩИЕ ПРЕОБРАЗОВАНИЕ (upcasting)
                // Переменной per типа Person присваивается ссылка на Employee
                // Employee наследуется от Person, поэтому выполняется восходящее преобразование,
                // в итоге employee и person указывают на один объект,
                // но per доступна только та часть, которая представляет функционал Person
                var emp = new Employee("Dima", "Company");
                Person per = emp;
                Console.WriteLine(per.Name);

                // НИСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (downcasting) - от базового класса к производному
                // Чтобы обратиться к функционалу Employee через переменную типа Person,
                // нужно явное преобразование
                var emp2 = new Employee("Tom", "Microsoft");
                Person per2 = emp2;                     // преобразование от Employee к Person
                var emp3 = (Employee)per2;              // преобразование от Person к Employee
                Console.WriteLine(emp3.Company);

                // obj присвоена ссылка на Employee, поэтому можем преобразовать obj к любому типу,
                // который располагается в иерархии классов между object и Employee
                object obj = new Employee("Bill", "Microsoft");
                var emp4 = (Employee)obj;

                object obj2 = new Employee("Bill", "Microsoft");
                ((Person)obj2).Display();               // преобразование к Person для вызова метода Display
                ((Employee)obj2).Display();             // эквивалентно предыдущей записи
                string comp = ((Employee)obj2).Company; // преобразование к Employee, чтобы получить свойство Company

                // СПОСОБЫ ПРЕОБРАЗОВАНИЙ
                // as пытается преобразовать выражение к определенному типу и не выбрасывает исключение
                // В случае неудачи вернет null
                var per7 = new Person("Tom");
                var emp5 = per7 as Employee;
                if (emp == null) { Console.WriteLine("Преобразование прошло неудачно"); }
                else { Console.WriteLine(emp.Company); }

                // is - проверка допустимости преобразования
                // person is Employee проверяет, является ли person объектом типа Employee
                // В данном случае не является, поэтому вернет false
                var per8 = new Person("Tom");
                if (per8 is Employee)
                {
                    var emp6 = (Employee)per8;
                    Console.WriteLine(emp6.Company);
                }
                else { Console.WriteLine("Преобразование недопустимо"); }
            }
        }

        class Generics_
        {
            class A<T>
            {
                public T x;
                public T param { get; set; }
                public A() { }
                public A(T _x) { x = _x; }
            }

            class B<T, Z>
            {
                public T x;
                public Z y;
                public T param1 { get; set; }
                public Z param2 { get; set; }
                public B(T _x, Z _y) { x = _x; y = _y; }
            }

            class C<T> : A<T>
            {
                T x;
                public C(T _x) : base(_x) { x = _x; }
            }

            // where означает, что тип T может быть только типа C, либо быть любым его наследником
            // where T : new() - у класса должен быть публичный конструктор без параметров
            class D<T, Z>
                where T : A<int>
                where Z : new()
            {
                public T x;
                public Z y;
                public T param1 { get; set; }
                public Z param2 { get; set; }
                public D(T _x, Z _y) { x = _x; y = _y; }
                public T F1() { return x; } // шаблонный тип можно возвращать
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

        class Iterator_
        {
            // Итератор возвращает все члены коллекции
            // Нужен для сокрытия коллекции и способа ее обхода, ведь разные коллекции обходятся по разному
            class A : IEnumerable
            {
                int[] arr = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

                public IEnumerator GetEnumerator()
                {
                    foreach (var i in arr)
                        yield return arr[i];
                }
            }

            static void Main_()
            {
                var objA = new A();

                // Цикл автоматически вызовет метод GetEnumerator
                foreach (var i in objA)
                    Console.WriteLine(i);
            }
        }

        class Cortege_
        {
            // Через NuGet установить System.ValueTuple
            // Нужны для возвращения из функции двух и боле значений
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
                var tuple = (5, 10);
                Console.WriteLine(tuple.Item1);
                Console.WriteLine(tuple.Item2);

                tuple.Item1 += 2;
                Console.WriteLine(tuple.Item1);

                // Можно дать названия полям и обращаться по имени а не черезItem1 и Item2
                var tuple2 = (count: 5, sum: 10);
                Console.WriteLine(tuple2.count);
                Console.WriteLine(tuple2.sum);

                tuple2 = GetValues();
                Console.WriteLine(tuple2.count);
                Console.WriteLine(tuple2.sum);

                var tuple3 = F2();
                Console.WriteLine(tuple3.number + tuple3.name + tuple3.year);

                var tupleDictionary = new Dictionary<(int, int), string>();
                tupleDictionary.Add((1, 2), "string1");
                tupleDictionary.Add((3, 4), "string2");
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
                var query1 = collA.Where(z => z.Name.StartsWith("D")).OrderBy(z => z.Id);

                var query2 = from i in collA
                             where i.Id > 1
                             orderby i.Name
                             select i;

                foreach (var i in query2)
                    Console.WriteLine(i.Id + " " + i.Name);
            }
        }

        class Indexator_
        {
            // Свойства с параметрами и без названия, должны иметь минимум один параметр
            // В классе может быть несколько индексаторов, должны отличаться сигнатурой
            class A
            {
                int[] arr = new int[5];
                public int this[int i]
                {
                    get { return arr[i]; }
                    set { arr[i] = value; }
                }
            }

            class Car
            {
                public string Name { get; set; }
                public string Number { get; set; }
                public override string ToString() { return Name + " " + Number; }
            }

            class Parking
            {
                List<Car> _cars = new List<Car>();

                public Car this[string indexNumber] // Индексатор позволяет обращаться к объектам коллекции Car
                {
                    get
                    {
                        var car = _cars.FirstOrDefault(c => c.Number == indexNumber);
                        return car;
                    }
                }

                public Car this[int indexPosition]  // Индексаторы можно перегружать
                {
                    get
                    {
                        if (indexPosition < _cars.Count)
                            return _cars[indexPosition];
                        return null;
                    }
                    set
                    {
                        if (indexPosition < _cars.Count)
                            _cars[indexPosition] = value;
                    }
                }

                public int Add(Car car)
                {
                    if (car == null)
                        throw new ArgumentException(nameof(car), "Car is null");

                    if (_cars.Count < 100)
                    {
                        _cars.Add(car);
                        return _cars.Count;
                    }

                    return -1;
                }
            }

            static void Main_()
            {
                A obj = new A();
                obj[0] = 0;
                obj[1] = 1;
                Console.WriteLine(obj[0]);
                Console.WriteLine(obj[1]);

                var cars = new List<Car>()
                {
                    new Car() { Name = "Lada", Number = "12132EA" },
                    new Car() { Name = "Hyundai", Number = "2225EN" },
                };

                var parking = new Parking();
                foreach (var item in cars) { parking.Add(item); }

                Console.WriteLine(parking["12132EA"].Name);                     // Выведет Lada
                parking[1] = new Car() { Name = "BMW", Number = "156742XN" };   // Устанавливаем значение через индексатор 
                Console.WriteLine(parking[1]);
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

                Во время сборки мусора выполнение программы приостанавливается
            */
            class A
            {
                ~A() { Console.WriteLine("Destructor"); }
            }

            // IDisposable объявляет единственный метод Dispose,
            // освобождающий неуправляемые ресурсы - вызывает финализатор немедленно
            public class B : IDisposable
            {
                public void Dispose() { Console.WriteLine("Dispose"); }
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
                    if (objB != null)
                        objB.Dispose();
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
                        Console.WriteLine("You caught ArgumentException");
                    else
                        throw e;
                }
            }

            static class ExceptionThrower
            {
                public static void TriggerException(bool isTrigger) { throw new ArgumentException(); }
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
                    finally { Console.WriteLine("3"); }
                }
                // Если в try произошло исключение, то вызовется соответствующий блок catch
                catch (NullReferenceException ex) { Console.WriteLine("4"); }
                catch (Exception ex) { Console.WriteLine("5"); }
                finally // Вызовется в любом случае
                {
                    Console.WriteLine("6");
                    throw new NullReferenceException(); // Необработанное исключение
                }

                try { ExceptionThrower.TriggerException(true); }
                catch (Exception e) { ExceptionHandler.Handle(e); }
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
            Func<int, string> func;     // принимает от 1 до 16 аргументов и возвращает значение

            static class A
            {
                public static void Display1() { Console.WriteLine("1"); }
                public static void Display2() { Console.WriteLine("2"); }
            }

            static void F1() { Console.WriteLine("F1"); }
            static void F2() { Console.WriteLine("F2"); }

            static void Main_()
            {
                var del_1 = new Del(A.Display1);
                var del_2 = new Del(A.Display2);
                var del_3 = del_1 + del_2;
                del_3(); // 1 2

                // Создаем экземпляр делегата и сообщаем ему анонимный метод
                Del2 del2 = delegate (int a, int b) { return a + b; };
                Console.WriteLine(del2(1, 2));

                Del3 del3; // 3 варианта одного и того-же:
                del3 = delegate (int val) { Console.WriteLine("0"); return val * 2; };
                del3 = (val) => { Console.WriteLine("0"); return val * 2; };
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
            event MyDel TankIsEmpty;                        // Объявляем событие для нашего типа делегата
            void F1() { TankIsEmpty(); }                    // Метод вызывает событие
            static void F2() { Console.WriteLine("1"); }
            static void F3() { Console.WriteLine("0"); }

            class A
            {
                public event del ev = null;                 // Создаем событие
                public void InvokeEvent() { ev.Invoke(); }  // Здесь можно проверять, кто вызывает событие
            }

            class B
            {
                public del ev = null;
                // add remove
                // Как в свойствах, здесь можно включить дополнительные проверки
                public event del Event
                {
                    add { ev += value; }
                    remove { ev -= value; }
                }
                public void InvokeEvent() { ev.Invoke(); }
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
                objB.Event += delegate { Console.WriteLine("Anonimnyi method"); };

                // Отписать анонимный метод нельзя, код ниже не сработает
                objB.Event -= delegate { Console.WriteLine("Anonimnyi method"); };
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
                if (objA is A tmp) Console.WriteLine($"a is A, {tmp}");
                if (objB is A) Console.WriteLine("b is A");
                if (objA is B) Console.WriteLine("Error");
                if (objA is object) Console.WriteLine("a is object");
                if (objC is A) { Console.WriteLine("Error"); }
            }

            static void UseAs()
            {
                // as выполняе преобразование типов во время выполнения и не
                // генерирует исключение, если преобразование не удалось
                // Если удалось - возвращается ссылка на тип
                A objA = new A();
                B objB = new B();

                if (objA is B)
                    objB = (B)objA;
                else
                    Console.WriteLine("Error");

                // В примере выше выполняем проверку и в случае успеха делаем присвоение,
                // as делает это в один шаг
                objB = objA as B;

                if (objB != null)
                    Console.WriteLine("Success");
                else
                    Console.WriteLine("Error");
            }

            static void UseTypeof()
            {
                // Type содержит свойства и методы, для получения информации о типе
                Type type = typeof(StreamReader);
                Console.WriteLine(type.FullName);

                if (type.IsClass)
                    Console.WriteLine("Is class");
                if (type.IsAbstract)
                    Console.WriteLine("Is abstract class");
                else
                    Console.WriteLine("Is concrete class");
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
                    Console.WriteLine("Main 1: Object Exists");
                else
                    Console.WriteLine("Main 1: Object Deleted");

                // Удаляем ссылку на объект
                obj = null;

                // Вызываем сборщик мусора для удаления объекта из памяти
                GC.Collect();

                // Проверяем, что объект был удален
                if (weakRef.TryGetTarget(out obj))
                    Console.WriteLine("Main 2: Object Exists");
                else
                    Console.WriteLine("Main 2: Object Deleted");
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

        class CQRS
        {
            /*
                Паттерн позволяет:
                - разделять операции чтения (query) и записи (command)
                - улучшает масшабируемость и простоту
                - новые фичи легко добавляются путем добавления новых запросов и команд               
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
}
