using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using SCG = System.Collections.Generic; псевдоним

namespace SharpEdu
{
    public class General
    {
        // const инициализируется при обьявлении, константы неявно статические, слово static к ним применять нельзя
        // readonly инициализируется во время выполнения из конструктора
        // partial класс позволяет разбить класс на несколько частей, чтобы над каждой можно было работать отдельно
        // Если в базовом классе есть вирт метод, то в дочернем его можно переопределить через override,
        // либо через new сказать, что переопределять не надо, а использовать метод дочернего класса
        // ref — передача аргумента по ссылке, инициализировать до вызова метода
        // out — передача аргумента по ссылке, можно не инициализировать до вызова метода
        // Структуры нужны для хранения переменных, в них можно только объявлять поля, но нельзя инициализировать
        // Структура может наследоваться только от интерфейса, а ее саму нельзя наследовать
        // Ключевое слово base/this в конструкторе дочернего класса
        // Если sealed применить к классу — запрещаем наследоваться от класса
        // Если sealed применить к override-методу — запрещаем дальнейшее переопределение
        // override можно применять к виртуальным и абстрактным методам
        // Константные поля неявно статические, обращаемся к ним так [имя класса].[имя поля]
        // Стек ограничен по размеру, но он быстрее, а куча медленнее, но это почти вся оперативная память
        // Дебаг F5 - до следующей дочки останова, F10 - на следующий шаг
        // int? x переменная может принмать null
        // IEnumerable - возвращает коллекцию, а фильтрация по заданным критериям критериям происходит на стороне клиента
        // IQueriable - возвращает отфильтрованную по заданным критериям коллекцию 

        // TCP - отправляет сообщение, пока не получит подтверждение об успешной доставке или не будет превышено число попыток
        // UDP - не гарантирует доставку, но более быстрый и больше подходит для широковещательной передачи

        // Вызов конструктора по умолчанию из конструктора с параметрами
        public General()
        { }
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
            // Упаковка нужна, чтобы работать со значимым типом, как с ссылочным, чтобы при передаче в метод не создавался его дубликат,
            // а передавался адрес, позволяет не указывать ref при передаче аргумента по ссылке
            int i = 5;
            object obj = (object)i;

            // Распаковка должна производиться в тип, из которого производилась упаковка
            short a = 5;
            object o = a;       // упаковка значимого типа в ссылочный
            short b = (short)o; // распаковка

            int? x = null;  // проверка на null
            int y = x ?? 5; // если левый операнд != null, то вернется он, иначе - правый
        }

        #region Локальная функция
        public int LocalFunction(int a, int b)
        {
            int LocalFunction(int c, int d)
            {
                return c + d;
            }
            return LocalFunction(a, b);
        }
        #endregion

        #region Свойства
        // Могут быть виртуальными и их можно переопределять
        public int x { get; private set; }
        // Строки ниже равнозначны 
        public int X1 { get { return x; } set { x = value; } }
        public int X2 { get => x; set => x = value; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Массивы
        Double[,] myDoubles = new Double[10, 20];
        String[,,] myStrings = new String[5, 3, 10];
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

        #region==== Контейнеры ======================================================
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
            st.Pop(); // Удалить элемент
            st.Clear();

            // Первый пришел - первый ушел
            Queue qu = new Queue();
            qu.Enqueue("string");
            qu.Enqueue(5);
            qu.Dequeue(); // Возвращает элемент из начала очереди
        }
        #endregion
    }

    #region==== Расширяющие методы ==============================================
    // Имеют слово this перед первым аргументом
    // Могут быть только статическими и только в статических классах
    // Запрещаются параметры по умолчанию
    // Можно перегружать
    // Первый аргумент не может быть помечен словами ref, out, остальные могут
    // Не имеют доступ к private и protected полям расширяемого класса
    static class A1
    {
        public static void Extention(this string value)
        { 
            Console.WriteLine(value); 
        }

        public static void Extention(this string value1, string value2)
        { 
            Console.WriteLine(value1 + value2); 
        }

        class B
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
}

namespace InterfaceLink
{
    // Интерфейсы могут хранить свойства и методы
    public interface IMyInterface
    {
        void F1();
        void F2();
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

    class B
    {
        static void Main_()
        {
            A objA = new A();
            IMyInterface inter;
            inter = objA;
            inter.F1();
            inter.F2();
        }
    }
}

namespace Solid
{
    // Single Responsibility    большие классы разделять на малые, чтобы каждый выполнял конкретную задачу
    // Open Closed              методы класса должны быть открыты для расширения, но закрыты для модификации
    // Liskov Substitution      объекты можно заменить их наследниками без изменения свойств программы
    // Interface Segregation    не создавать интерфейсы с большим числом методов
    // Dependency Invertion     зависимости кода должны строиться от абстракции
    interface IDependencyInjection
    {
        void F1();
    }

    class A : IDependencyInjection
    {
        public void F1() 
        { 
            Console.WriteLine("1"); 
        }
    }

    class B : IDependencyInjection
    {
        public void F1() 
        { 
            Console.WriteLine("2"); 
        }
    }

    class C
    {
        private readonly IDependencyInjection _di;

        public C(IDependencyInjection di)
        {
            _di = di;
        }

        public void F2()
        {
            _di.F1();
        }
    }

    class Program
    {
        static void Main_()
        {
            // A меняем на B при необходимости
            IDependencyInjection dependencyInjection = new A();
            C c = new C(dependencyInjection);
            c.F2();
        }
    }
}

namespace AbstractAndStaticClass
{
    // Статический класс нужен для группировки логически связанных членов
    // От него нельзя наследоваться и он не может реализовывать интерфейс
    static class A
    {
        static public void F1() { Console.WriteLine("A"); }
    }

    // Абстрактные методы могут быть лишь в абстрактных классах, не могут иметь тело и должны быть переопределены наследником
    // В отличие от интерфейсов, могут иметь поля, определение методов, конструктор, неявно вызываемый конструктором дочернего класса
    abstract class B
    {
        public void F2() { Console.WriteLine("B"); }
        public abstract void F3();
    }

    class Program : B
    {
        static void Main_()
        {
            A.F1();

            B objB = new Program();
            objB.F2();
            objB.F3();
        }

        public override void F3() { Console.WriteLine("Program"); }
    }
}

namespace OOP
{
    // Множественное наследование запрещено, но его можно реализовать через интерфейсы
    class A
    {
        public void F1() { Console.WriteLine("F1"); }
    }

    interface IB
    {
        void F2();
    }

    class B : IB
    {
        public void F2() { Console.WriteLine("F2"); }
    }

    class C : A, IB
    {
        B objB = new B();
        public void F2() { objB.F2(); }
    }

    class Program
    {
        static void Main_()
        {
            C objC = new C();
            objC.F1();
            objC.F2();
        }
    }
}

namespace Polymorphism
{
    class A
    {
        public A() { Console.WriteLine("ctor A"); }

        public virtual void F1() { Console.WriteLine("A1"); }
        public void F2() { Console.WriteLine("A2"); }
        public virtual void F3() { Console.WriteLine("A3"); }
        public void F4() { F1(); }
        public void F5() { Console.WriteLine("A5"); }
        public void F6() { Console.WriteLine("A6"); }
    }

    class B : A
    {
        public B() { Console.WriteLine("ctor B"); }

        public override void F1() { Console.WriteLine("B1"); }
        public void F2() { Console.WriteLine("B2"); }
        public new void F3() { Console.WriteLine("B3"); }
        public new virtual void F5() { Console.WriteLine("B5"); }
        public void F6() { Console.WriteLine("B6"); }
    }

    class Program
    {
        static void Main_()
        {
            // upcast
            A obj = new B();    // ctorA ctorB
            obj.F1();           // B1
            obj.F2();           // A2
            obj.F3();           // A3
            obj.F4();           // B1
            obj.F5();           // A5
            obj.F6();           // A6

            Console.WriteLine("==");

            // downcast
            //B obj2 = new A() as B;  // ctorA
            B obj2 = obj as B;
            obj.F1();           // B1
            obj.F2();           // A2
            obj.F3();           // A3
            obj.F4();           // B1
            obj.F5();           // A5
            obj.F6();           // A6
        }
    }
}

namespace TypeConversion
{
    class A { }
    class B { }

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
                A a = new A();
                B b = new B();
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
        public Person(string name)
        {
            Name = name;
        }
        public void Display()
        {
            Console.WriteLine($"Person {Name}");
        }
    }

    class Employee : Person
    {
        public string Company { get; set; }
        public Employee(string name, string company) : base(name)
        {
            Company = company;
        }
    }

    class Client : Person
    {
        public string Bank { get; set; }
        public Client(string name, string bank) : base(name)
        {
            Bank = bank;
        }
    }

    class Program
    {
        static void Main_()
        {
            // ВОСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (upcasting)
            // Переменной person типа Person присваивается ссылка на объект Employee
            // Чтобы сохранить ссылку на объект одного класса в переменную другого класса,
            // необходимо выполнить преобразование от Employee к Person
            // Employee наследуется от Person, поэтому выполняется восходящее преобразование,
            // в итоге employee и person указывают на один объект,
            // но переменной person доступна только та часть, которая представляет функционал Person
            Employee emp = new Employee("Dima", "Company");
            Person per = emp;
            Console.WriteLine(per.Name);

            Person per2 = new Client("Bob", "Bank");
            Console.WriteLine(per2.Name);

            // object - базовый тип для всех остальных типов, преобразование к нему
            // происходит автоматически
            object per3 = new Employee("Name", "Company");  // не можем получить доступ ни к одному полю
            object per4 = new Client("Name", "Company");    // не можем получить доступ ни к одному полю
            object per5 = new Person("Name");               // можем получить доступ к полю Name
            Console.WriteLine(per.Name);

            // НИСХОДЯЩИЕ ПРЕОБРАЗОВАНИЯ (downcasting)
            // Кроме восходящих преобразований от производного к базовому типу есть нисходящие преобразования
            // от базового к производному
            // Чтобы обратиться к функционалу типа Employee через переменную типа Person, нужно применить явное преобразования,
            // указав в скобках тип, к которому нужно выполнить преобразование
            Employee emp2 = new Employee("Tom", "Microsoft");
            Person per6 = emp2;                 // преобразование от Employee к Person
                                                //Employee emp2 = per6;             // так нельзя, нужно явное преобразование
            Employee emp3 = (Employee)per6;     // преобразование от Person к Employee
            Console.WriteLine(emp3.Company);

            // obj присвоена ссылка на Employee, поэтому можем преобразовать obj к любому типу,
            // который располагается в иерархии классов между object и Employee
            object obj = new Employee("Bill", "Microsoft");
            Employee emp4 = (Employee)obj;

            Person person = new Client("Sam", "ContosoBank");
            Client client = (Client)person;

            object obj2 = new Employee("Bill", "Microsoft");
            ((Person)obj2).Display();               // преобразование к Person для вызова метода Display
            ((Employee)obj2).Display();             // эквивалентно предыдущей записи
            string comp = ((Employee)obj2).Company; // преобразование к Employee, чтобы получить свойство Company

            // Ниже получим ошибку, т.к. obj3 хранит ссылку на объект Employee, а не Client
            object obj3 = new Employee("Bill", "Microsoft");
            string bank = ((Client)obj3).Bank;

            // СПОСОБЫ ПРЕОБРАЗОВАНИЙ
            // as пытается преобразовать выражение к определенному типу и не выбрасывает исключение
            // В случае неудачи вернет null
            Person per7 = new Person("Tom");
            Employee emp5 = per7 as Employee;
            if (emp == null)
            {
                Console.WriteLine("Преобразование прошло неудачно");
            }
            else
            {
                Console.WriteLine(emp.Company);
            }

            // is - проверка допустимости преобразования
            // person is Employee проверяет, является ли person объектом типа Employee
            // В данном случае не является, поэтому вернет false
            Person per8 = new Person("Tom");
            if (per8 is Employee)
            {
                Employee emp6 = (Employee)per8;
                Console.WriteLine(emp6.Company);
            }
            else
            {
                Console.WriteLine("Преобразование не допустимо");
            }
        }
    }
}

namespace Generics
{
    class A<T>
    {
        public void Display<B>() { }
    }

    class B<T>
    {
        T x;

        public void Display<B>(B y)
        {
            Console.WriteLine(x.GetType() + " " + y.GetType());
        }
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

    class Program
    {
        static int Displ(int val)
        {
            Console.WriteLine(val);
            return 0;
        }

        static void Main_()
        {
            A<int> objA = new A<int>();
            objA.Display<int>();

            B<bool> objB = new B<bool>();
            objB.Display<bool>(true);

            Del<int, int> del = new Del<int, int>(Displ);
            del(1);

            C<int> c1 = new C<int>(5);
            D<bool, string> c2 = new D<bool, string>(true, "false");
        }
    }
}

namespace Iterator
{
    // Итератор возвращает все члены коллекции от начала до конца
    // Нужен для сокрытия коллекции и способа ее обхода, т.к. массивы, структуры и т.д. обходятся по разному,
    // без итераторов пришлось бы для каждой коллекции знать, как ее обходить
    // В отличие от стандартных коллекций (Dictionary), пользовательские (Итераторы) могут иметь больше возможностей
    class Program
    {
        static void Main_()
        {
            A objA = new A();

            foreach (var i in objA)  // Цикл автоматически вызовет метод GetEnumerator
                Console.WriteLine(i);
        }
    }

    // Интерфейс IEnumerable перебирает элементы коллекции
    class A : IEnumerable
    {
        int[] arr = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

        public IEnumerator GetEnumerator()
        {
            foreach (var i in arr)
                yield return arr[i];
        }
    }
}

namespace Cortege
{
    // Через NuGet нужно установить System.ValueTuple
    // Нужны для возвращения из функции двух и боле значений
    class Program
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
}

namespace Linq
{
    // IEnumerable определяет метод GetEnumerator, который возвращает IEnumerator. IEnumerator показывает элементы коллекции
    // Каждый экземпляр Enumerator находится в определенной позиции и может предоставить этот элемент (IEnumerator.Current)
    // или перейти к следующему (IEnumerator.MoveNext). Цикл foreach использует IEnumerator
    class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main_()
        {
            IEnumerable<A> collA = new List<A>()
        {
            new A { Id = 2, Name = "Dima" },
            new A { Id = 4, Name = "Champion" },
            new A { Id = 1, Name = "Developer" },
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
}

namespace Indexator
{
    // Индексаторы — свойства с параметрами
    // В классе может быть более одного индексатора, они должны отличаться типом или количеством индексов
    class A
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

        public Car this[string indexNumber] // Данный индексатор позволяет обращаться к объектам коллекции Car
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

    class Program
    {
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

            Console.WriteLine(parking["12132EA"].Name);              // Выведет Lada
            parking[1] = new Car() { Name = "BMW", Number = "156742XN" };   // Устанавливаем значение через индексатор 
            Console.WriteLine(parking[1]);
        }
    }
}

namespace MemoryClean
{
    // Большинство объектов программы относятся к управляемым и очищаются сборщиком мусора, но есть неуправляемые
    // объекты (низкоуровневые файловые дескрипторы, сетевые подключения), которые сборщик мусора не может удалить    
    // Освобождение неуправляемых ресурсов подразумевает реализацию одного из двух механизмов
    // - Финализатор
    // - Реализация интерфейса IDisposable

    // Финализатор вызывается непосредственно перед сборкой мусора
    // Программа может завершиться до того, как произойдет сборка мусора, поэтому финализатор может быть не вызван
    class A
    {
        ~A() { Console.WriteLine("Destructor"); }
    }

    // На деле сборщик мусора вызывает не финализатор, а метод Finalize класса A, потому что компилятор компилирует
    // финализатор в конструкцию
    //protected override void Finalize()
    //{
    //    try { /* здесь идут инструкции деструктора */ }
    //    finally { base.Finalize(); }
    //}

    // IDisposable объявляет один единственный метод Dispose, освобождающий неуправляемые ресурсы, он вызывает финализатор немедленно
    public class B : IDisposable
    {
        public void Dispose() { Console.WriteLine("Dispose"); }
    }

    class Program
    {
        // try finally гарантирует, что в случае исключения Dispose освободит ресурсы
        // Можно использовать using
        private static void Test()
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

        static void Main_()
        {
            Test();
        }
    }
}

namespace Exceptions
{
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
    public static void TriggerException(bool isTrigger)
    {
        throw new ArgumentException();
    }
}

class Program
{
    public static void F1()
    {
        // Программа выведет 1 2 3 4 6
        try
        {
            // Сначала выполнятся try catch finally данного уровня, даже если возникнет исключение
            try
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
            finally // Вызовется в любом случае
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
    static void Main_()
    {
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
}

namespace Events
{
    public class E
    {
        // В отличие от делегатов, события более защищены от непреднамеренных изменений,
        // тогда как делегат может быть случайно обнулен, если использовать '=' вместо '+='
        // Событие можно использовать лишь внутри класса, в котором оно определено
        // Пример 1
        public delegate void MyDel();
        public event MyDel TankIsEmpty;     // Объявляем событие для нашего типа делегата
        public void F1() { TankIsEmpty(); } // Метод вызывает событие

        static void EventHandlerMethod1() 
        { 
            Console.WriteLine("1"); 
        }

        static void EventHandlerMethod2()
        { 
            Console.WriteLine("2"); 
        }

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
    public class A
    {
        public event del ev = null; // создаем событие
        public void InvokeEvent()   // здесь можно проверять, кто вызывает событие
        { ev.Invoke(); }
    }

    class B
    {
        static private void F1() 
        {
            Console.WriteLine("0");
        }

        static private void F2() 
        {
            Console.WriteLine("1");
        }

        static void Main_()
        {
            A objA = new A();
            // присваиваем событию делегат, на который подписываем метод
            objA.ev += new del(F1); // смысл
            objA.ev += F2;          // одинаковый
            objA.InvokeEvent();     // напрямую запрещено вызывать события через objA.ev.Invoke()
        }
    }

    public class C
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
        private static void Method0()
        { 
            Console.WriteLine("0"); 
        }

        private static void Method1()
        { 
            Console.WriteLine("1");
        }

        static void Main_()
        {
            C objC = new C();
            objC.Event += Method0;
            objC.Event += Method1;
            objC.Event += delegate { 
                Console.WriteLine("Anonimnuy method"); 
            };
            // Отписать анонимный метод нельзя, код ниже не сработает
            objC.Event -= delegate {
                Console.WriteLine("Anonimnuy method"); 
            };
            objC.InvokeEvent();
        }
    }
}

namespace Delegates
{
    // На делегаты можно подписать один и более методов, а затем при вызове делегата будут вызваны эти методы
    // Делегаты можно суммировать
    // Сигнатуры делегата и его методов должны совпадать
    // Если на делегат подписано несколько методов, возвращающих значение, то через делегат мы получим значение лишь последнего метода

    // Пример 1
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

    delegate void Del4();

    class Program
    {
        static void Main_()
        {
            Del4 del1 = new Del4(A.Display1);
            Del4 del2 = new Del4(A.Display2);
            Del4 del3 = del1 + del2;
            del3(); // 1 2
                    // del.Invoke();  // аналогичный, но более наглядный способ вызова делегата
        }

        static void F1() 
        { 
            Console.WriteLine("F1"); 
        }

        static void F2() 
        {
            Console.WriteLine("F2"); 
        }

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

            del = delegate (int val) { 
                Console.WriteLine("0"); return val * 2; 
            };

            del = (val) => { 
                Console.WriteLine("0"); return val * 2;
            };

            del = val => val * 2;   // 1е значение - аргумент, 2е - возвращаемое значение

            Console.WriteLine(del(5));
        }
    }
}
