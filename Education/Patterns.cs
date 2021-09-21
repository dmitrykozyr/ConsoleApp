using System;
using System.Collections;
using System.Collections.Generic;

// Порождающие

namespace FactoryMethod
{
    // Есть фабрика по постройке деревнных домов
    // Мы расширяемся и создаем фабрику по постройке панельных домов
    // Мы не привязаны к типу домов, поэтому можем легко добавить это расширение
    // Создаем новые объекты не напрямую через new, а через фабричный метод, благодаря этому
    // можем его переопределить, чтобы он возвращал нужный тип объектов
    interface IHouse { }
    class WoodHouse : IHouse { public WoodHouse() { Console.WriteLine("Wood house was build"); } }
    class PanelHouse : IHouse { public PanelHouse() { Console.WriteLine("Panel house was build"); } }

    interface IDeveloper { IHouse FactoryMethod(); }
    class WoodDeveloper : IDeveloper { public IHouse FactoryMethod() { return new WoodHouse(); } }
    class PanelDeveloper : IDeveloper { public IHouse FactoryMethod() { return new PanelHouse(); } }

    class Program
    {
        static void Main_()
        {
            IDeveloper developer = new WoodDeveloper();
            IHouse woodHouse = developer.FactoryMethod();

            developer = new PanelDeveloper();
            IHouse panelHouse = developer.FactoryMethod();
        }
    }
}

namespace AbstractFactory
{
    // Задает интерфейс создания доступных типов продуктов,
    // каждая реализация фабрики порождает продукты одной из вариаций
    // Клиент вызывает методы фабрики для получения продуктов,
    // вместо самостоятельного создания с new
    interface IWeapon { public void Hit(); }
    class Arbalet : IWeapon { public void Hit() { Console.WriteLine("Shoot from arbalet"); } }
    class Sword : IWeapon { public void Hit() { Console.WriteLine("Hit by sword"); } }

    interface IMovement { public void Move(); }
    class Fly : IMovement { public void Move() { Console.WriteLine("Fly"); } }
    class Run : IMovement { public void Move() { Console.WriteLine("Run"); } }

    interface IHeroesFactory
    {
        IWeapon CreateWeapon();
        IMovement CreateMovement();
    }

    class ElfFactory : IHeroesFactory
    {
        public ElfFactory() { Console.WriteLine("Elf was created"); }
        public IMovement CreateMovement() { return new Fly(); }
        public IWeapon CreateWeapon() { return new Arbalet(); }
    }

    class WarriorFactory : IHeroesFactory
    {
        public WarriorFactory() { Console.WriteLine("Warrior was created"); }
        public IMovement CreateMovement() { return new Run(); }
        public IWeapon CreateWeapon() { return new Sword(); }
    }

    class Hero
    {
        private IWeapon _weapon;
        private IMovement _movement;
        public Hero(IHeroesFactory factory)
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
            var elf = new Hero(new ElfFactory());
            elf.Hit();
            elf.Run();

            Console.WriteLine();

            var warrior = new Hero(new WarriorFactory());
            warrior.Hit();
            warrior.Run();
        }
    }
}

namespace Builder
{
    // Динамически создаем объект из нескольких частей
    class Product
    {
        public List<object> parts = new List<object>();
        public void Add(string part) { parts.Add(part); }
    }

    interface IBuilder
    {
        abstract void BuildPartA();
        abstract void BuildPartB();
        abstract void GetResult();
    }

    // Может быть несколько рализаций строителя
    class ConcreteBuilder : IBuilder
    {
        Product product = new Product();

        public void BuildPartA() { product.Add("Part A"); }
        public void BuildPartB() { product.Add("Part B"); }
        public void GetResult()
        {
            foreach (var part in product.parts)
                Console.WriteLine(part);
        }
    }

    // Директор отвечает за построение в определенной последовательности,
    // его можно определить на стороне клиента
    class Director
    {
        IBuilder _builder;
        public Director(IBuilder builder) { _builder = builder; }
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
            IBuilder builder = new ConcreteBuilder();
            var director = new Director(builder);
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
    class Singleton
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

    interface IEuropeanRozetka { string GetElectricity(); }
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
    // При добавлении новых цветов не нужно расширять классы фигур
    interface IColor { string Color(); }
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
    abstract class Gift
    {
        public string Name { get; }
        public int Price { get; set; }
        public Gift(string name, int price) { Name = name; Price = price; }
        public abstract int TotalPrice();
    }

    interface IGiftOperations { void Add(Gift gift); }

    class Box : Gift, IGiftOperations
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
    }

    class ConcreteGift : Gift
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
    // потом кнопкн подачи питания на инжектор и кнопку включения генератора - это было-бы сложно
    // Эти действя заменяются поворотом ключа, остальное происходит под капотом
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

    class Program
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
    interface ISubject { void Request(); }
    class Client { public void ClientCode(ISubject subject) { subject.Request(); } }
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
    // В банке говорят что нужно идти в другое отделение, в другом
    // отправляют в региональное отделение и там получаем справку
    // Запрос может быть обработан в первом отделении или в нескольких
    interface IHandler
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
            foreach (var food in new List<string> { "Nut", "Banana", "Meat" })
            {
                Console.Write($"Who wants {food}?");
                var result = handler.Handle(food);

                if (result != null) Console.WriteLine($"    {result}");
                else Console.WriteLine($" {food} left untouched");
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

//!
namespace Command
{
    
}

//! 
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

    interface IAbstractExpression { public abstract void Interpreter(Context context); }

    class TerminalExpression : IAbstractExpression
    {
        public void Interpreter(Context context)
        {
            context.Result = context.Source[context.Position] == context.Vocabulary;
        }
    }

    class NonterminalExpression : IAbstractExpression
    {
        IAbstractExpression nonterminalExpression;
        IAbstractExpression terminalExpression;

        public void Interpreter(Context context)
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

    class Program
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

//!
namespace Iterator
{
    // Не важно, какой класс и из каких учеников построен, должны быть общие правила подсчета
    interface IIterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    interface IAggregate
    {
        public abstract IIterator CreateIterator();
        public abstract int Count { get; }
        public abstract object this[int index] { get; set; }
    }

    class ConcreteIterator : IIterator
    {
        private IAggregate aggregate;
        private int current = 0;

        public ConcreteIterator(IAggregate aggregate) { this.aggregate = aggregate; }
        public object First() { return aggregate[0]; }

        public object Next()
        {
            if (current++ < aggregate.Count - 1)
                return aggregate.Count;
            else
                return null;
        }

        public bool IsDone()
        {
            if (current < aggregate.Count)
                return false;

            current = 0;
            return true;
        }

        public object CurrentItem() { return aggregate[current]; }
    }

    class ConcreteAggregete : IAggregate
    {
        private ArrayList items = new ArrayList();
        public IIterator CreateIterator() { return new ConcreteIterator(this); }
        public int Count { get { return items.Count; } }

        public object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }

    class Program
    {
        static void Main_()
        {
            IAggregate a = new ConcreteAggregete();
            a[0] = "A";
            a[1] = "B";
            a[2] = "C";
            a[3] = "D";
            IIterator i = a.CreateIterator();
            
            for (object e = i.First(); !i.IsDone(); e = i.Next())
                Console.WriteLine(e);
        }
    }
}

namespace Mediator
{
    // Самолеты не общаются друг с другом напрямую, их корректирует диспетчер (медиатор)
    interface IDispatcher { void Notify(string destination); }

    class Dispatcher : IDispatcher
    {
        private PlaneBoeing _planeBoeing;
        private PlaneAirbus _planeAirbus;
        public Dispatcher(PlaneBoeing planeBoeing, PlaneAirbus planeAirbus)
        {
            _planeBoeing = planeBoeing;
            _planeBoeing.SetDispatcher(this);

            _planeAirbus = planeAirbus;
            _planeAirbus.SetDispatcher(this);
        }

        public void Notify(string destination)
        {
            if (destination == "Paris")
            {
                Console.WriteLine("Dispatcher reacts on Paris:");
                _planeAirbus.FlyToTokyo();
            }
            if (destination == "London")
            {
                Console.WriteLine("Dispatcher reacts on London:");
                _planeBoeing.FlyToSanFrancisco();
                _planeAirbus.FlyToTokyo();
            }
        }
    }

    class Planes
    {
        protected IDispatcher _dispatcher;
        public Planes(IDispatcher dispatcher = null) { _dispatcher = dispatcher; }
        public void SetDispatcher(IDispatcher dispatcher) { _dispatcher = dispatcher; }
    }

    class PlaneBoeing : Planes
    {
        public void FlyToParis()
        {
            Console.WriteLine("Boeing flying to Paris");
            _dispatcher.Notify("Paris");
        }

        public void FlyToSanFrancisco()
        {
            Console.WriteLine("Boeing flying to San Francisco");
            _dispatcher.Notify("San Francisco");
        }
    }

    class PlaneAirbus : Planes
    {
        public void FlyToTokyo()
        {
            Console.WriteLine("Airbus flying to Tokyo");
            _dispatcher.Notify("Tokyo");
        }

        public void FlyToLondon()
        {
            Console.WriteLine("Airbus flying to London");
            _dispatcher.Notify("London");
        }
    }

    class Program
    {
        static void Main_()
        {
            var planeBoeing = new PlaneBoeing();
            var planeAirbus = new PlaneAirbus();
            new Dispatcher(planeBoeing, planeAirbus);

            planeBoeing.FlyToParis();
            Console.WriteLine();
            planeAirbus.FlyToLondon();
        }
    }
}

namespace Memento
{
    // Просим друга с мобильным телефоном на время записать номер, что диктуют нам по телефону
    // Объект сохранчет состояние в другом объекте и потом восстанавливает его
    class Man
    {
        public string Сlothes { get; set; }
        public void Dress(Backpack backpack) { Сlothes = backpack.Сontents; }
        public Backpack Undress() { return new Backpack(Сlothes); }
    }
        
    class Backpack
    {
        public string Сontents { get; private set; }
        public Backpack(string сontents) { this.Сontents = сontents; }
    }

    class Robot { public Backpack Backpack { get; set; } }

    class Program
    {
        static void Main_()
        {
            var man = new Man();
            var robot = new Robot();

            man.Сlothes = "Clothes";            // Одеваем челоека в одежду
            robot.Backpack = man.Undress();     // Отдаем рюкзак роботу
            man.Сlothes = "Another clothes";
            man.Dress(robot.Backpack);          // Берем у робота рбкзак и одеваем другую одежду
        }
    }
}

namespace Observer
{
    // Подписчики подписались на рассылку издателя и в зависимости 
    // от типа рассылки, реагируют на нее по разному
    interface IObserver { void ReactToPublisherEvent(IPublisher publisher); }

    interface IPublisher
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void NotifySubscribers();
    }

    class Publisher : IPublisher
    {        
        public int State { get; set; }
        private List<IObserver> _subscribers = new List<IObserver>();

        public void Attach(IObserver subscriber)
        {
            Console.WriteLine("Attached subscriber " + subscriber.GetType().Name);
            _subscribers.Add(subscriber);
        }

        public void Detach(IObserver subscriber)
        {
            _subscribers.Remove(subscriber);
            Console.WriteLine("Detached subscriber");
        }

        public void NotifySubscribers()
        {
            Console.WriteLine("Notifying all subscribers");
            foreach (var subscriber in _subscribers)
                subscriber.ReactToPublisherEvent(this);
        }

        public void SomeBusinessLogic()
        {
            State = new Random().Next(0, 10);
            Console.WriteLine("\nPublisher changed state to: " + State);
            NotifySubscribers();
        }
    }

    class SubscriberA : IObserver
    {
        public void ReactToPublisherEvent(IPublisher publisher)
        {
            if ((publisher as Publisher).State < 3)
                Console.WriteLine("Subscriber A reacted to the event");
        }
    }

    class SubscriberB : IObserver
    {
        public void ReactToPublisherEvent(IPublisher publisher)
        {
            if ((publisher as Publisher).State == 0 || (publisher as Publisher).State >= 2)
                Console.WriteLine("Subscriber B reacted to the event");
        }
    }

    class Program
    {
        static void Main_()
        {
            var publisher = new Publisher();
            var subscriberA = new SubscriberA();
            publisher.Attach(subscriberA);

            var subscriberB = new SubscriberB();
            publisher.Attach(subscriberB);

            publisher.SomeBusinessLogic();
            publisher.SomeBusinessLogic();

            publisher.Detach(subscriberB);
        }
    }
}

namespace State
{
    // Если мы устали, то на фразу "Сходи в магазин" скажем "Не пойду",
    // а если нужно сходить за пивом - скажем "Уже бегу"
    // Человек тот-же, а поведение разное
    interface IState { public abstract void Handle(Context context); }
    class StateBuyFood : IState { public void Handle(Context context) { context.State = new StateBuyBeer(); } }
    class StateBuyBeer : IState { public void Handle(Context context) { context.State = new StateBuyFood(); } }

    class Context
    {
        public IState State { get; set; }
        public Context(IState state) { State = state; }
        public void Request() { State.Handle(this); }
    }

    class Program
    {
        static void Main_()
        {
            var context = new Context(new StateBuyFood());
            context.Request();

            context = new Context(new StateBuyBeer());
            context.Request();
        }
    }
}

namespace Strategy
{
    // Говорим "Хочу права, денег мало" - получим права через месяц
    // Говорим "Хочу права, денег много" - получим права завтра
    // Что делал человек, мы не знаем, но задаем начальные условия, а он решает, как себя вести
    interface IStrategy { public abstract void AlgorithmInterface(); }
    class SlowLicense : IStrategy { public void AlgorithmInterface() { Console.WriteLine("Make driver's license slow"); } }
    class FastLicence : IStrategy { public void AlgorithmInterface() { Console.WriteLine("Make driver's license fast"); } }

    class Context
    {
        IStrategy _strategy;
        public Context(IStrategy strategy) { _strategy = strategy; }
        public void ContextInterface() { _strategy.AlgorithmInterface(); }
    }

    class Program
    {
        static void Main_()
        {
            var context = new Context(new SlowLicense());
            context.ContextInterface();

            context = new Context(new FastLicence());
            context.ContextInterface();
        }
    }
}

namespace TemplateMethod
{
    // Определяет основу алгоритма и позволяет подклассам переопределить шаги, не меняя структуру
    interface IAbstractClass
    {
        public abstract void Step1();
        public abstract void Step2();
        public void TemplateMethod()
        {
            Step1();
            Step2();
        }
    }

    class ConcreteClass : IAbstractClass
    {
        public void Step1() { Console.WriteLine("PrimitiveOperation1"); }
        public void Step2() { Console.WriteLine("PrimitiveOperation2"); }
    }

    class Program
    {
        static void Main_()
        {
            IAbstractClass instance = new ConcreteClass();
            instance.TemplateMethod();
        }
    }
}

namespace Visitor
{
    // Есть несколько покупателей и несколько магазинов
    // Разные покупатели могут прийти в разные магазины и что-то купить
    interface IVisitor
    {
        void VisitComixShop(ComixShop comixShop);
        void VisitDressShop(DressShop dressShop);
    }

    class VisitorJohn : IVisitor
    {
        public void VisitComixShop(ComixShop comixShop) { comixShop.BuyComix(); }
        public void VisitDressShop(DressShop dressShop) { dressShop.BuyDress(); }
    }

    class VisitorKate : IVisitor
    {
        public void VisitComixShop(ComixShop comixShop) { comixShop.BuyComix(); }
        public void VisitDressShop(DressShop dressShop) { dressShop.BuyDress(); }
    }   

    interface IShop
    {
        void Accept(IVisitor visitor);
        public string SomeState { get; set; }
    }

    class ComixShop : IShop
    {
        public string SomeState { get; set; }
        public void Accept(IVisitor visitor) { visitor.VisitComixShop(this); }
        public void BuyComix() { Console.WriteLine("Comix was bought"); }
    }

    class DressShop : IShop
    {
        public string SomeState { get; set; }
        public void Accept(IVisitor visitor) { visitor.VisitDressShop(this); }
        public void BuyDress() { Console.WriteLine("Dress was bought"); }
    }

    class Client
    {
        List<IShop> shops = new List<IShop>();
        public void Add(IShop shop) { shops.Add(shop); }
        public void Remove(IShop shop) { shops.Remove(shop); }
        public void Accept(IVisitor visitor)
        {
            foreach (var shop in shops)
                shop.Accept(visitor);
        }
    }

    class Program
    {
        static void Main()
        {
            var client = new Client();

            client.Add(new ComixShop());
            client.Add(new DressShop());

            client.Accept(new VisitorJohn());
            client.Accept(new VisitorKate());
        }
    }
}
