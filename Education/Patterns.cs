using System;
using System.Collections;
using System.Collections.Generic;

// Порождающие

namespace FactoryMethod
{
    // Создаем отдел производства пакетов сока и предупреждаем подотделы, что каждый должен реализовать фабричный метод
    // Каждый подотдел заведует своим типом сока
    // Если потребуется пакет апельсинового сока, скажем об этом отделу по производству апельсинового сока,
    // а он скажет отделу по созданию пакетов сока, чтобы сделал пакет и залил этот сок
    // Сперва реализуют фабричный метод, а по мере усложнения кода его преобразуют в фабрику, строитель или прототип
    // При использовании фабричного метода каждый объект является фабрикой
    // Используется, когда заранее неизвестно, объекты каких типов необходимо создавать, когда система должна быть независимой
    // от процесса создания новых объектов - в нее можно легко вводить новые классы, объекты которых система должна создавать,
    // когда создание новых объектов необходимо делегировать из базового класса наследникам
    abstract class House { }

    abstract class Developer            // Класс строительной компании
    {
        public string Name { get; set; }
        public Developer(string name) { Name = name; }
        abstract public House Create(); // Фабричный метод
    }

    class PanelHouse : House
    {
        public PanelHouse() { Console.WriteLine("Panel house was build"); }
    }

    class WoodHouse : House
    {
        public WoodHouse() { Console.WriteLine("Wood house was build"); }
    }

    class PanelDeveloper : Developer
    {
        public PanelDeveloper(string name) : base(name) { }
        public override House Create() { return new PanelHouse(); }
    }

    class WoodDeveloper : Developer
    {
        public WoodDeveloper(string name) : base(name) { }
        public override House Create() { return new WoodHouse(); }
    }

    public class Program
    {
        static void Main_()
        {
            Developer developer = new PanelDeveloper("Panel building company");
            House housePanel = developer.Create();

            developer = new WoodDeveloper("Wood building company");
            House houseWood = developer.Create();
        }
    }
}

namespace AbstractFactory
{
    // Предоставляет интерфейс для создания взаимосвязанных или взаимозависимых объектов
    // Фабрика получает запрос на создание продукта и возвращает его
    // Клиент не знает, как создается продукт
    // Способ производства продукта может быть изменен, но способ получения - нет
    // Одна фабрика должна создавать один вид продукта
    abstract class Weapon { public abstract void Hit(); }
    class Arbalet : Weapon { public override void Hit() { Console.WriteLine("Shoot from arbalet"); } }
    class Sword : Weapon { public override void Hit() { Console.WriteLine("Hit by sword"); } }
    abstract class Movement { public abstract void Move(); }
    class FlyMovement : Movement { public override void Move() { Console.WriteLine("Fly"); } }
    class RunMovement : Movement { public override void Move() { Console.WriteLine("Run"); } }

    // Класс абстрактной фабрики
    abstract class HeroFactory
    {
        public abstract Movement CreateMovement();
        public abstract Weapon CreateWeapon();
    }

    // Через фабрику создаем летающего эльфа с арбалетом
    class ElfFactory : HeroFactory
    {
        public override Movement CreateMovement() { return new FlyMovement(); }
        public override Weapon CreateWeapon() { return new Arbalet(); }
    }

    // .. и бегущего воина с мечом
    class WarriorFactory : HeroFactory
    {
        public override Movement CreateMovement() { return new RunMovement(); }

        public override Weapon CreateWeapon() { return new Sword(); }
    }

    class Hero
    {
        private Weapon weapon;
        private Movement movement;
        public Hero(HeroFactory factory)
        {
            weapon = factory.CreateWeapon();
            movement = factory.CreateMovement();
        }

        public void Run() { movement.Move(); }
        public void Hit() { weapon.Hit(); }
    }

    class Program
    {
        static void Main_()
        {
            Hero elf = new Hero(new ElfFactory());
            elf.Hit();
            elf.Run();

            Hero warrior = new Hero(new WarriorFactory());
            warrior.Hit();
            warrior.Run();
        }
    }
}

namespace Builder
{
    // Связан с паттерном Фабрика, содержит операции создания объекта (пакета сока)
    // Мы говорим "хочу сока", а строитель запускает цепочку операций (создание пакета, заправка сока)
    // Если потребуется другой сок, говорим об этом, а строитель позаботится об остальном (какие-то процессы повторит, какие-то сделает заново)
    // Процессы в строителе можно менять (изменить рисунок на упаковке), но потребителю сока этого
    // знать не нужно, он будет получать сок по тому же запросу
    // Фабрика - это автомат по продаже напитков, в нем уже есть всё готовое, мы только говорим, что нам нужно (нажимаем кнопку)
    // Строитель - это завод, который производит напитки и может собирать сложные объекты из более простых
    // Разделяет процесс создания объекта на разные этапы
    // Нужно, когда процесс создания не должен зависеть от того, из каких частей
    // состоит объект и как эти части между собой связаны
    abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void BuildPartC();
        public abstract void GetResult();
    }

    class Product
    {
        public List<object> parts = new List<object>();
        public void Add(string part) { parts.Add(part); }
    }

    class ConcreteBuilder : Builder
    {
        Product product = new Product();
        public override void BuildPartA() { product.Add("Part A"); }
        public override void BuildPartB() { product.Add("Part B"); }
        public override void BuildPartC() { product.Add("Part C"); }
        public override void GetResult()
        {
            foreach (var p in product.parts)
                Console.WriteLine(p);
        }
    }

    class Director
    {
        Builder _builder;
        public Director(Builder builder) { _builder = builder; }

        public void Costruct()
        {
            _builder.BuildPartA();
            _builder.BuildPartB();
            _builder.BuildPartC();
        }
    }

    class Program
    {
        static void Main_()
        {
            Builder builder = new ConcreteBuilder();
            Director director = new Director(builder);
            director.Costruct();
            builder.GetResult();
        }
    }
}

namespace Prototype
{
    // Создаем объект прототип и на его онове другие такие-же объекты - клоны
    abstract class Prototype
    {
        public int Id { get; set; }
        public Prototype(int id) { Id = id; }
        public abstract void Clone();
    }

    class ConcretePrototypeBox : Prototype
    {
        public ConcretePrototypeBox(int id) : base(id) { }
        public override void Clone() { Console.WriteLine($"Box with id {Id} was cloned"); }
    }

    class ConcretePrototypeSphere : Prototype
    {
        public ConcretePrototypeSphere(int id) : base(id) { }
        public override void Clone() { Console.WriteLine($"Sphere with id {Id} was cloned"); }
    }

    class Program
    {
        static void Main_()
        {
            Prototype protypeOfBox = new ConcretePrototypeBox(1);
            protypeOfBox.Clone();
            protypeOfBox.Clone();

            Prototype protypeOfSphere = new ConcretePrototypeSphere(2);
            protypeOfSphere.Clone();
            protypeOfSphere.Clone();
        }
    }
}

namespace Singleton
{
    // Гарантирует, что у класса есть только один экземпляр, и предоставляет к нему глобальную точку доступа
    public class Singleton
    {
        private Singleton() { }
        private static Singleton instance = new Singleton();
        public static Singleton getInstance() { return instance; }
    }
}

// Структурные

namespace Adapter
{
    // Переходник между японской и европейской розеткой
    // Может быть преходноком как в одну сторону, так и в обе
    interface IRozetkaEurope { void GetElectricityEuropeanRozetka(); }

    class TV : IRozetkaEurope
    {
        public void GetElectricityEuropeanRozetka() 
            { Console.WriteLine("Electricity is going, tv"); }
    }

    interface IRozetkaJapan { void GetElectricityJapaneseRozetka(); }

    class Refregerator : IRozetkaJapan
    {
        public void GetElectricityJapaneseRozetka()
            { Console.WriteLine("Electricity is going, refregerator"); }
    }

    class JapanToEuropeanAdatper : IRozetkaEurope
    {
        Refregerator _refregerator;

        public JapanToEuropeanAdatper(Refregerator refregerator)
        {
            _refregerator = refregerator;
        }

        public void GetElectricityEuropeanRozetka()
            { _refregerator.GetElectricityJapaneseRozetka(); }
    }

    class Program
    {
        static void Main_()
        {
            var tv = new TV();                      // Подключаем ТВ к европейской розетке
            var refregerator = new Refregerator();  // Подключаем холодильник к японской розетке

            tv.GetElectricityEuropeanRozetka();
            refregerator.GetElectricityJapaneseRozetka();

            // Через адаптер подключаем холодильник к европейской розетке
            IRozetkaEurope rozetkaEurope = new JapanToEuropeanAdatper(refregerator);
            rozetkaEurope.GetElectricityEuropeanRozetka();
        }
    }
}

namespace Bridge
{
    // В авто есть абстракция в виде руля
    // При изготовлении авто задаем общие правила взаимодействия и можем одинаково управлять каждым
    // Мост здесь - это два объекта: конкретный авто и правила взаимодействия с ним
    abstract class Implementor { public abstract void WheelImplementation(); }

    abstract class Abstraction
    {
        protected Implementor implementor;
        public Abstraction(Implementor imp) { this.implementor = imp; }
        public virtual void WheelGeneral() { implementor.WheelImplementation(); }
    }

    class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(Implementor imp) : base(imp) { }
        public override void WheelGeneral() { base.WheelGeneral(); }
    }

    class Citroen : Implementor
    {
        public override void WheelImplementation() { Console.WriteLine("Citroen"); }
    }

    class Hyundai : Implementor
    {
        public override void WheelImplementation() { Console.WriteLine("Hyundai"); }
    }

    class A
    {
        static void Main_()
        {
            Abstraction abstraction;
            abstraction = new RefinedAbstraction(new Citroen());
            abstraction.WheelGeneral();
            abstraction = new RefinedAbstraction(new Hyundai());
            abstraction.WheelGeneral();
        }
    }
}

namespace Composite
{
    // Компоновщик минимизирует различия в управлении группами объектов и индивидуальными объектами
    // Например, существует алгоритм управления роботами, определяющий способ управления
    // Не важно, кому отдается команда - одному роботу или группе
    // В алгориитм нельзя включить команду, которую может
    // исполнить только один робот, но не может исполнить группа
    abstract class Component
    {
        protected string name;
        public Component(string name) { this.name = name; }
        public abstract void Operation();
        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract Component GetChild(int index);
    }

    class Leaf : Component
    {
        public Leaf(string name) : base(name) { }
        public override void Operation() { Console.WriteLine(name); }

        // Leaf (лист дерева) - это компонент, не имеющий потомков
        // Поэтому при добавлении/удалении элемента из листа генерируем исключение
        public override void Add(Component component) { throw new InvalidOperationException(); }
        public override void Remove(Component component) { throw new InvalidOperationException();}
        public override Component GetChild(int index) { throw new InvalidOperationException();}
    }

    class Composite : Component
    {
        ArrayList nodes = new ArrayList();
        public Composite(string name) : base(name) { }
        
        public override void Operation() // Метод рекурсивно обходит все дерево
        {
            Console.WriteLine(name);

            foreach (Component component in nodes)
                component.Operation();
        }

        public override void Add(Component component) { nodes.Add(component); }
        public override void Remove(Component component) { nodes.Remove(component); }
        public override Component GetChild(int index) { return nodes[index] as Component; }
    }

    class A
    {
        static void Main_()
        {
            Component root = new Composite("root");
            Component branch1 = new Composite("b1");
            Component branch2 = new Composite("b2");
            Component leaf1 = new Composite("l1");
            Component leaf2 = new Composite("l2");
            
            root.Add(branch1); // Создание элементов дерева и его рекурсивный обход
            root.Add(branch2);
            branch1.Add(leaf1);
            branch2.Add(leaf2);
            root.Operation();
        }
    }
}

namespace Decorator
{
    // Декоратор
    // Пиццерия готовит разные пиццы с разными добавками
    // Есть два вида пиццы и два вида добавок, в зависимости от комбинации меняется стоимость
    abstract class Pizza
    {
        public string Name { get; set; }
        public Pizza(string name) { this.Name = name; }
        public abstract int GetCost();
    }

    class ItalianPizza : Pizza
    {
        public ItalianPizza() : base("Italian pizza") { }
        public override int GetCost() { return 10; }
    }

    class BulgerianPizza : Pizza
    {
        public BulgerianPizza() : base("Bulgerian pizza") { }
        public override int GetCost() { return 8; }
    }

    // Декоратор класса Pizza -  наследуется от него
    abstract class PizzaDecorator : Pizza
    {
        protected Pizza pizza; // Ссылка на декорируемый объект
        public PizzaDecorator(string name, Pizza pizza) : base(name) { this.pizza = pizza; }
    }
    // Добавляем новый функционал
    class TomatoPizza : PizzaDecorator
    {
        public TomatoPizza(Pizza pizza) : base(pizza.Name + ", with tomatos", pizza) { }
        public override int GetCost() { return pizza.GetCost() + 3; }
    }
    // Добавляем новый функционал
    class CheesePizza : PizzaDecorator
    {
        public CheesePizza(Pizza pizza) : base(pizza.Name + ", with cheeze", pizza) { }
        public override int GetCost() { return pizza.GetCost() + 5; }
    }

    class Program
    {
        static void Main_()
        {
            Pizza bulgerianPizza = new BulgerianPizza();
            var costBulgerianPizza = bulgerianPizza.GetCost();

            bulgerianPizza = new TomatoPizza(bulgerianPizza);   // Декорируем пиццу томатом
            var costBulgerianPizzaWithTomato = bulgerianPizza.GetCost();

            bulgerianPizza = new CheesePizza(bulgerianPizza);   // Декорируем пиццу сыром
            var costBulgerianPizzaWithTomatoWithCheeae = bulgerianPizza.GetCost();
        }
    }
}

namespace Facade
{
    // Если бы управление автомобилем происходило так - нажать одну кнопку чтобы подать питание с аккумулятора,
    // другую чтобы подать питание на инжектор, третью чтобы включить генератор, это было бы сложно
    // Такие наборы действий заменяются простыми - поворот ключа зажигания будет фасадом
    class Facade
    {
        SubsystemA subsystemA = new SubsystemA();
        SubsystemB subsystemB = new SubsystemB();
        SubsystemC subsystemC = new SubsystemC();

        public void OperationABC()
        {
            subsystemA.OperationA();
            subsystemB.OperationB();
            subsystemC.OperationC();
        }
    }

    class SubsystemA { public void OperationA() { Console.WriteLine("Operation A"); } }
    class SubsystemB { public void OperationB() { Console.WriteLine("Operation B"); } }
    class SubsystemC { public void OperationC() { Console.WriteLine("Operation C"); } }

    class A
    {
        static void Main_()
        {
            Facade facade = new Facade();
            facade.OperationABC();
        }
    }
}

namespace Flyweight
{
    // Один актер может исполнить много разных ролей
    // Применяется, когда приложение использует много однообразных объектов,
    // из-за чего выделяется большое количество памяти
    abstract class Flyweight { public abstract void Greeting(string speech); }

    class Actor : Flyweight
    {
        public override void Greeting(string speech) { Console.WriteLine(speech); }
    }
    
    class Role1 : Flyweight
    {
        Flyweight flyweight;
        public Role1(Flyweight flyweight) { this.flyweight = flyweight; }
        public override void Greeting(string speech) { this.flyweight.Greeting(speech); }
    }
    
    class Role2 : Flyweight
    {
        Flyweight flyweight;
        public Role2(Flyweight flyweight) { this.flyweight = flyweight; }
        public override void Greeting(string speech) { this.flyweight.Greeting(speech);}
    }

    class A
    {
        static void Main_()
        {
            Actor actor = new Actor();

            Role1 role1 = new Role1(actor); // Актер играет одну роль
            role1.Greeting("I'm actor1, play role1");

            Role2 role2 = new Role2(actor); // Тот-же актер играет уже другую роль
            role2.Greeting("I'm actor1, play role2");
        }
    }
}

namespace Proxy
{
    // Сначала отрабатывает прокси, потом основной объект и опять прокси
    // Прокси может замещать или дополнять объект
    public abstract class Subject { public abstract void Request(); }

    public class RealSubject : Subject
    {
        public override void Request() { Console.WriteLine("RealSubject"); }
    }

    public class ProxySubject : Subject
    {
        private RealSubject realSubject;
        public override void Request()
        {
            Console.WriteLine("ProxySubject begin");
            if (realSubject == null)
                realSubject = new RealSubject();
            realSubject.Request();
            Console.WriteLine("ProxySubject end");
        }
    }

    public class Program
    {
        public static void Main_()
        {
            ProxySubject proxy = new ProxySubject();
            proxy.Request();
        }
    }
}

// Поведенческие

namespace ChainOfResponsibility
{
    // Требуется получить справку из банка, но не ясно, кто должен ее дать
    // Приходим в банк, там говорят что нужно идти в другое отделение
    // Идем в другое, там отправляют в региональное отделение и там получаем справку
    // Паттерн реализует цепочку, отдельные объекты которой должны обработать запрос
    // Запрос может быть обработан в первом отделении или в нескольких
    // Позволяет избежать привязки отправителя запроса к получателю,
    // давая шанс обработать запрос нескольким объектам
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;

            // Возврат обработчика позволит связать обработчики так: monkey.SetNext(squirrel)
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null) return _nextHandler.Handle(request);
            else return null;
        }

        class MonkeyHandler : AbstractHandler
        {
            public override object Handle(object request)
            {
                if ((request as string) == "Banana") return $"Monkey: I love {request}";
                else return base.Handle(request);
            }
        }

        class SquirrelHandler : AbstractHandler
        {
            public override object Handle(object request)
            {
                if ((request as string) == "Nut") return $"Sqiurrel: I love {request}";
                else return base.Handle(request);
            }
        }

        class DogHandler : AbstractHandler
        {
            public override object Handle(object request)
            {
                if ((request as string) == "Meat") return $"Dog: I love {request}";
                else return base.Handle(request);
            }
        }

        class Client
        {
            public static void ClientCode(AbstractHandler handler)
            {
                foreach(var food in new List<string> { "Nut", "Banana", "Meat" })
                {
                    Console.Write($"Who wants {food}?");
                    var result = handler.Handle(food);

                    if (result != null) Console.WriteLine($"    {result}");
                    else Console.WriteLine($"    {food} left untouched");
                }
            }
        }

        class Program
        {
            static void Main_()
            {
                var monkey = new MonkeyHandler();
                var squirrel = new SquirrelHandler();
                var dog = new DogHandler();

                monkey.SetNext(squirrel).SetNext(dog);  // Создаем цепочку

                // Можем отправить запрос любому обработчику, а не только первому в цепочке
                //Client.ClientCode(monkey);      // Обходим цепочку, начиная с обезьяны
                //Client.ClientCode(squirrel);    // Обходим цепочку, начиная с белки
                Client.ClientCode(dog);           // Обходим цепочку, начиная с белки
            }
        }
    }
}
//! Остановился здесь
namespace Command
{
    // В ресторане к нам (клиент) подходит официант, записывает заказ на бумажку (команда)
    // и отдает ее повару (получатель)
    // Клиент и получатель не знают друг об друге

    // Интерфейс команды имеет единственный метод для запуска
    public interface ICommand { void Execute(); }

    // Некоторые команды сами выполняют простые операции
    class SimpleCommand : ICommand
    {
        private string _payload = "";

        public SimpleCommand(string payload)
        {
            _payload = payload;
        }

        public void Execute() { Console.WriteLine($"I can do {_payload}"); }
    }

    class Receiver
    {
        public void DoSomething(string value) { Console.WriteLine($"Working on {value}"); }

        public void DoSomethingElse(string value) { Console.WriteLine($"and working on {value}"); }
    }

    // Некоторые команды делегируют сложные задачи другим объектам (получателям)
    class ComplexCommand : ICommand
    {
        private Receiver _receiver;
        private string _a;
        private string _b;

        public ComplexCommand(Receiver receiver, string a, string b)
        {
            _receiver = receiver;
            _a = a;
            _b = b;
        }

        // Команды могут делегировать выполнение любым методам-получателям
        public void Execute()
        {
            _receiver.DoSomething(_a);
            _receiver.DoSomethingElse(_b);
        }
    }

    // Получатель
    class Invoker
    {
        private ICommand _onStart;
        private ICommand _onFinish;

        public void SetOnStart(ICommand comand) { _onStart = comand; }
        public void SetOnFinish(ICommand comand) { _onFinish = comand; }

        // Отправитель передает запрос получателю
        public void DoSomethingImportant()
        {
            Console.WriteLine("Inwoker: does anyone want to do something before I begin?");
            if (_onStart is ICommand) _onStart.Execute();

            Console.WriteLine("Inwoker: ... doing some job ...");

            Console.WriteLine("Inwoker: does anyone want to do something after I finish?");
            if (_onFinish is ICommand) _onFinish.Execute();
        }
    }

    class Program
    {
        static void Main()
        {
            var invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Do something simple"));
            var receiver = new Receiver();
            invoker.SetOnFinish(new  ComplexCommand(receiver, "hamburger", "desert"));

            invoker.DoSomethingImportant();
        }
    }
}

namespace Interpreter
{
    // Закладываем часто используемые действия в сокращенный набор слов, чтобы интерпретатор превратил его
    // в более комплексные действия
    // Если знакомому сказать "Литр молока, половинку белого, 200 грамм творога",
    // то мы перечислили набор продуктов, но интерпретатор транслирует это в команду "зайди в магазин и купи следующее"
    class Context
    {
        public string Source { get; set; }
        public char Vocabulary { get; set; }
        public bool Result { get; set; }
        public int Position { get; set; }
    }

    abstract class AbstractExpression { public abstract void Interpreter(Context context); }

    class TerminalExpression : AbstractExpression
    {
        public override void Interpreter(Context context)
        {
            context.Result = context.Source[context.Position] == context.Vocabulary;
        }
    }

    class NonterminalExpression : AbstractExpression
    {
        AbstractExpression nonterminalExpression;
        AbstractExpression terminalExpression;

        public override void Interpreter(Context context)
        {
            if (context.Position < context.Source.Length)
            {
                terminalExpression = new TerminalExpression();
                terminalExpression.Interpreter(context);
                context.Position++;

                if (context.Result)
                {
                    nonterminalExpression = new NonterminalExpression();
                    nonterminalExpression.Interpreter(context);
                }
            }
        }
    }

    class A
    {
        static void Main_()
        {
            Context context = new Context
            {
                Vocabulary = 'a',
                Source = "aa"
            };

            var expression = new NonterminalExpression();
            expression.Interpreter(context);
            Console.WriteLine(context.Result);
        }
    }
}

namespace Iterator
{
    // Предоставляет правила доступа к списку объектов независимо от того, что это за объекты
    // Не важно, какой класс построен и из каких учеников, должны быть общие правила подсчета и
    // обращения как каждому ученику по списку, вроде "13-ый, выйти из строя"
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
        public abstract int Count { get; }
        public abstract object this[int index] { get; set; }
    }

    class ConcreteIterator : Iterator
    {
        private Aggregate aggregate;
        private int current = 0;

        public ConcreteIterator(Aggregate aggregate) { this.aggregate = aggregate; }
        public override object First() { return aggregate[0]; }

        public override object Next()
        {
            if (current++ < aggregate.Count - 1)
                return aggregate.Count;
            else
                return null;
        }

        public override bool IsDone()
        {
            if (current < aggregate.Count)
                return false;

            current = 0;
            return true;
        }

        public override object CurrentItem() { return aggregate[current]; }
    }

    class ConcreteAggregete : Aggregate
    {
        private ArrayList items = new ArrayList();

        public override Iterator CreateIterator() { return new ConcreteIterator(this); }
        public override int Count { get { return items.Count; } }

        public override object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }

    class B
    {
        static void Main_()
        {
            Aggregate a = new ConcreteAggregete();
            a[0] = "A";
            a[1] = "B";
            a[2] = "C";
            a[3] = "D";
            Iterator i = a.CreateIterator();
            
            for (object e = i.First(); !i.IsDone(); e = i.Next())
                Console.WriteLine(e);
        }
    }
}

namespace Mediator
{
    // Есть группа роботов и администратор (медиатор), который ими управляет
    // Нет необходимости взаимодействовать с каждым роботом отдельно, достаточно отдавать команды администратору,
    // а он решит, какие действия выполнить
    // Посредник обеспечивает слабую связанность системы, избавляя объекты от необходимости явно ссылаться
    // друг на друга и позволяя независимо изменять взаимодействия между ними
    // Когда применяется:
    // - Когда имеется множество взаимосвязаных объектов, связи между которыми сложны и запутаны
    // - Когда необходимо повторно использовать объект, но повторное использование затруднено
    //   в силу сильных связей с другими объектами
    abstract class Mediator { public abstract void Send(string msg, Colleague colleague); }

    // Посредник меджу фермером, фабрикой и магазином, которые друг об друге не знают
    class ConcreteMediator : Mediator
    {
        public Farmer Farmer { get; set; }
        public Cannery Cannery { get; set; }
        public Shop Shop { get; set; }
                
        public override void Send(string msg, Colleague colleague) // Посредник получает сообщение
        {
            if (colleague == Farmer)        // Если сообщение передал фермер, значит он прислал нам ящик помидоров
                Cannery.MakeKetchup(msg);   // Пересылаем помидоры фабрике
            
            else if (colleague == Cannery)  // Если сообщение прислала фабрика, значит она из помидроров сделал кетчуп
                Shop.SellKetchup(msg);      // Отправляем кетчуп в магазин
        }
    }

    abstract class Colleague
    {
        protected Mediator mediator;
        public Colleague(Mediator mediator) { this.mediator = mediator; }
    }
    
    class Farmer : Colleague // Фермер выращивает помидоры
    {
        public Farmer(Mediator mediator) : base(mediator) { }

        public void GrowTomato()
        {
            string tomato = "Tomato";
            Console.WriteLine(this.GetType().Name + " raised " + tomato);
            mediator.Send(tomato, this);
        }
    }

    class Cannery : Colleague // Фабрика из помидоров делает кетчуп
    {
        public Cannery(Mediator mediator) : base(mediator) { }

        public void MakeKetchup(string message)
        {
            string ketchup = message + "Ketchup";
            Console.WriteLine(this.GetType().Name + " produced " + ketchup);
            mediator.Send(ketchup, this);
        }
    }
        
    class Shop : Colleague // Магазин продает кетчуп
    {
        public Shop(Mediator mediator) : base(mediator) { }
        public void SellKetchup(string message) { Console.WriteLine(this.GetType().Name + " sold " + message); }
    }

    class A
    {
        static void Main_()
        {
            var mediator = new ConcreteMediator();
            var farmer = new Farmer(mediator);
            var cannery = new Cannery(mediator);
            var shop = new Shop(mediator);

            mediator.Farmer = farmer;
            mediator.Cannery = cannery;
            mediator.Shop = shop;

            farmer.GrowTomato();
        }
    }
}

namespace Memento
{
    // Хранитель - когда просим друга с мобильным телефоном на время записать себе номер, что диктуют нам по телефону,
    // потому что не можем его запомнить сами
    // Нужен, когда объекту требуется сохранить состояние в другом объекте и при необходимости
    // его потом восстановить спросить у друга номера и восстановить состояние, когда мы его знали
    // Не нарушая инкапсуляции, фиксирует и выносит за пределы объекта его состояние,
    // чтобы позднее можно было восстановить в нем объект
    // Когда применяется:
    // - Когда нужно сохранить состояние объекта для возможного последующего восстановления
    // - Когда сохранение состояния должно проходить без нарушения инкапсуляции
    class Man // Человек, который изменяет свое состояние
    {
        public string Сlothes { get; set; }
        public void Dress(Backpack backpack) { Сlothes = backpack.Сontents; }
        public Backpack Undress() { return new Backpack(Сlothes); }
    }
        
    class Backpack // Рюкзак, где человек хранит свою одежду - состояние
    {
        public string Сontents { get; private set; }
        public Backpack(string сontents) { this.Сontents = сontents; }
    }

    // Робот, который держит рюкзак с одежной человека - состоянием
    class Robot { public Backpack Backpack { get; set; } }

    class A
    {
        static void Main_()
        {
            Man David = new Man();
            Robot ASIMO = new Robot();
            David.Сlothes = "Футболка, Джинсы, Кеды";   // Одеваем челоека в одежду
            ASIMO.Backpack = David.Undress();           // Отдаем рюкзак роботу
            David.Сlothes = "Шорты спортивные";
            David.Dress(ASIMO.Backpack);                // Берем у робота рбкзак и одеваем другую одежду
        }
    }
}

namespace Memento_2
{
    class Originator
    {
        public string State { get; set; }
        public void SetMemento(Memento memento) { State = memento.State; }
        public Memento CreateMemento() { return new Memento(State); }
    }

    class Memento
    {
        public string State { get; private set; }
        public Memento(string state) { this.State = state; }
    }

    class Caretaker { public Memento Memento { get; set; } }

    class A
    {
        static void Main_()
        {
            Originator originator = new Originator();
            originator.State = "On";

            Caretaker caretaker = new Caretaker();
            caretaker.Memento = originator.CreateMemento();
            originator.State = "Off";
            originator.SetMemento(caretaker.Memento);
        }
    }
}

namespace Observer
{
    // Если подписались на email рассылку, то email начинает реализовывать паттерн наблюдатель
    // Когда подписываемся на событие, всем подписанным высылается уведомление, а они могут выбрать, как реагировать
    // Определяет зависимость один ко многим между объектами так, что при изменении состояния одного объекта
    // все зависящие от него оповещаются и автоматически обновляются
    // Когда применяется:
    // - Когда система состоит из множества классов, объекты которых должны быть согласованы
    // - Когда схема взаимодействия объектов предполагает две стороны: одна рассылает сообщения,
    //   другая получает сообщения и реагирует на них
    // - Когда существует один объект, рассылающий сообщения, и множество подписчиков, которые получают сообщения
    //   При этом точное число подписчиков заранее неизвестно и может изменяться
    abstract class Observer { public abstract void Update(); }

    abstract class Subject
    {
        ArrayList observers = new ArrayList();
        public void Attach(Observer observer) { observers.Add(observer); }
        public void Detach(Observer observer) { observers.Remove(observer); }
        public void Notify()
        {
            foreach (Observer observer in observers)
                observer.Update();
        }
    }

    class ConcreteObserver : Observer
    {
        string observerState; ConcreteSubject subject;
        public ConcreteObserver(ConcreteSubject subject) { this.subject = subject; }
        public override void Update() { observerState = subject.State; }
    }

    class ConcreteSubject : Subject { public string State { get; set; } }

    class A
    {
        static void Main_()
        {
            ConcreteSubject subject = new ConcreteSubject();
            subject.Attach(new ConcreteObserver(subject));
            subject.Attach(new ConcreteObserver(subject));
            subject.State = "Some State ...";
            subject.Notify();
        }
    }
}

namespace State
{
    // Человек может прибывать в разных состояниях, а объекты могут вести себя по разному в зависимости от состояний
    // Если мы устали, то на фразу "Сходи в магазин" скажем "Не пойду", а если нужно
    // сходить в магазин за пивом, то на "Сходи в магазин" скажем "Уже бегу"
    // Человек один и тот же, а поведение разное
    abstract class State { public abstract void Handle(Context context); }

    class ConcreteStateA : State
    {
        public override void Handle(Context context) { context.State = new ConcreteStateB(); }
    }

    class ConcreteStateB : State
    {
        public override void Handle(Context context) { context.State = new ConcreteStateA(); }
    }

    class Context
    {
        public State State { get; set; }
        public Context(State state) { this.State = state; }
        public void Request() { this.State.Handle(this); }
    }

    class A
    {
        static void Main_()
        {
            Context context = new Context(new ConcreteStateA());
            context.Request();

            context = new Context(new ConcreteStateB());
            context.Request();
        }
    }
}

namespace Strategy
{
    // Используется для выбора различных путей получения результата
    // Говорим "Хочу права, денег мало" - получим права через месяц и с большой тратой ресурсов
    // Говорим "Хочу права, денег много" - получим права завтра
    // Что делал человек, мы не знаем, но задаем начальные условия, а он решает, как себя вести, сам выбирает стратегию
    // Внутри стратегии хранятся различные способы поведения и чтобы выбрать, нужны параметры
    // Как устроена стратегия, нам знать не требуется
    // Когда применяется:
    // - Когда необходимо обеспечить выбор из нескольких вариантов алгоритмов,
    //   которые можно менять в зависимости от условий
    // - Когда необходимо менять поведение объектов на стадии выполнения программы
    abstract class Strategy { public abstract void AlgorithmInterface(); }

    class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface() { Console.WriteLine("ConcreteStrategyA"); }
    }

    class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface() { Console.WriteLine("ConcreteStrategyB"); }
    }

    class Context
    {
        Strategy strategy;
        public Context(Strategy strategy) { this.strategy = strategy; }
        public void ContextInterface() { strategy.AlgorithmInterface(); }
    }

    class A
    {
        static void Main_()
        {
            Context context;
            
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();

            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
        }
    }
}

namespace TemplateMethod
{
    // Определяет основу алгоритма и позволяет подклассам переопределить некоторые шаги, не изменяя структуру
    // Когда применяется:
    // - Когда планируется, что в будущем подклассы будут переопределять алгоритмы без изменения структуры
    // - Когда в классах, реализующих схожий алгоритм, происходит дублирование кода
    abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();

        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
        }
    }

    class ConcreteClass : AbstractClass
    {
        public override void PrimitiveOperation1() { Console.WriteLine("PrimitiveOperation1"); }
        public override void PrimitiveOperation2() { Console.WriteLine("PrimitiveOperation2"); }
    }

    class A
    {
        static void Main_()
        {
            AbstractClass instance = new ConcreteClass();
            instance.TemplateMethod();
        }
    }
}

namespace Visitor
{
    // Паттерн можно сравнить с обследованием в больнице, но посетителем будут врачи
    // Есть больной, которого требуется обследовать и полечить, но так как за разные обследования отвечают разные врачи,
    // мы присылаем к больному врачей в качестве посетителей
    // Правило взаимодействия для больного такое - пригласите врача чтобы он сделал свою работу,
    // врач приходит, обследует и делает необходимое
    // Так можно использовать врачей для разных больных по одним и тем же алгоритмам
    // Паттерн посетитель позволяет определить новую операцию, не изменяя классы этих объектов
    // Когда применяется:
    // - Когда имеется много объектов разных классов с разными интерфейсами,
    //   и требуется выполнить ряд операций над каждым из этих объектов
    // - Когда классам необходимо добавить одинаковый набор операций без изменения этих классов
    // - Когда часто добавляются новые операции к классам, при этом общая структура классов
    //   стабильна и практически не изменяется
    abstract class Visitor
    {
        public abstract void VisitElementA(ConcreteElementA elementA);
        public abstract void VisitElementB(ConcreteElementB elementB);
    }

    class ConcreteVisitor1 : Visitor
    {
        public override void VisitElementA(ConcreteElementA elementA) { elementA.OperationA(); }
        public override void VisitElementB(ConcreteElementB elementB) { elementB.OperationB(); }
    }

    class ConcreteVisitor2 : Visitor
    {
        public override void VisitElementA(ConcreteElementA elementA) { elementA.OperationA(); }
        public override void VisitElementB(ConcreteElementB elementB) { elementB.OperationB(); }
    }

    class ObjectStructure
    {
        ArrayList elements = new ArrayList();
        public void Add(Element element) { elements.Add(element); }
        public void Remove(Element element) { elements.Remove(element); }

        public void Accept(Visitor visitor)
        {
            foreach (Element element in elements)
                element.Accept(visitor); 
        }
    }

    abstract class Element { public abstract void Accept(Visitor v); }

    class ConcreteElementA : Element
    {
        public override void Accept(Visitor v) { v.VisitElementA(this); }
        public void OperationA() { Console.WriteLine("OperationA"); }
    }

    class ConcreteElementB : Element
    {
        public override void Accept(Visitor v) { v.VisitElementB(this); }
        public void OperationB() { Console.WriteLine("OperationB"); }
    }

    class A
    {
        static void Main_()
        {
            var structure = new ObjectStructure();
            structure.Add(new ConcreteElementA());
            structure.Add(new ConcreteElementB());
            structure.Accept(new ConcreteVisitor1());
            structure.Accept(new ConcreteVisitor2());
        }
    }
}
