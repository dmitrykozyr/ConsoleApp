using System;
using System.Collections;
using System.Collections.Generic;

// Дописать описания ко всем паттернам

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
    // Конструируем объекты из готовых частей
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
    // Динамически создаем объект из нескольких частей
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
            return handler; // Возврат обработчика позволит связать обработчики так: monkey.SetNext(squirrel)
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

//!
namespace Iterator
{
    // Не важно, какой класс построен и из каких учеников, должны быть общие правила подсчета и
    // обращения как каждому ученику по списку
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
    // Самолеты в небе не общаются друг с другом напрямую,
    // их поведение корректирует диспетчер (медиатор), с которым самолеты общаются
    public interface IDispatcher { void Notify(string destination); }

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

//!
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

    class A
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
    // от типа рассылки, реагирую на нее по разному

    public interface IObserver { void ReactToPublisherEvent(IPublisher publisher); }

    public interface IPublisher
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void NotifySubscribers();
    }

    public class Publisher : IPublisher
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
    abstract class State { public abstract void Handle(Context context); }
    class ConcreteStateA : State { public override void Handle(Context context) { context.State = new ConcreteStateB(); } }
    class ConcreteStateB : State { public override void Handle(Context context) { context.State = new ConcreteStateA(); } }

    class Context
    {
        public State State { get; set; }
        public Context(State state) { State = state; }
        public void Request() { State.Handle(this); }
    }

    class Program
    {
        static void Main_()
        {
            var context = new Context(new ConcreteStateA());
            context.Request();

            context = new Context(new ConcreteStateB());
            context.Request();
        }
    }
}

namespace Strategy
{
    // Используется для выбора различных путей получения результата
    // Говорим "Хочу права, денег мало" - получим права через месяц
    // Говорим "Хочу права, денег много" - получим права завтра
    // Что делал человек, мы не знаем, но задаем начальные условия, а он решает, как себя вести
    // Как устроена стратегия, нам знать не требуется
    abstract class Strategy { public abstract void AlgorithmInterface(); }
    class StrategyA : Strategy { public override void AlgorithmInterface() { Console.WriteLine("Strategy A"); } }
    class StrategyB : Strategy { public override void AlgorithmInterface() { Console.WriteLine("Strategy B"); } }

    class Context
    {
        Strategy _strategy;
        public Context(Strategy strategy) { _strategy = strategy; }
        public void ContextInterface() { _strategy.AlgorithmInterface(); }
    }

    class Program
    {
        static void Main_()
        {
            var context = new Context(new StrategyA());
            context.ContextInterface();

            context = new Context(new StrategyB());
            context.ContextInterface();
        }
    }
}

namespace TemplateMethod
{
    // Определяет основу алгоритма и позволяет подклассам переопределить некоторые шаги, не изменяя структуру
    // Применяется, когда планируется, что в будущем подклассы будут переопределять алгоритмы без изменения структуры
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

//! Тут остановился
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
    // - Когда имеется много объектов разных классов с разными интерфейсами, и требуется выполнить ряд операций над каждым из этих объектов
    // - Когда классам необходимо добавить одинаковый набор операций без изменения этих классов
    // - Когда часто добавляются новые операции к классам, при этом общая структура классов стабильна и практически не изменяется
    public interface IComponent { void Accept(IVisitor visitor); }
    public class ConcreteComponentA : IComponent
    {
        public void Accept(IVisitor visitor) { visitor.VisitConcreteComponentA(this); }
        public string ExclusiveMethodOfConcreteComponentA() { return "A"; }
    }

    public class ConcreteComponentB : IComponent
    {
        public void Accept(IVisitor visitor) { visitor.VisitConcreteComponentB(this); }
        public string SpecialMethodOfConcreteComponentB() { return "B"; }
    }

    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);
        void VisitConcreteComponentB(ConcreteComponentB element);
    }

    class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class Client
    {
        public static void ClientCode(List<IComponent> components, IVisitor visitor)
        {
            foreach (var component in components)
                component.Accept(visitor);
        }
    }

    class Program
    {
        static void Main()
        {
            var components = new List<IComponent> 
            { 
                new ConcreteComponentA(), 
                new ConcreteComponentB() 
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            Client.ClientCode(components, visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            Client.ClientCode(components, visitor2);
        }
    }
}
