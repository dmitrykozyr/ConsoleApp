using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpEdu
{
    #region==== Общее ===========================================================
    // const инициализируется при обьявлении, константы неявно статические, слово static к ним применять нельзя
    // readonly инициализируется в конструкторе или при объявлении, могут быть static, в отличие от констант - они неявно static
    // partial класс позволяет разбить класс на несколько частей
    // Если в базовом классе есть вирт метод, то в дочернем его можно переопределить через override,
    // либо через new сказать, что переопределять не надо, а использовать метод дочернего класса
    // ref - передача аргумента по ссылке, инициализировать до вызова метода
    // out - можно не инициализировать до вызова метода    
    // Ключевое слово base/this в конструкторе дочернего класса
    // Если sealed применить к классу - запрещаем наследоваться от класса
    // Если sealed применить к override-методу - запрещаем дальнейшее переопределение
    // override можно применять к виртуальным и абстрактным методам
    // Стек ограничен по размеру, но он быстрее, а куча медленнее, но это почти вся оперативная память
    // Дебаг F5 - до следующей дочки останова, F10 - на следующий шаг
    // IEnumerable - возвращает коллекцию, а фильтрация по заданным критериям критериям происходит на стороне клиента
    // IQueriable - возвращает отфильтрованную по заданным критериям коллекцию 
    // using SCG = System.Collections.Generic; псевдоним
    // TCP - отправляет сообщение, пока не получит подтверждение об доставке или не будет превышено число попыток
    // UDP - не гарантирует доставку, но более быстрый и подходит для широковещательной передачи
    // public доступ из любого места в коде, из других программ и сборок
    // private доступ только из кода в том же классе или контексте
    // protected доступ из любого места в текущем или производных классах, производные классы могут быть в других сборках
    // internal доступ из любого места кода в той же сборке
    // protected internal доступ из текущей сборки и из производных классов
    // private protected доступ из любого места в текущем или производных классах в той же сборке

    // Статические классы могут содержать только статические поля, свойства и методы    
    // Статические св-ва хранят состояние всего класса, а не отдельного объекта, к ним можно обращаться по имени класса
    // Это относится и к методам, статические методы могут обращаться только к статическим членам класса
    // Статические конструкторы:
    // - не имеют модификаторов доступа и не принимают параметры
    // - нельзя использовать слово this для ссылки на текущий объект класса
    // - вызовутся автоматически при первом создании объекта класса или первом обращении к статическому члену
    // - нужны для инициализации статических данных
    // - нужны для выполнения действия, которое должно быть выполнено один раз

    // 77 ключевых слов языка
    // abstract     as          base        bool        break
    // byte         case        catch       char        checked
    // class        const       continue    decimal     default
    // delegate     do          double      else        enum
    // event        explicit    extern      false       finally
    // fixed        float       for         foreach     goto
    // if           implicit    in          int         interface
    // internal     is          lock        long        namespace
    // new          null        object      operator    out
    // override     params      private     protected   public
    // readonly     ref         return      sbyte       sealed
    // short        sizeof      stackalloc  static      string
    // struct       switch      this        throw       true
    // try          typeof      uint        ulong       unchecked
    // unsafe       ushort      using       virtual     volatile
    // void         while

    // 18 контекстных ключевых слов
    // add      dynamic from    get     global
    // group    into    join    let     orderby
    // partial  remove  select  set     value
    // var      where   yield

    // 13 значимых типов                                Разрядность в битах     Диапазон представления чисел
    // bool     логический
    // byte     8-разрядный целочисленный без знака     8   ------------------  0 - 255
    // char     символьный                              16  ------------------  0 - 65.535
    // decimal  десятичный (для финансовых расчетов)    128 ------------------  1Е-28 - 7,9Е+28
    // double   с плавающей точкой двойной точности     64  ------------------  5Е-324 - 1,7Е+308
    // float    с плавающей точкой одинарной точности   32  ------------------  5Е-45 - 3,4Е+38
    // int      целочисленный                           32  ------------------  -2.147.483.648 - 2.147.483.647
    // long     длинный целочисленный                   64  ------------------  -9.223.372.036.854.775.808 - 9.223.372.036.854.775.807
    // sbyte    8-разрядный целочисленный со знаком     16  ------------------  -128-127
    // short    короткий целочисленный                  16  ------------------  -32.768 - 32.767
    // uint     целочисленный без знака                 32  ------------------  0 - 4.294.967.295
    // ulong    длинный целочисленный без знака         64  ------------------  0 - 18.446.744.073.709.551.615
    // ushort   короткий целочисленный без знака        16  ------------------  0 - 65.535
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
        //Console.WriteLine((int)MyEnum.c);

        public void F2()
        {
            int i = 5;              // Упаковка нужна, чтобы работать со значимым типом, как с ссылочным,
            object obj = (object)i; // чтобы при передаче в метод не создавался его дубликат, а передавался адрес

            short a = 5;            // Распаковка должна производиться в тип, из которого производилась упаковка
            object o = a;           // упаковка значимого типа в ссылочный
            short b = (short)o;     // распаковка

            int? x = null;          // переменная может принмать null
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

            // Двухсвязный список, где каждый элемент хранит ссылку на следующий и предыдущий элемент
            LinkedList<int> numbers = new LinkedList<int>();
            numbers.AddLast(1);                 // вставляем в конец
            numbers.AddFirst(2);                // вставляем в начало
            numbers.AddAfter(numbers.Last, 3);  // вставляем после последнего

            // В словаре нельзя создать два поля с одинаковым ключем
            Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
            dictionary1.Add(0, "zero");
            dictionary1.Add(1, "one");

            // Похоже на словарь, но здесь ключи и значения приводятся к object,
            // что увеличивает расход памяти, зато поддерживается многопоточное чтение
            var ht = new Hashtable()
            {
                { 0, "string 0" },
                { 1, "string 1" },
                { 2, "string 2" },
            };
            var count = ht.Count;
            ht.Add(3, "string 3");
            ht.Remove(2);

            // Позволяет хранить разнотипные объекты
            var al = new ArrayList { Capacity = 50 };
            al.Add(1);
            al.RemoveAt(0);             // удаляем первый элемент
            al.Reverse();               // переворачиваем список
            Console.WriteLine(al[0]);   // получение элемента по индексу
            al.Remove(4);               // удалить 4й элемент
            al.Sort();                  // отсортировать все элементы
            al.Clear();

            // Коллекция отсортирована по ключу
            SortedList sl = new SortedList()
            {
                { 0, "string 0" },
                { 1, "string 1" },
                { 2, "string 2" },
            };
            sl.Add(3, "string 3");
            sl.Clear();

            // Первый пришел - последний ушел
            Stack st = new Stack();
            st.Push("string");
            st.Push(4);
            st.Pop();       // Удалить элемент
            st.Clear();

            // Первый пришел - первый ушел
            Queue qu = new Queue();
            qu.Enqueue("string");
            qu.Enqueue(5);
            qu.Dequeue();   // Возвращает элемент из начала очереди
        }
    }
    #endregion

    #region==== Структуры =======================================================
    // Как и классы, структуры могут хранить состояние в виде переменных и определять поведение в виде методов
    // Может наследоваться только от интерфейса, а ее саму нельзя наследовать
    // Могут быть readonly, но все их поля тоже должны быть readonly
    struct StructuresUser
    {
        public string name;
        public int age;
        //public string gender = "Male";    // Ошибка - в отличие от класса нельзя инициализировать
                                            // поля структуры при объявлении
        public StructuresUser(string name, int age) // Если определяем конструктор в структуре, то он должен инициализировать все поля
        {
            this.name = name;
            this.age = age;
        }

        public void DisplayInfo() { Console.WriteLine($"Name: {name} Age: {age}"); }
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

            StructuresUser john = new StructuresUser("John", 37);
            john.DisplayInfo();

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
    // Значимые типы: byte, short, int, long, float, double, decimal, bool, char, enum, struct
    // Ссылочные типы: object, string, class, interface, delegate

    // Cтек - это область памяти в адресном пространстве
    // Когда программа запускается, в конце блока памяти, зарезервированного для стека, устанавливается указатель
    // При вызове каждого метода, в стеке будет выделяться область память,
    // где будут храниться значения переменных

    // При вызове программы ниже, в стеке будут определены два фрейма - по одному на метод
    // Фрейм метода Main будет пустым, а метода Calculate будет содержать x, y, z, c - x будет внизу

    // Если параметр или переменная метода представляет значимый тип, то в стеке будет храниться непосредсвенное значение
    // Ссылочные типы хранятся в куче, при создании объекта ссылочного типа в стек помещается ссылка на адрес в куче
    public class A
    {
        static void Calculate(int x)
        {
            int y = 0;
            int z = y + x;
            object c = 0; // Ссылочный тип будет храниться в куче, а в стеке будет храниться ссылка на объект в куче
        }
        
        static void Main_()
        {
            Calculate(5);
            Console.ReadKey();
        }
    }

    // Ниже значимый и ссылочный типы представляют структуру и класс
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
    // Есть два объекта класса - два счетчика, которые хотим сравнивать
    // На данный момент операция == и + для объектов Counter недоступны
    // Эти операции могут использоваться для примитивных типов, но не для классов и структур
    // Для перегрузки оператора определеним в классе, для объектов которого хотим определить оператор, специальный метод
    // Он должен иметь модификаторы public static, так как будет использоваться для всех объектов класса
    // Далее идет название возвращаемого типа
    // В результате сложения ожидаем получить новый объект Counter, сравнения - bool
    // Вместо названия метода идет слово operator и сам оператор
    // Далее в скобках перечисляются параметры - один из них должен представлять класс или структуру, в котором определяется оператор
    // В примере перегруженные операторы проводятся над двумя объектами, поэтому для каждой перегрузки будет по два параметра
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
    // O(A * B) Если внутри одной ф-ии выполняется другая или есть вложенные циклы, то общая сложность равна произведению
    //          При увеличении времении выполнения вложенной ф-ии, увеличивается время выполнения родительской

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
        public void F3() { Console.WriteLine("F3"); }
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
            //inter.F3(); // Ошибка
        }
    }
    #endregion

    #region==== SOLID ===========================================================
    // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял конкретную задачу
    // Open Closed              методы класса должны быть открыты для расширения, но закрыты для модификации
    // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
    // Interface Segregation    не создавать интерфейсы с большим числом методов
    // Dependency Invertion     зависимости кода должны строиться от абстракции
    interface IDependencyInvertion { void F1(); }

    class ADI : IDependencyInvertion
    {
        public void F1() { Console.WriteLine("ADI"); }
    }

    class BDI : IDependencyInvertion
    {
        public void F1() { Console.WriteLine("BDI"); }
    }

    class CDI
    {
        private readonly IDependencyInvertion _di;
        public CDI(IDependencyInvertion di) { _di = di; }
        public void F2() { _di.F1(); }
    }

    class ProgramDependencyInvertion
    {
        static void Main_()
        {
            IDependencyInvertion dependencyInvertion = new ADI(); // ADI меняем на BDI при необходимости
            dependencyInvertion.F1();   // ADI

            CDI c = new CDI(dependencyInvertion);
            c.F2();                     // ADI
        }
    }
    #endregion

    #region==== AbstractAndStaticClass ==========================================
    // Статический класс нужен для группировки логически связанных членов
    // От него нельзя наследоваться и он не может реализовывать интерфейс
    static class AStat
    {
        static public void F1() { Console.WriteLine("A3"); }
    }

    // Абстрактный класс похож на обычный - может иметь переменные, методы, конструкторы, свойства
    // Абстрактные методы могут быть лишь в абстрактных классах, не могут иметь тело и должны быть переопределены наследником
    // Нельзя использовать конструктор абстрактного класса для создания объекта
    // В программе для банковского сектора определим классы клиента банка и сотрудника банка
    // Для сотрудника надо определить должность, а для клиента - сумму на счете
    // Они могут иметь что-то общее, например, имя и фамилию
    // Общую функциональность выносят в абстрактный класс - напрямую его экземпляры мы создавать не будем,
    // он будет хранить что-то общее для дочерних классов

    // Кроме обычных свойств и методов абстрактный класс может иметь абстрактные методы, свойства, индексаторы, события
    // Абстрактные члены не должны иметь модификатор private
    // Производный класс обязан переопределить (override) и реализовать все абстрактные методы и свойства, которые имеются в базовом абстрактном классе ..
    // .. исключение - если дочерний класс тоже абстрактный
    abstract class BAbs
    {
        public abstract string Name { get; set; }
        public void F2() { Console.WriteLine("B3"); }
        public abstract void F3();
    }

    class ProgramAbstractAndStaticClass : BAbs
    {
        private string name; // Переопределение абстрактного св-ва из базового абстрактного класса
        public override string Name
        {
            get { return "Mr/Ms. " + name; }
            set { name = value; }
        }

        static void Main_()
        {
            AStat.F1();

            BAbs objB = new ProgramAbstractAndStaticClass();
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
        static void Main_()
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
    class AMI
    {
        public void F1() { Console.WriteLine("F1"); }
    }

    interface IMI { void F2(); }

    class BMI : IMI
    {
        public void F2() { Console.WriteLine("F2"); }
    }

    class CMI : AMI, IMI
    {
        BMI objB = new BMI();
        public void F2() { objB.F2(); }
    }

    class ProgramMultipleInheritance
    {
        static void Main_()
        {
            CMI objC = new CMI();
            objC.F1();
            objC.F2();
        }
    }
    #endregion

    #region==== Polymorphism ====================================================
    class AP
    {
        public AP() { Console.WriteLine("ctor A"); }
        public virtual void F1() { Console.WriteLine("A1"); }
        public void F2() { Console.WriteLine("A2"); }
        public virtual void F3() { Console.WriteLine("A3"); }
        public void F4() { F1(); }
        public void F5() { Console.WriteLine("A5"); }
    }

    class BP : AP
    {
        public BP() { Console.WriteLine("ctor B"); }
        public override void F1() { Console.WriteLine("B1"); }
        public void F2() { Console.WriteLine("B2"); }
        public new void F3() { Console.WriteLine("B3"); }
        public new virtual void F5() { Console.WriteLine("B5"); }
    }

    class ProgramPolymorphism
    {
        static void Main_()
        {
            // upcast
            AP obj = new BP();  // ctorA ctorB
            obj.F1();           // B1
            obj.F2();           // A2
            obj.F3();           // A3
            obj.F4();           // B1
            obj.F5();           // A5
            
            Console.WriteLine("==");

            // downcast
            
            BP obj2 = obj as BP; // B obj2 = new A() as B;
            obj.F1();            // B1
            obj.F2();            // A2
            obj.F3();            // A3
            obj.F4();            // B1
            obj.F5();            // A5
        }
    }
    #endregion

    #region==== Преобразование типов ============================================
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

    class ProgramTypeConversion
    {
        static void Main_()
        {
            // ВОСХОДЯЩИЕ ПРЕОБРАЗОВАНИЕ (upcasting)
            // Переменной per типа Person присваивается ссылка на Employee
            // Employee наследуется от Person, поэтому выполняется восходящее преобразование,
            // в итоге employee и person указывают на один объект,
            // но per доступна только та часть, которая представляет функционал Person
            Employee emp = new Employee("Dima", "Company");
            Person per = emp;
            Console.WriteLine(per.Name);

            // НИСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (downcasting) - от базового класса к производному
            // Чтобы обратиться к функционалу Employee через переменную типа Person,
            // нужно явное преобразование
            Employee emp2 = new Employee("Tom", "Microsoft");
            Person per2 = emp2;             // преобразование от Employee к Person
            Employee emp3 = (Employee)per2; // преобразование от Person к Employee
            Console.WriteLine(emp3.Company);

            // obj присвоена ссылка на Employee, поэтому можем преобразовать obj к любому типу,
            // который располагается в иерархии классов между object и Employee
            object obj = new Employee("Bill", "Microsoft");
            Employee emp4 = (Employee)obj;

            object obj2 = new Employee("Bill", "Microsoft");
            ((Person)obj2).Display();               // преобразование к Person для вызова метода Display
            ((Employee)obj2).Display();             // эквивалентно предыдущей записи
            string comp = ((Employee)obj2).Company; // преобразование к Employee, чтобы получить свойство Company

            // СПОСОБЫ ПРЕОБРАЗОВАНИЙ
            // as пытается преобразовать выражение к определенному типу и не выбрасывает исключение
            // В случае неудачи вернет null
            Person per7 = new Person("Tom");
            Employee emp5 = per7 as Employee;
            if (emp == null) { Console.WriteLine("Преобразование прошло неудачно"); }
            else { Console.WriteLine(emp.Company); }

            // is - проверка допустимости преобразования
            // person is Employee проверяет, является ли person объектом типа Employee
            // В данном случае не является, поэтому вернет false
            Person per8 = new Person("Tom");
            if (per8 is Employee)
            {
              Employee emp6 = (Employee)per8;
              Console.WriteLine(emp6.Company);
            }
            else { Console.WriteLine("Преобразование недопустимо"); }
        }
    }
    #endregion

    #region==== Generics ========================================================
    class AG<T>
    {
        public void Display<B>() { }
    }

    class BG<T>
    {
        T x;
        public void Display<B>(B y) { Console.WriteLine(x.GetType() + " " + y.GetType()); }
    }

    class CG<T>
    {
        public T x;
        public T param { get; set; }
        public CG() { }
        public CG(T _x) { x = _x; }
    }

    class DG<T, Z>
    {
        public T x;
        public Z y;
        public T param1 { get; set; }
        public Z param2 { get; set; }
        public DG(T _x, Z _y) { x = _x; y = _y; }
    }

    class EG<T> : CG<T>
    {
        T x;
        public EG(T _x) : base(_x) { x = _x; }
    }

    // where означает, что тип T может быть только типа C, либо быть любым его наследником
    // where T : new() - у класса должен быть публичный конструктор без параметров
    class FG<T, Z>
        where T : CG<int>
        where Z : new()
    {
        public T x;
        public Z y;
        public T param1 { get; set; }
        public Z param2 { get; set; }
        public FG(T _x, Z _y) { x = _x; y = _y; }
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

        static void Main_()
        {
            AG<int> objA = new AG<int>();
            objA.Display<int>();

            BG<bool> objB = new BG<bool>();
            objB.Display<bool>(true);

            Del<int, int> del = new Del<int, int>(Displ);
            del(1);

            CG<int> c1 = new CG<int>(5);
            DG<bool, string> c2 = new DG<bool, string>(true, "false");
        }
    }
    #endregion

    #region==== Iterator ========================================================
    // Итератор возвращает все члены коллекции от начала до конца
    // Нужен для сокрытия коллекции и способа ее обхода, т.к. массивы, структуры и т.д. обходятся по разному,
    // без итераторов пришлось бы для каждой коллекции знать, как ее обходить
    // В отличие от стандартных коллекций (Dictionary), иИтераторы имеют больше возможностей
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
            Console.WriteLine(tuple.Item1);     // 5
            Console.WriteLine(tuple.Item2);     // 10
            tuple.Item1 += 2;
            Console.WriteLine(tuple.Item1);     // 7

            // Можно дать названия полям и обращаться по имени а не черезItem1 и Item2
            var tuple2 = (count: 5, sum: 10);
            Console.WriteLine(tuple2.count);    // 5
            Console.WriteLine(tuple2.sum);      // 10
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
    class AL
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class ProgramLinq
    {
        static void Main_()
        {
            IEnumerable<AL> collA = new List<AL>()
            {
                new AL { Id = 2, Name = "Dima" },
                new AL { Id = 4, Name = "Champion" },
                new AL { Id = 1, Name = "Developer" },
            };

            var query1 = collA.Where(z => z.Name.StartsWith("D")).OrderBy(z => z.Id);

            var query2 = from i in collA
                         where i.Id > 1
                         orderby i.Name
                         select i;

            foreach (var i in query2)
                Console.WriteLine(i.Id + " " + i.Name);

            //var data = dbContext.PupilTable.Include(e => e.Name).Where(e => e.Id > 3).OrderBy(e => e.Id).ThenBy(e => e.Name);
            //var data = dbContext.PupilTable.FirstOrDefault(e => e.Id == 3);
            //var data = dbContext.PupilTable.Select(e => new Pupil
            //{
            //    Name = e.Name,
            //    FavouriteLesson = e.FavouriteLesson
            //});
            //var data = collection.Select(i => new { Id = i.Id, Name = i.Name }).Where(q => q.Name.Contains("i").OrderBy(q => q.Id);
            //var data = dbContext.PupilTable.Any(e => e.Id == 4);                // Проверяет, что есть указанные данные, возвращает bool
            //var data = dbContext.PupilTable.All(e => e.Name == "Some text");    // Все данные должны удовлетворять критерию
            //var data = dbContext.PupilTable.Count(e => e.Name == "Some text");  // Число строк в таблице, удовлетворяющих криртерию
        }
    }
    #endregion

    #region==== Indexator =======================================================
    // Свойства с параметрами и без названия, должны иметь минимум один параметр
    // В классе может быть несколько индексаторов, должны отличаться сигнатурой
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
        static void Main()
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
    // Большинство объектов относятся к управляемым и очищаются сборщиком мусора, но есть неуправляемые объекты
    // Освобождение неуправляемых ресурсов выполняет
    // - Финализатор
    // - Реализация интерфейса IDisposable

    // Финализатор вызывается непосредственно перед сборкой мусора
    // Программа может завершиться до того, как произойдет сборка мусора, поэтому финализатор может быть не вызван
    class A11
    {
        ~A11() { Console.WriteLine("Destructor"); }
    }

    // IDisposable объявляет единственный метод Dispose, освобождающий неуправляемые ресурсы - вызывает финализатор немедленно
    public class B11 : IDisposable
    {
        public void Dispose() { Console.WriteLine("Dispose"); }
    }

    class ProgramMemoryClean
    {
        // try finally гарантирует, что в случае исключения Dispose освободит ресурсы
        // Можно использовать using, вызывающий Dispose неявно
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

        static void Main_()
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
        static void Main_()
        {
            try { ExceptionThrower.TriggerException(true); }
            catch (Exception e) { ExceptionHandler.Handle(e); }
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
        Action action = F1;         // ничего не возвращает
        Predicate<int> predicate;   // принимает минимум один аргумент
        Func<int, string> func;     // принимает от 1 до 16 аргументов и возвращает значение

        // Анонимные методы позволяют присвоить делегату метод, не обЪявляя его
        public delegate int Del(int a, int b);

        static void Main_2()
        {
            // Создаем экземпляр делегата и сообщаем ему анонимный метод
            Del del = delegate (int a, int b) { return a + b; };
            Console.WriteLine(del(1, 2));
        }

        // Лямбда-операторы - в них не нужно указывать тип, эта информация есть внутри делегата
        // Короткая запись анонимного метода, присваемого экземпляру класса-делегата
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
        public event del ev = null;                 // Создаем событие
        public void InvokeEvent() { ev.Invoke(); }  // Здесь можно проверять, кто вызывает событие
    }

    class B13
    {
        static private void F1() { Console.WriteLine("0"); }
        static private void F2() { Console.WriteLine("1"); }

        static void Main_()
        {
            A13 objA = new A13();
            // присваиваем событию делегат, на который подписываем метод
            objA.ev += new del(F1); // смысл ..
            objA.ev += F2;          // .. одинаковый
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
}
