using System;
using System.Collections;
using System.Collections.Generic;

// Порождающие

namespace FactoryMethod
{
    abstract class House { }
    class WoodHouse : House { public WoodHouse() { Console.WriteLine("Wood house was build"); } }
    class PanelHouse : House { public PanelHouse() { Console.WriteLine("Panel house was build"); } }

    abstract class Developer { abstract public House FactoryMethod(); }
    class WoodDeveloper : Developer { public override House FactoryMethod() { return new WoodHouse(); } }
    class PanelDeveloper : Developer { public override House FactoryMethod() { return new PanelHouse(); } }

    class Program
    {
        static void Main_()
        {
            Developer developer = new WoodDeveloper();
            House woodHouse = developer.FactoryMethod();

            developer = new PanelDeveloper();
            House panelHouse = developer.FactoryMethod();
        }
    }
}

namespace AbstractFactory
{
    abstract class Weapon { public abstract void Hit(); }
    class Arbalet : Weapon { public override void Hit() { Console.WriteLine("Shoot from arbalet"); } }
    class Sword : Weapon { public override void Hit() { Console.WriteLine("Hit by sword"); } }

    abstract class Movement { public abstract void Move(); }
    class Fly : Movement { public override void Move() { Console.WriteLine("Fly"); } }
    class Run : Movement { public override void Move() { Console.WriteLine("Run"); } }

    abstract class HeroesFactory
    {
        public abstract Weapon CreateWeapon();
        public abstract Movement CreateMovement();
    }

    class ElfFactory : HeroesFactory
    {
        public ElfFactory() { Console.WriteLine("Elf was created"); }
        public override Movement CreateMovement() { return new Fly(); }
        public override Weapon CreateWeapon() { return new Arbalet(); }
    }

    class WarriorFactory : HeroesFactory
    {
        public WarriorFactory() { Console.WriteLine("Warrior was created"); }
        public override Movement CreateMovement() { return new Run(); }
        public override Weapon CreateWeapon() { return new Sword(); }
    }

    class Hero
    {
        private Weapon _weapon;
        private Movement _movement;

        public Hero(HeroesFactory factory)
        {
            _weapon = factory.CreateWeapon();
            _movement = factory.CreateMovement();
        }

        public void Run() { _movement.Move(); }
        public void Hit() { _weapon.Hit(); }
    }

    class Program
    {
        static void Main_()
        {
            Hero elf = new Hero(new ElfFactory());
            elf.Hit();
            elf.Run();

            Console.WriteLine();

            Hero warrior = new Hero(new WarriorFactory());
            warrior.Hit();
            warrior.Run();
        }
    }
}

namespace Builder
{
    class Product
    {
        public List<object> parts = new List<object>();
        public void Add(string part) { parts.Add(part); }
    }

    public interface IBuilder
    {
        abstract void BuildPartA();
        abstract void BuildPartB();
        abstract void GetResult();
    }

    abstract class Builder : IBuilder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void GetResult();
    }

    // В программе может быть несколько рализаций строителя
    class ConcreteBuilder : Builder
    {
        Product product = new Product();

        public override void BuildPartA() { product.Add("Part A"); }
        public override void BuildPartB() { product.Add("Part B"); }
        public override void GetResult()
        {
            foreach (var part in product.parts)
                Console.WriteLine(part);
        }
    }

    // Директор отвечает за выполнение шагов построения в определенной последовательности,
    // не является обязательным, можно определить его код на стороне клиента
    class Director
    {
        Builder _builder;
        public Director(Builder builder) { _builder = builder; }
        public void Construnt()
        {
            _builder.BuildPartA();
            _builder.BuildPartB();
        }
    }

    class Program
    {
        static void Main_()
        {
            Builder builder = new ConcreteBuilder();
            Director director = new Director(builder);
            director.Construnt();
            builder.GetResult();
        }
    }
}

namespace Prototype
{
    // Создаем прототип и на его онове клоны
    interface IClone { IClone Clone(); }

    class Box : IClone
    {
        int _width;
        int _height;

        public Box(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public IClone Clone()
        {
            Console.WriteLine("Box was cloned");
            return new Box(_width, _height);
        }
    }

    class Sphere : IClone
    {
        int _radius;
        public Sphere(int radius) { _radius = radius; }
        public IClone Clone()
        {
            Console.WriteLine("Sphere was cloned");
            return new Sphere(_radius);
        }
    }

    class Program
    {
        static void Main_()
        {
            IClone boxPrototype = new Box(20, 40);
            IClone boxClone1 = boxPrototype.Clone();
            IClone boxClone2 = boxPrototype.Clone();

            Console.WriteLine();

            IClone spherePrototype = new Sphere(30);
            IClone sphereCloned1 = spherePrototype.Clone();
            IClone sphereCloned2 = spherePrototype.Clone();
        }
    }
}

namespace Singleton
{
    public class Singleton
    {
        private Singleton() { }
        private static Singleton singleton = new Singleton();
        public static Singleton GetSingleton() { return singleton; }
    }
}

// Структурные

namespace Adapter
{
    class EuropeanRozetka { public string GetElectricity() { return "European rozetka"; } }
    class JapaneseRozetka { public string GetElectricity() { return "Japanese rozetka"; } }

    public interface IEuropeanRozetka { string GetElectricity(); }
    class Adapter : IEuropeanRozetka
    {
        private readonly JapaneseRozetka _japaneseRozetka;
        public Adapter(JapaneseRozetka japaneseRozetka) { _japaneseRozetka = japaneseRozetka; }
        public string GetElectricity() { return _japaneseRozetka.GetElectricity(); }
    }

    class Program
    {
        static void Main_()
        {
            var europeanRozetka = new EuropeanRozetka();
            Console.WriteLine(europeanRozetka.GetElectricity());

            // Если есть японская розетка, используем ее напрямую
            var japaneseRozetka = new JapaneseRozetka();
            Console.WriteLine(japaneseRozetka.GetElectricity());

            // Если нет - используем адаптер
            IEuropeanRozetka adapter = new Adapter(japaneseRozetka);
            Console.WriteLine(adapter.GetElectricity());
        }
    }
}

namespace Bridge
{
    // Разделяет абстракцию и реализацию, позволяя изменять их независимо друг от друга
    // Храним информацию об цвете геометрической фигуры в отдельном классе
    // и при добавлении новых цветов не нужно расширять классы фигур
    public interface IColor { string Color(); }
    class RedColor : IColor { public string Color() { return "RedColor"; } }
    class BlueColor : IColor { public string Color() { return "BlueColor"; } }

    class Figure
    {
        protected IColor _color;
        public Figure(IColor color) { _color = color; }
        public virtual string Paint() { return "Paint all figures with " + _color.Color(); }
    }

    class Sphere : Figure
    {
        public Sphere(IColor color) : base(color) { }
        public override string Paint() { return "Paint sphere with " + _color.Color(); }
    }

    class Program
    {
        static void Main_()
        {
            var figure = new Figure(new RedColor());
            Console.WriteLine(figure.Paint());

            figure = new Sphere(new BlueColor());
            Console.WriteLine(figure.Paint());
        }
    }
}

namespace Composite
{
    // Есть большая коробка, в которую ложим игрушки и еще одну коробку с игрушкой
    // Можем посчитать стоимость всех игрушек, обойдя эту древовидную структуру
    public abstract class Gift
    {
        public string Name { get; }
        public int Price { get; set; }
        public Gift(string name, int price) { Name = name; Price = price; }
        public abstract int TotalPrice();
    }

    public interface IGiftOperations { void Add(Gift gift); }

    public class Box : Gift, IGiftOperations
    {
        private List<Gift> _gifts = new List<Gift>();
        public Box(string name, int price) : base(name, price) { }
        public void Add(Gift gift) { _gifts.Add(gift); }
        public override int TotalPrice()
        {
            int totalPrice = 0;
            Console.WriteLine();
            Console.WriteLine($"{Name} contains: ");
            foreach (var gift in _gifts)
                totalPrice += gift.TotalPrice();
            return totalPrice;
        }

        public class ConcreteGift : Gift
        {
            public ConcreteGift(string name, int price) : base(name, price) { }
            public override int TotalPrice()
            {
                Console.WriteLine($"{Name}, price {Price}");
                return Price;
            }
        }

        class Program
        {
            static void Main_()
            {
                // Считаем стоимость отдельной игрушки не в коробке
                var ball = new ConcreteGift("Ball", 1);
                ball.TotalPrice();

                // Ложим в большую коробку игрушки
                var bigBox = new Box("Big box", 0);
                var phone = new ConcreteGift("Phone", 10);
                var ps5 = new ConcreteGift("PS5", 100);
                bigBox.Add(phone);
                bigBox.Add(ps5);

                // Ложим в маленькую коробку игрушку
                var smallBox = new Box("Small box", 0);
                var laptop = new ConcreteGift("Laptop", 1000);
                smallBox.Add(laptop);

                // Ложим в большую коробку маленькую коробку
                bigBox.Add(smallBox);

                // Считаем стоимость всех игрушек в большой коробке
                Console.WriteLine($"Price of all toys in big box: {bigBox.TotalPrice()}");
            }
        }
    }
}

namespace Decorator
{
    // Пиццерия готовит разные пиццы с разными добавками
    // Есть разные виды пиццы и разные виды добавок
    // В зависимости от комбинации меняется стоимость
    abstract class Pizza
    {
        public string Name { get; set; }
        public Pizza(string name) { Name = name; }
        public abstract int GetCost();
    }

    class ItalianPizza : Pizza
    {
        public ItalianPizza() : base("Italian pizza") { }
        public override int GetCost() { return 10; }
    }

    abstract class PizzaDecorator : Pizza
    {
        protected Pizza Pizza; // Ссылка на декорируемый объект
        public PizzaDecorator(string name, Pizza pizza) : base(name) { Pizza = pizza; }
    }

    class TomatoPizza : PizzaDecorator
    {
        public TomatoPizza(Pizza pizza) : base(pizza.Name + ", with tomatos", pizza) { }
        public override int GetCost() { return Pizza.GetCost() + 3; }
    }

    class CheesePizza : PizzaDecorator
    {
        public CheesePizza(Pizza pizza) : base(pizza.Name + ", with cheeze", pizza) { }
        public override int GetCost() { return Pizza.GetCost() + 5; }
    }

    class Program
    {
        static void Main_()
        {
            Pizza italianPizza = new ItalianPizza();
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");

            italianPizza = new TomatoPizza(italianPizza);   // Декорируем пиццу томатом
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");

            italianPizza = new CheesePizza(italianPizza);   // Декорируем пиццу сыром
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");
        }
    }
}

namespace Facade
{
    // Если бы для управления авто нужно было нажать кнопку подачи питания с аккумулятора,
    // потом кнопки подачи питания на инжектор и кнопки включения генератора, это было-бы сложно
    // Эти действя заменяются поворотом ключа зажигания, а остальное происходит под капотом
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

//!
namespace Flyweight
{

}

namespace Proxy
{
    // Сначала отрабатывает прокси, потом основной объект и опять прокси
    // Прокси может замещать или дополнять объект
    public interface ISubject { void Request(); }
    public class Client { public void ClientCode(ISubject subject) { subject.Request(); } }
    class RealObject : ISubject { public void Request() { Console.WriteLine("Real subject"); } }
    class Proxy : ISubject
    {
        private RealObject _realSubject;
        public Proxy(RealObject realSubject) { _realSubject = realSubject; }
        public void Request()
        {
            ProxyBegin();
            _realSubject.Request();
            ProxyEnd();
        }

        public void ProxyBegin() { Console.WriteLine("Proxy begin"); }
        public void ProxyEnd() { Console.WriteLine("Proxy end"); }
    }

    class Program
    {
        static void Main_()
        {
            var client = new Client();

            Console.WriteLine("Executing code with real subject:");
            var realObject = new RealObject();
            client.ClientCode(realObject);

            Console.WriteLine();

            Console.WriteLine("Executing code with proxy:");
            var proxy = new Proxy(realObject);
            client.ClientCode(proxy);
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
