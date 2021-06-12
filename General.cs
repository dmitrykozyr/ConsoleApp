using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpEdu
{
    #region==== Общее ===========================================================
    // const инициализируется при обьявлении, константы неявно статические, слово static к ним применять нельзя
    // readonly инициализируется в конструкторе или при объявлении, могут быть static, в отличие от констант - они уже неявно static
    // partial класс позволяет разбить класс на несколько частей, чтобы над каждой можно было работать отдельно
    // Если в базовом классе есть вирт метод, то в дочернем его можно переопределить через override,
    // либо через new сказать, что переопределять не надо, а использовать метод дочернего класса
    // ref — передача аргумента по ссылке, инициализировать до вызова метода
    // out — передача аргумента по ссылке, можно не инициализировать до вызова метода
    // Структуры нужны для хранения переменных, в них можно только объявлять поля, но нельзя инициализировать
    // Структура может наследоваться только от интерфейса, а ее саму нельзя наследовать
    // Ключевое слово base/this в конструкторе дочернего класса
    // Если sealed применить к классу - запрещаем наследоваться от класса
    // Если sealed применить к override-методу - запрещаем дальнейшее переопределение
    // override можно применять к виртуальным и абстрактным методам
    // Константные поля неявно статические, обращаемся к ним так [имя класса].[имя поля]
    // Стек ограничен по размеру, но он быстрее, а куча медленнее, но это почти вся оперативная память
    // Дебаг F5 - до следующей дочки останова, F10 - на следующий шаг
    // int? x переменная может принмать null
    // IEnumerable - возвращает коллекцию, а фильтрация по заданным критериям критериям происходит на стороне клиента
    // IQueriable - возвращает отфильтрованную по заданным критериям коллекцию 
    // using SCG = System.Collections.Generic; псевдоним

    // TCP - отправляет сообщение, пока не получит подтверждение об успешной доставке или не будет превышено число попыток
    // UDP - не гарантирует доставку, но более быстрый и больше подходит для широковещательной передачи

    // public доступен из любого места в коде, а также из других программ и сборок
    // private доступен только из кода в том же классе или контексте
    // protected доступен из любого места в текущем или производных классах, производные классы могут быть в других сборках
    // internal доступен из любого места кода в той же сборке, но недоступен для других программ и сборок
    // protected internal доступен из текущей сборки и из производных классов
    // private protected доступен из любого места в текущем или производных классах в той же сборке

    // Статические св-ва хранят состояние всего класса, а не отдельного объекта, к ним можно обращаться по имени класса
    // Это относится и к методам, но статические методы могут обращаться только к статическим членам класса
    // Статические конструкторы:
    // - не имеют модификаторов доступа и не принимают параметры
    // - как в статических методах, здесь нельзя использовать слово this для ссылки на текущий объект класса
    // - нельзя вызывать вручную, вызовутся автоматически при первом создании объекта класса или первом обращении к статическому члену
    // - нужны для инициализации статических данных
    // - нужны для выполнения действия, которое должно быть выполнено один раз
    // Статические классы могут содержать только статические поля, свойства и методы    
    public class General
    {        
        public General() { }                     // Вызов конструктора по умолчанию из конструктора с параметрами
        public General(string data) : this() { } // this вызывает конструктор по умомлчанию
                                                 // если указать base, вызовется конструктор родительского класса

        enum MyEnum : int
        {
            a = 0,
            b = 5,
            c,
        }
        //Console.WriteLine((int) MyEnum.c);

        public void F2()
        {
            int i = 5;              // Упаковка нужна, чтобы работать со значимым типом, как с ссылочным,
            object obj = (object)i; // чтобы при передаче в метод не создавался его дубликат, а передавался адрес

            short a = 5;            // Распаковка должна производиться в тип, из которого производилась упаковка
            object o = a;           // упаковка значимого типа в ссылочный
            short b = (short)o;     // распаковка

            int? x = null;          // проверка на null
            int y = x ?? 5;         // если левый операнд != null, то вернется он, иначе - правый
        }
                
        public int LocalFunction(int a, int b) // Локальная функция
        {
            int LocalFunction(int c, int d) { return c + d; }
            return LocalFunction(a, b);
        }

        public int x { get; private set; }                      // Свойства могут быть виртуальными и их можно переопределять        
        public int X1 { get { return x; } set { x = value; } }  // Строки ..
        public int X2 { get => x; set => x = value; }           // .. равнозначны
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string name2;
        public string Name2 { get { return name2; } }   // Строки ..
        public string Name3 => name2;                   // .. равнозначны

        Double[,] myDoubles = new Double[10, 20];       // Массивы
        String[,,] myStrings = new String[5, 3, 10];        
    }
    #endregion

    #region==== Контейнеры ======================================================
    public class Containers
    {
        public static void F1()
        {
            // В отличие от массивов, в списках не нужно указывать размер
            // Хранят однотипные объекты
            List<int> list1 = new List<int>();
            list1.Add(0);
            list1.Add(1);

            // Эквивалентно предыдущей записи
            List<int> list2 = new List<int>() { 0, 1 };

            // Двухсвязный список, где каждый элемент хранит ссылку на следующий и предыдущийэлемент
            LinkedList<int> numbers = new LinkedList<int>();
            numbers.AddLast(1);                 // вставляем в конец
            numbers.AddFirst(2);                // вставляем в начало
            numbers.AddAfter(numbers.Last, 3);  // вставляем после последнего

            // В словаре нельзя создать два поля с одинаковым ключем
            Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
            dictionary1.Add(0, "zero");
            dictionary1.Add(1, "one");

            // Похоже на словарь, но здесь ключи и значения приводятся к object,
            // что увеличивает расход памяти,
            // в отличие от словаря поддерживает многопоточное чтение
            var ht = new Hashtable()
            {
                { 0, "string 0" },
                { 1, "string 1" },
                { 2, "string 2" },
            };
            var count = ht.Count;
            ht.Add(3, "string 3");
            ht.Remove(2);
                        
            var al = new ArrayList { Capacity = 50 }; // Позволяет хранить разнотипные объекты
            al.Add(1);
            al.RemoveAt(0);             // удаляем первый элемент
            al.Reverse();               // переворачиваем список
            Console.WriteLine(al[0]);   // получение элемента по индексу
            al.Remove(4);               // удалить 4й элемент
            al.Sort();                  // отсортировать все элементы
            al.Clear();
                        
            SortedList sl = new SortedList() // Коллекция отсортирована по ключу
            {
                { 0, "string 0" },
                { 1, "string 1" },
                { 2, "string 2" },
            };
            sl.Add(3, "string 3");
            sl.Clear();
                        
            Stack st = new Stack(); // Первый пришел - последний ушел
            st.Push("string");
            st.Push(4);
            st.Pop();               // Удалить элемент
            st.Clear();
                        
            Queue qu = new Queue(); // Первый пришел - первый ушел
            qu.Enqueue("string");
            qu.Enqueue(5);
            qu.Dequeue();           // Возвращает элемент из начала очереди
        }
    }
    #endregion

    #region==== Структуры =======================================================
    // Как и классы, структуры могут хранить состояние в виде переменных и определять поведение в виде методов
    // Структуры могут быть readonly, но все их поля тоже должны быть readonly
    struct StructuresUser
    {
        public string name;
        public int age;
        //public string gender = "Male";    // Ошибка - в отличие от класса нельзя инициализировать
                                            // поля структуры напрямую при объявлении
        public StructuresUser(string name, int age) // Если определяем конструктор в структуре, то он должен инициализировать все поля
        {
            this.name = name;
            this.age = age;
        }
        public void DisplayInfo() { Console.WriteLine($"Name: {name}  Age: {age}"); }
    }

    class ProgramStructures
    {
        static void Main_()
        {
            StructuresUser tom; // В отличие от класса нам не обязательно вызывать конструктор для создания объекта структуры
                                // Надо проинициализировать все поля структуры перед получением их значений 
            tom.name = "Tom";
            tom.age = 34;
            tom.DisplayInfo();

            StructuresUser tom2 = new StructuresUser("Tom", 34);
            tom2.DisplayInfo();

            StructuresUser bob = new StructuresUser();  // Можем использовать конструктор без параметров, при вызове которого
                                                        // полям структуры будет присвоено значение по умолчанию
            bob.DisplayInfo();
            Console.ReadKey();
        }
    }
    #endregion

    #region==== Расширяющие методы ==============================================
    // Имеют слово this перед первым аргументом
    // Могут быть только статическими и только в статических классах
    // Запрещаются параметры по умолчанию
    // Можно перегружать
    // Первый аргумент не может быть помечен словами ref, out, остальные могут
    // Не имеют доступ к private и protected полям расширяемого класса
    static class ExtensionMethods
    {
        public static void Extention(this string value)
        { 
            Console.WriteLine(value); 
        }

        public static void Extention(this string value1, string value2)
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
    #endregion

    #region==== Значимые и ссылочные типы =======================================
    // Значимые типы: byte, sbyte, short, ushort, int, uint, long, ulong, float, double, decimal, bool, char, enum, struct
    // Ссылочные типы: object, string, class, interface, delegate

    // Стек - структура данных, которая растет снизу вверх: новый элемент помещается поверх предыдущего
    // Время жизни переменных ограничено их контекстом
    // Cтек - это область памяти в адресном пространстве
    // Когда программа запускается, в конце блока памяти, зарезервированного для стека, устанавливается указатель
    // При вызове каждого отдельного метода, в стеке будет выделяться область память,
    // где будут храниться значения его переменных

    // При вызове программы ниже в стеке будут определены два фрейма - по одному на метод
    // Фрейм метода Main будет пустым, а метода Calculate будет содержать x, y, z, c - x будет внизу
    // Когда метод отработает, область памяти, которая выделялась под стек, может быть использована другими методами

    // Если параметр или переменная метода представляет значимый тип, то в стеке будет храниться непосредсвенное значение
    // Ссылочные типы хранятся в куче, при создании объекта ссылочного типа в стек помещается ссылка на адрес в куче
    // Когда объект ссылочного типа перестает использоваться, сборщик мусора видит, что на объект в куче нет ссылок,
    // условно удаляет этот объект и очищает память, помечая, что сегмент памяти может быть использован для других данных
    public class A
    {
        static void Main_()
        {
            Calculate(5);
            Console.ReadKey();
        }

        static void Calculate(int x)
        {
            int y = 0;
            int z = y + x;
            object c = 0; // Ссылочный тип будет храниться в куче, а в стеке будет храниться ссылка на объект в куче
        }
    }

    // Рассмотим ситуацию, когда тип значений и ссылочный тип представляют составные типы - структуру и класс
    // При присвоении данных объекту значимого типа, он получает копию данных
    // При присвоении данных объекту ссылочного типа, он получает ссылку на объект в куче
    // В стеке будут
    // - country
    // - state.x
    // - state.y
    // В куче будут
    // - country.x
    // - country.y
    // - state.country.x
    // - state.country.y
    struct State
    {
        public int x;
        public int y;
        public Country country;
    }
    
    class Country
    {
        public int x;
        public int y;
    }

    class Program2
    {
        private static void Main_()
        {
            State state = new State();
            Country country = new Country();
            state.country = new Country();
        }
    }

    // Внутри структуры может быть переменная ссылочного типа, например, класса
    // В стеке будут
    // - state1.country
    // - state1.x
    // - state1.y
    // - state2.country
    // - state2.x
    // - state2.y
    // В куче будут
    // - country.x
    // - country.y
    struct State3
    {
        public int x;
        public int y;
        public Country country;
    }
    class Country3
    {
        public int x;
        public int y;
    }
    
    class Program3
    {
        private static void Main_()
        {
            State state1 = new State();
            State state2 = new State();

            state2.country = new Country();
            state2.country.x = 5;
            state1 = state2;
            state2.country.x = 8;
            Console.WriteLine(state1.country.x); // 8
            Console.WriteLine(state2.country.x); // 8

            Console.Read();
        }
    }
    #endregion

    #region==== Перегрузка операторов ===========================================
    // Класс Counter представляет счетчик, значение которого хранится в свойстве Value
    // Есть два объекта класса - два счетчика, которые мы хотим сравнивать
    // На данный момент операция == и + для объектов Counter недоступны
    // Эти операции могут использоваться для примитивных типов
    // Но как складывать объекты комплексных типов - классов и структур - надо выполнить перегрузку
    // Перегрузка операторов заключается в определении в классе, для объектов которого мы хотим определить оператор, специального метода
    // Он должен иметь модификаторы public static, так как будет использоваться для всех объектов класса
    // Далее идет название возвращаемого типа - это тип, объекты которого мы хотим получить
    // В результате сложения двух объектов ожидаем получить новый объект Counter
    // В результате сравнения хотим получить bool
    // Вместо названия метода идет ключевое слово operator и сам оператор
    // Далее в скобках перечисляются параметры - один из них должен представлять класс или структуру, в котором определяется оператор
    // В примере все перегруженные операторы бинарные - то есть проводятся над двумя объектами, поэтому
    // для каждой перегрузки предусмотрено по два параметра
    // В случае с операцией сложения хотим сложить два объекта класса Counter, то оператор принимает два объекта этого класса
    // В результате сложения хотим получить новый объект Counter, поэтому этот класс используется в качестве возвращаемого типа
    // Также переопределены две операции сравнения - если переопределяем одну из них, то также должны переопределить вторую
    class Counter
    {
        public int Value { get; set; }
        public static Counter operator +(Counter c1, Counter c2) { return new Counter { Value = c1.Value + c2.Value }; }
        public static bool operator >(Counter c1, Counter c2) { return c1.Value > c2.Value; }
        public static bool operator <(Counter c1, Counter c2) { return c1.Value < c2.Value; }

        class Program
        {
            static void Main_()
            {
                Counter c1 = new Counter { Value = 23 };
                Counter c2 = new Counter { Value = 45 };
                bool result = c1 > c2;
                Console.WriteLine(result);      // false
                Counter c3 = c1 + c2;
                Console.WriteLine(c3.Value);    // 23 + 45 = 68

                Console.ReadKey();
            }
        }
    }
    #endregion

    #region==== Сложность O(N) ==================================================
    // Big O описывает скорость роста времени выполнения бесконечного алгоритма

    // O(N^2 + B) не упрощается, т.к. мы ничего не знаем о 'B'
    // O(A + B) Если выполняется одна ф-я, а затем другая, то общая сложность равна сумме сложностей, т.к. они не влияют друг на друга
    // O(A * B) Если внутри одной ф-ии выполняется другая (либо есть вложенные циклы), то общая сложность равна произведению,
    //          т.к. при увеличении времении выполнения вложенной ф-ии, увеличивается время выполнения родительской

    // O(0)      На вход не передаются данные, либо алгоритм их не обрабатывает

    // O(1)      Время обработки не меняется с изменением входного объема данных
    //           В ф-ии нет циклов и рекурсии, она всегда выполняется фиксированное число шагов

    // O(logN)   На каждой итерации берется половина элементов, как при бинарном поиске в отсортированном массиве

    // O(N)      На скольлько возрастает объем входных данных, на столько возрастает и время обработки
    //           Входной аргумент ф-ии определяет число шагов цикла/рекурсии
    //           Алгоритм, описываемый как O(2N) нужно описывать без констант как O(N)
    //           O(N + logN) = O(N), т.к. величина logN < N

    // O(NlogN)  

    // O(N^A)    Есть вложенные циклы, каждый выполняет от 0 до N шагов
    //           Алгоритм, описываемый как O(N^2 + N), нужно описывать без неважной величины N как O(N^2)
    //           O(N^2) - сложность пузырьковой сортировки

    // O(A^N)    O(5 * 2^N + 10 * N^1000) = 2^N, т.к. степень растет быстрее всего остального

    // O(N!)

    // O(N^N)
    #endregion

    #region==== InterfaceLink ===================================================
    // Интерфейсы могут хранить свойства и методы
    public interface IMyInterface
    {
        void F1();
        void F2();
    }

    class Z : IMyInterface
    {
        public void F1() { Console.WriteLine("F1"); }
        public void F2() { Console.WriteLine("F2"); }
    }

    class B
    {
        static void Main_()
        {
            Z obj = new Z();
            IMyInterface inter;
            inter = obj;
            inter.F1();
            inter.F2();
        }
    }
    #endregion

    #region==== SOLID ===========================================================
    // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял конкретную задачу
    // Open Closed              методы класса должны быть открыты для расширения, но закрыты для модификации
    // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
    // Interface Segregation    не создавать интерфейсы с большим числом методов
    // Dependency Invertion     зависимости кода должны строиться от абстракции
    interface IDependencyInjection { void F1(); }

    class A2 : IDependencyInjection
    {
        public void F1() { Console.WriteLine("1"); }
    }

    class B1 : IDependencyInjection
    {
        public void F1() { Console.WriteLine("2"); }
    }

    class C
    {
        private readonly IDependencyInjection _di;
        public C(IDependencyInjection di) { _di = di; }
        public void F2() { _di.F1(); }
    }

    class Program1
    {
        static void Main_()
        {
            IDependencyInjection dependencyInjection = new A2(); // A2 меняем на B1 при необходимости
            C c = new C(dependencyInjection);
            c.F2();
        }
    }
    #endregion

    #region==== AbstractAndStaticClass ==========================================
    // Статический класс нужен для группировки логически связанных членов
    // От него нельзя наследоваться и он не может реализовывать интерфейс
    static class A3
    {
        static public void F1() { Console.WriteLine("A3"); }
    }

    // Абстрактные методы могут быть лишь в абстрактных классах, не могут иметь тело и должны быть переопределены наследником
    // Могут иметь поля, определение методов, конструктор, неявно вызываемый конструктором дочернего класса
    abstract class B3
    {
        public void F2() { Console.WriteLine("B3"); }
        public abstract void F3();
    }

    class Program4 : B3
    {
        static void Main3_()
        {
            A3.F1();

            B3 objB = new Program4();
            objB.F2();
            objB.F3();
        }

        public override void F3() { Console.WriteLine("Program"); }
    }
    #endregion

    #region==== Наследование ====================================================
    // Наследование реализует отношение is-a (является)
    // Сначала отрабатывают конструкторы базовых классов, а потом производных
    class InheritancePerson
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public void Display() { Console.WriteLine(Name); }
    }

    class InheritanceEmployee : InheritancePerson { }

    class Inheritanve
    {
        static void Main()
        {
            InheritancePerson p = new InheritancePerson { Name = "Dima" };
            p.Display();
            p = new InheritanceEmployee { Name = "Kate" };
            p.Display();

            // Объект класса Employee также является объектом класса Person, поэтому можно создать объкт так
            InheritancePerson p2 = new InheritanceEmployee { Name = "John" };
            p2.Display();
        }
    }
    #endregion

    #region==== Множественное наследование ======================================
    // Множественное наследование запрещено, но его можно реализовать через интерфейсы
    class A4
    {
        public void F1() { Console.WriteLine("F1"); }
    }

    interface IB { void F2(); }

    class B4 : IB
    {
        public void F2() { Console.WriteLine("F2"); }
    }

    class C4 : A4, IB
    {
        B4 objB = new B4();
        public void F2() { objB.F2(); }
    }

    class ProgramOOP
    {
        static void Main4_()
        {
            C4 objC = new C4();
            objC.F1();
            objC.F2();
        }
    }
    #endregion

    #region==== Polymorphism ====================================================
    class A5
    {
        public A5() { Console.WriteLine("ctor A"); }
        public virtual void F1() { Console.WriteLine("A1"); }
        public void F2() { Console.WriteLine("A2"); }
        public virtual void F3() { Console.WriteLine("A3"); }
        public void F4() { F1(); }
        public void F5() { Console.WriteLine("A5"); }
        public void F6() { Console.WriteLine("A6"); }
    }

    class B5 : A5
    {
        public B5() { Console.WriteLine("ctor B"); }
        public override void F1() { Console.WriteLine("B1"); }
        public void F2() { Console.WriteLine("B2"); }
        public new void F3() { Console.WriteLine("B3"); }
        public new virtual void F5() { Console.WriteLine("B5"); }
        public void F6() { Console.WriteLine("B6"); }
    }

    class ProgramPolymorphism
    {
        static void Main5_()
        {
            // upcast
            A5 obj = new B5();    // ctorA ctorB
            obj.F1();           // B1
            obj.F2();           // A2
            obj.F3();           // A3
            obj.F4();           // B1
            obj.F5();           // A5
            obj.F6();           // A6

            Console.WriteLine("==");

            // downcast
            //B obj2 = new A() as B;  // ctorA
            B5 obj2 = obj as B5;
            obj.F1();           // B1
            obj.F2();           // A2
            obj.F3();           // A3
            obj.F4();           // B1
            obj.F5();           // A5
            obj.F6();           // A6
        }
    }
    #endregion

    #region==== Преобразование типов ============================================
    class A6 { }
    class B6 { }

    class Person
    {
        public void F1()
        {
            // is проверяет совместимость объекта с данным типом и выдает true или false
            Object o = new Object();
            Boolean b1 = (o is Object);   // b1 равно true
            Boolean b2 = (o is Employee); // b2 равно false

            if (o is Employee)
            {
                Employee e = (Employee)o;

                // Если А — базовый класс, а В — дочерний:
                A6 a = new A6();
                B6 b = new B6();
                //if (a is A) // true
                //if (b is B) // true
                //if (a is B) // false
                //if (b is A) // true

                // as
                // Проверка совместимости o с типом Employee
                // Если o и Employee совместимы, as возвращает ненулевой указатель на этот объект, иначе — null
                // as отличается от оператора приведения типа тем, что не генерирует исключение
                //Employee e = o as Employee;
                //if (e != null) { }

                // а — экземпляр класса А, b — экземпляр класса В
                // Если b можно привести к классу А (можно, если В наследуется от А), то в а запишется b, иначе null
                //a = b as A;
            }
        }

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

    class ProgramTypeConversion
    {
        static void Main6_()
        {
            // ВОСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (upcasting)
            // Переменной person типа Person присваивается ссылка на объект Employee
            // Чтобы сохранить ссылку на объект одного класса в переменную другого класса,
            // необходимо выполнить преобразование от Employee к Person
            // Employee наследуется от Person, поэтому выполняется восходящее преобразование,
            // в итоге employee и person указывают на один объект,
            // но переменной person доступна только та часть, которая представляет функционал Person
            //Employee emp = new Employee("Dima", "Company");
            //Person per = emp; // Слева базовый класс, справа дочерний
            //Console.WriteLine(per.Name);

            //Person per2 = new Client("Bob", "Bank");
            //Console.WriteLine(per2.Name);

            // НИСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (downcasting)
            // Кроме восходящих преобразований от производного к базовому типу есть нисходящие преобразования
            // от базового к производному
            // Чтобы обратиться к функционалу типа Employee через переменную типа Person,
            // нужно применить явное преобразование
            //Employee emp2 = new Employee("Tom", "Microsoft");
            //Person per6 = emp2;             // преобразование от Employee к Person
            //Employee emp2 = per6; // так нельзя, нужно явное преобразование
            //Employee emp3 = (Employee)per6; // преобразование от Person к Employee
            //Console.WriteLine(emp3.Company);

            // obj присвоена ссылка на Employee, поэтому можем преобразовать obj к любому типу,
            // который располагается в иерархии классов между object и Employee
            //object obj = new Employee("Bill", "Microsoft");
            //Employee emp4 = (Employee)obj;

            //Person person = new Client("Sam", "ContosoBank");
            //Client client = (Client)person;

            //object obj2 = new Employee("Bill", "Microsoft");
            //((Person)obj2).Display();               // преобразование к Person для вызова метода Display
            //((Employee)obj2).Display();             // эквивалентно предыдущей записи
            //string comp = ((Employee)obj2).Company; // преобразование к Employee, чтобы получить свойство Company

            // Ниже получим ошибку, т.к. obj3 хранит ссылку на объект Employee, а не Client
            //object obj3 = new Employee("Bill", "Microsoft");
            //string bank = ((Client)obj3).Bank;

            // СПОСОБЫ ПРЕОБРАЗОВАНИЙ
            // as пытается преобразовать выражение к определенному типу и не выбрасывает исключение
            // В случае неудачи вернет null
            //Person per7 = new Person("Tom");
            //Employee emp5 = per7 as Employee;
            //if (emp == null) { Console.WriteLine("Преобразование прошло неудачно"); }
            //else { Console.WriteLine(emp.Company); }

            // is - проверка допустимости преобразования
            // person is Employee проверяет, является ли person объектом типа Employee
            // В данном случае не является, поэтому вернет false
            //Person per8 = new Person("Tom");
            //if (per8 is Employee)
            //{
            //    Employee emp6 = (Employee)per8;
            //    Console.WriteLine(emp6.Company);
            //}
            //else { Console.WriteLine("Преобразование не допустимо"); }
        }
    }
    #endregion

    #region==== Generics ========================================================
    class A7<T>
    {
        public void Display<B>() { }
    }

    class B7<T>
    {
        T x;
        public void Display<B>(B y) { Console.WriteLine(x.GetType() + " " + y.GetType()); }
    }

    class C<T>
    {
        public T x;
        public T param { get; set; }
        public C() { }
        public C(T _x) { x = _x; }
    }

    class D<T, Z>
    {
        public T x;
        public Z y;
        public T param1 { get; set; }
        public Z param2 { get; set; }
        public D(T _x, Z _y) { x = _x; y = _y; }
    }

    class E<T> : C<T>
    {
        T x;
        public E(T _x) : base(_x) { x = _x; }
    }

    // where означает, что тип T может быть только типа C, либо быть любым его наследником
    // where T : new() - у класса должен быть публичный конструктор без параметров
    class F<T, Z>
        where T : C<int>
        where Z : new()
    {
        public T x;
        public Z y;
        public T param1 { get; set; }
        public Z param2 { get; set; }
        public F(T _x, Z _y) { x = _x; y = _y; }
        public T F1() { return x; } // шаблонный тип можно возвращать
    }

    delegate R Del<R, T>(T val);

    class ProgramGenerics
    {
        static int Displ(int val)
        {
            Console.WriteLine(val);
            return 0;
        }

        static void Main7_()
        {
            A7<int> objA = new A7<int>();
            objA.Display<int>();

            B7<bool> objB = new B7<bool>();
            objB.Display<bool>(true);

            Del<int, int> del = new Del<int, int>(Displ);
            del(1);

            C<int> c1 = new C<int>(5);
            D<bool, string> c2 = new D<bool, string>(true, "false");
        }
    }
    #endregion

    #region==== Iterator ========================================================
    // Итератор возвращает все члены коллекции от начала до конца
    // Нужен для сокрытия коллекции и способа ее обхода, т.к. массивы, структуры и т.д. обходятся по разному,
    // без итераторов пришлось бы для каждой коллекции знать, как ее обходить
    // В отличие от стандартных коллекций (Dictionary), пользовательские (Итераторы) могут иметь больше возможностей
    // Интерфейс IEnumerable перебирает элементы коллекции
    class A8 : IEnumerable
    {
        int[] arr = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

        public IEnumerator GetEnumerator()
        {
            foreach (var i in arr)
                yield return arr[i];
        }
    }

    class ProgramIterator
    {
        static void Main8_()
        {
            A8 objA = new A8();

            foreach (var i in objA) // Цикл автоматически вызовет метод GetEnumerator
                Console.WriteLine(i);
        }
    }
    #endregion

    #region==== Кортежи =========================================================
    // Через NuGet нужно установить System.ValueTuple
    // Нужны для возвращения из функции двух и боле значений
    class ProgramCortege
    {
        private static (int, int) GetValues()
        {
            var result = (1, 3);
            return result;
        }

        private static (int number, string name, int year) F2()
        {
            return (1, "Dima", 1990);
        }

        static void Main_()
        {
            var tuple = (5, 10);
            Console.WriteLine(tuple.Item1); // 5
            Console.WriteLine(tuple.Item2); // 10
            tuple.Item1 += 2;
            Console.WriteLine(tuple.Item1); // 7

            // Можно дать названия полям и обращаться по имени а не черезItem1 и Item2
            var tuple2 = (count: 5, sum: 10);
            Console.WriteLine(tuple2.count); // 5
            Console.WriteLine(tuple2.sum);   // 10
            tuple2 = GetValues();
            Console.WriteLine(tuple2.count);
            Console.WriteLine(tuple2.sum);

            var tuple3 = F2();
            Console.WriteLine(tuple3.number + tuple3.name + tuple3.year);

            // Словарь из кортежей
            var tupleDictionary = new Dictionary<(int, int), string>();
            tupleDictionary.Add((1, 2), "string1");
            tupleDictionary.Add((3, 4), "string2");
            var result = tupleDictionary[(1, 2)];   // Обращаемся к элементу по двум ключам
            Console.WriteLine(result);
        }
    }
    #endregion

    #region==== LINQ ============================================================
    //===========================================================================
    // IEnumerable определяет метод GetEnumerator, который возвращает IEnumerator
    // IEnumerator показывает элементы коллекции
    // Каждый экземпляр Enumerator находится в определенной позиции и может предоставить этот элемент (IEnumerator.Current)
    // или перейти к следующему (IEnumerator.MoveNext). Цикл foreach использует IEnumerator
    class A9
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class ProgramLinq
    {
        static void Main_()
        {
            IEnumerable<A9> collA = new List<A9>()
        {
            new A9 { Id = 2, Name = "Dima" },
            new A9 { Id = 4, Name = "Champion" },
            new A9 { Id = 1, Name = "Developer" },
        };

            var query1 = collA.Where(f => f.Name.StartsWith("D"))
                              .OrderBy(f => f.Id);

            var query2 = from i in collA
                         where i.Id > 1
                         orderby i.Name
                         select i;

            foreach (var i in query2)
                Console.WriteLine(i.Id + " " + i.Name);

            string[] cities = new string[] { "Donetsk", "San Francisco", "New York", "Tokyo" };
            IEnumerable<string> collectionCities =
                from i in cities
                where i.StartsWith("D") && i.Length == 7
                orderby i
                select i;

            foreach (var i in collectionCities)
                Console.WriteLine(i);

            // Примеры запросов
            //var data = dbContext.PupilTable.Include(e => e.Name).Where(e => e.Id == updateTeacher.Id);
            //var data2 = dbContext.PupilTable
            //    .Where(e => e.Id > 3)
            //    .OrderBy(e => e.Id)
            //    //.OrderByDescending(e => e.Id)
            //    .ThenBy(e => e.Name);
            //var data3 = dbContext.PupilTable.Where(e => EF.Functions.Like(e.Name, "%Dima%"));   // Выберутся записи со словом 'Dima'
            //var data4 = dbContext.PupilTable.Find(1);                                           // Выбор единичного элемента по id

            //// FirstOrDefault - возвращает первый соответствующий критерию элемент, а не найдя ни одного элемента, вернет null
            //// Single/SingleOrDefault выдает исключение, если кртерию соответствуют несколько элементов
            //var data5 = dbContext.PupilTable.FirstOrDefault(e => e.Id == 3);

            //// Select проверяет все написи на соответствие критерию и возвращает true/false
            //var data6 = dbContext.PupilTable.Select(e => new Pupil   // Если много свойств, делаем выборку только с нужными
            //{
            //    Name = e.Name,                                       // Имена слева указываем сами
            //    FavouriteLesson = e.FavouriteLesson
            //});

            //// new создает новую коллекцию с новыми полями, значения для которых мы задаем
            //var data7 = list.Select(q => new { Name = q, IsA = q.Contains("а") });
            //var data8 = collection.Select(i => new { Id = i.Id, Name = i.Name }).Where(q => q.Name.Contains("i").OrderBy(q => q.Id);

            //// SelectMany - выбирает данные из массива
            //var data9 = a.SelectMany(q => q.Numbers, (box, number) => (number, box.Name));
            //var data10 = a.SelectMany((x, index) => x.Numbers.Select(number => (number, index)));

            //var data11 = dbContext.PupilTable.Any(e => e.Id == 4);                // Проверяет, что есть указанные данные, Возвращает bool
            //var data12 = dbContext.PupilTable.All(e => e.Name == "Some text");    // Все данные должны удовлетворять критерию
            //var data13 = dbContext.PupilTable.Count(e => e.Name == "Some text");  // Число строк в таблице, удовлетворяющих криртерию
            //var data14 = dbContext.PupilTable.Min(e => e.Id);                     // Min/Max значение заданного параметра
        }
    }
    #endregion

    #region==== Indexator =======================================================
    // Индексаторы — свойства с параметрами и без названия, должны иметь минимум один параметр
    // В классе может быть более одного индексатора, они должны отличаться типом или количеством индексов
    class A10
    {
        private int[] arr = new int[5];
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
        private List<Car> _cars = new List<Car>();
        private const int MAX_CARS = 100;

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
                if (indexPosition < _cars.Count) return _cars[indexPosition];
                return null;
            }
            set
            {
                if (indexPosition < _cars.Count) _cars[indexPosition] = value;
            }
        }

        public int Add(Car car)
        {
            if (car == null)
                throw new ArgumentException(nameof(car), "Car is null");

            if (_cars.Count < MAX_CARS)
            {
                _cars.Add(car);
                return _cars.Count;
            }

            return -1;
        }
    }

    class ProgramIndexator
    {
        static void Main_()
        {
            A10 obj = new A10();
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
    #endregion

    #region==== MemoryClean =====================================================
    // Большинство объектов программы относятся к управляемым и очищаются сборщиком мусора, но есть неуправляемые
    // объекты (низкоуровневые файловые дескрипторы, сетевые подключения), которые сборщик мусора не может удалить    
    // Освобождение неуправляемых ресурсов подразумевает реализацию одного из двух механизмов
    // - Финализатор
    // - Реализация интерфейса IDisposable

    // Финализатор вызывается непосредственно перед сборкой мусора
    // Программа может завершиться до того, как произойдет сборка мусора, поэтому финализатор может быть не вызван
    class A11
    {
        ~A11() { Console.WriteLine("Destructor"); }
    }

    // На деле сборщик мусора вызывает не финализатор, а метод Finalize класса A, потому что компилятор компилирует
    // финализатор в конструкцию
    //protected override void Finalize()
    //{
    //    try { /* здесь идут инструкции деструктора */ }
    //    finally { base.Finalize(); }
    //}

    // IDisposable объявляет один единственный метод Dispose, освобождающий неуправляемые ресурсы, он вызывает финализатор немедленно
    public class B11 : IDisposable
    {
        public void Dispose() { Console.WriteLine("Dispose"); }
    }

    class ProgramMemoryClean
    {
        // try finally гарантирует, что в случае исключения Dispose освободит ресурсы
        // Можно использовать using
        private static void Test()
        {
            B11 objB = null;
            try
            {
                objB = new B11();
            }
            finally
            {
                if (objB != null)
                    objB.Dispose();
            }
        }

        static void Main11_()
        {
            Test();
        }
    }
    #endregion

    #region==== Exceptions ======================================================
    // Исключение не обязательно должно быть обработано в том классе, где оно произошло
    // Можно создать класс для обработки определенных исключений
    public class ExceptionHandler
    {
        public static void Handle(Exception e)
        {
            if (e.GetBaseException().GetType() == typeof(ArgumentException))
                Console.WriteLine("You caught ArgumentException");
            else
                throw e;
        }
    }

    public static class ExceptionThrower
    {
        public static void TriggerException(bool isTrigger) { throw new ArgumentException(); }
    }

    class ProgramExceptions
    {
        public static void F1()
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
        }
        static void Main12_()
        {
            try { ExceptionThrower.TriggerException(true); }
            catch (Exception e) { ExceptionHandler.Handle(e); }
        }
    }
    #endregion

    #region==== Events ==========================================================
    public class E
    {
        // В отличие от делегатов, события более защищены от непреднамеренных изменений,
        // тогда как делегат может быть случайно обнулен, если использовать '=' вместо '+='
        // Событие можно использовать лишь внутри класса, в котором оно определено
        // Пример 1
        public delegate void MyDel();
        public event MyDel TankIsEmpty;     // Объявляем событие для нашего типа делегата
        public void F1() { TankIsEmpty(); } // Метод вызывает событие
        static void EventHandlerMethod1() { Console.WriteLine("1"); }
        static void EventHandlerMethod2() { Console.WriteLine("2"); }
        static void Main_()
        {
            E obj = new E();
            obj.TankIsEmpty += EventHandlerMethod1; // Подписка на событие
            obj.TankIsEmpty += EventHandlerMethod2;
            obj.F1();
        }
    }

    // Пример 3
    public delegate void del();
    public class A13
    {
        public event del ev = null; // Создаем событие
        public void InvokeEvent() { ev.Invoke(); } // Здесь можно проверять, кто вызывает событие
    }

    class B13
    {
        static private void F1() { Console.WriteLine("0"); }
        static private void F2() { Console.WriteLine("1"); }

        static void Main_()
        {
            A13 objA = new A13();
            // присваиваем событию делегат, на который подписываем метод
            objA.ev += new del(F1); // смысл
            objA.ev += F2;          // одинаковый
            objA.InvokeEvent();     // напрямую запрещено вызывать события через objA.ev.Invoke()
        }
    }

    public class C13
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

    class D
    {
        private static void Method0() { Console.WriteLine("0"); }
        private static void Method1() { Console.WriteLine("1"); }

        static void Main_()
        {
            C13 objC = new C13();
            objC.Event += Method0;
            objC.Event += Method1;
            objC.Event += delegate { Console.WriteLine("Anonimnuy method"); };
            // Отписать анонимный метод нельзя, код ниже не сработает
            objC.Event -= delegate { Console.WriteLine("Anonimnuy method"); };
            objC.InvokeEvent();
        }
    }
    #endregion

    #region==== Delegates =======================================================
    // На делегаты можно подписать один и более методов, а затем при вызове делегата будут вызваны эти методы
    // Делегаты можно суммировать
    // Сигнатуры делегата и его методов должны совпадать
    // Если на делегат подписано несколько методов, возвращающих значение, то через делегат мы получим значение лишь последнего метода

    // Пример 1
    static class A14
    {
        public static void Display1() { Console.WriteLine("1"); }
        public static void Display2() { Console.WriteLine("2"); }
    }

    delegate void Del4();

    class ProgramDelegates
    {
        static void Main_()
        {
            Del4 del1 = new Del4(A14.Display1);
            Del4 del2 = new Del4(A14.Display2);
            Del4 del3 = del1 + del2;
            del3(); // 1 2
                    // del.Invoke();  // аналогичный, но более наглядный способ вызова делегата
        }

        static void F1() { Console.WriteLine("F1"); }
        static void F2() { Console.WriteLine("F2"); }
        delegate void MyDel();

        void Start()
        {
            MyDel myDel = F1;
            myDel();     // F1
            myDel += F2;
            myDel();     // F1 F1 F2
            myDel += F1;
            myDel -= F1; // такой код удалит последний добавленный метод F1
                         // удаляются в том порядке, в котором были добавлены
        }

        // Пример 3 - шаблонные делегаты
        // Action
        Action action = F1; // public delegate void Action(); это можно не писать, этот делегат можно сразу использовать без объявления
                            // ничего не возвращает, но может быть перегружен, чтобы принимать от 0 до 16 аргументов

        // Predicate - принимает минимум один аргумент
        Predicate<int> predicate; // эквивалентно этому delegate bool Predicate<T>(T value);
                                  // и этому            delegate bool Predicate(int value);

        // Func - принимает от 1 до 16 аргументов
        Func<int, string> func;  // Delegate int Func(string i) здесь последний тип - это тип возвращаемого значения
                                 // в любом случае возвращает значение

        // Анонимные методы
        // Позволяют присвоить делегату метод, не обЪявляя его
        public delegate int Del(int a, int b);

        static void Main_2()
        {
            // Создаем экземпляр делегата и сообщаем ему анонимный метод
            Del del = delegate (int a, int b) { return a + b; };
            Console.WriteLine(del(1, 2));
        }

        // Лямбда-операторы
        // В них не нужно указывать тип, ведь эта информация есть внутри делегата
        // Это лишь короткая запись анонимного метода, присваемого экземпляру класса-делегата
        delegate int Del2(int val);

        static void Main_3()
        {
            Del2 del; // 3 варианта одного и того-же:
            del = delegate (int val) { Console.WriteLine("0"); return val * 2; };
            del = (val) => { Console.WriteLine("0"); return val * 2; };
            del = val => val * 2; // 1е значение - аргумент, 2е - возвращаемое значение
            Console.WriteLine(del(5));
        }
    }
    #endregion
}

#region==== .................. ==============================================
//===========================================================================
// 
#endregion
