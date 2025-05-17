// ПОРОЖДАЮЩИЕ

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FactoryMethod
{
    // Есть фабрика постройки деревянных домов
    // Создаем фабрику постройки панельных домов
    // Мы не привязаны к типу домов, поэтому можем легко добавить это расширение
    // Создаем новые объекты не через new, а через фабричный метод, который переопределить, чтобы возвращал нужный тип объектов

    interface IHouse
    {
    }

    class WoodHouse : IHouse 
    {
        public WoodHouse()
        {
            Console.WriteLine("Wood house was build");
        }
    }

    class PanelHouse : IHouse 
    { 
        public PanelHouse()
        {
            Console.WriteLine("Panel house was build");
        }
    }

    interface IDeveloper
    {
        IHouse FactoryMethod();
    }

    class WoodDeveloper : IDeveloper
    {
        public IHouse FactoryMethod()
        {
            return new WoodHouse();
        }
    }

    class PanelDeveloper : IDeveloper
    {
        public IHouse FactoryMethod()
        {
            return new PanelHouse();
        }
    }

    class Program
    {
        static void _Main()
        {
            // Деревянный дом
            IDeveloper developer = new WoodDeveloper();
            IHouse woodHouse = developer.FactoryMethod();

            // Панельный дом
            developer = new PanelDeveloper();
            IHouse panelHouse = developer.FactoryMethod();
        }
    }
}

namespace AbstractFactory
{
    // Задаем интерфейс создания продуктов
    // Каждая реализация фабрики порождает продукты одной из вариаций
    // Клиент вызывает методы фабрики для получения продуктов, вместо создания через new

    // WEAPON
    interface IWeapon
    { 
        public void Hit();
    }

    class Arbalet : IWeapon
    { 
        public void Hit()
        {
            Console.WriteLine("Shoot from arbalet");
        } 
    }

    class Sword : IWeapon
    { 
        public void Hit()
        {
            Console.WriteLine("Hit by sword");
        } 
    }


    // MOVEMENT
    interface IMovement
    { 
        public void Move();
    }

    class Fly : IMovement
    { 
        public void Move()
        {
            Console.WriteLine("Fly");
        } 
    }

    class Run : IMovement
    { 
        public void Move()
        {
            Console.WriteLine("Run");
        }
    }


    // FACTORY
    interface IHeroesFactory
    {
        IWeapon CreateWeapon();

        IMovement CreateMovement();
    }

    class ElfFactory : IHeroesFactory
    {
        public ElfFactory()
        {
            Console.WriteLine("Elf was created");
        }

        public IMovement CreateMovement()
        { 
            return new Fly(); 
        }

        public IWeapon CreateWeapon()
        { 
            return new Arbalet();
        }
    }

    class WarriorFactory : IHeroesFactory
    {
        public WarriorFactory()
        {
            Console.WriteLine("Warrior was created");
        }

        public IMovement CreateMovement()
        { 
            return new Run(); 
        }

        public IWeapon CreateWeapon()
        { 
            return new Sword();
        }
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

        public void Run()
        { 
            _movement.Move();
        }

        public void Hit()
        { 
            _weapon.Hit();
        }
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

        public void Add(string part)
        {
            parts.Add(part);
        }
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

        public void BuildPartA() 
        { 
            product.Add("Part A"); 
        }

        public void BuildPartB() 
        { 
            product.Add("Part B"); 
        }

        public void GetResult()
        {
            foreach (var part in product.parts)
            {
                Console.WriteLine(part);
            }
        }
    }

    // Директор отвечает за построение в определенной последовательности, его можно определить на стороне клиента
    class Director
    {
        IBuilder _builder;

        public Director(IBuilder builder) 
        { 
            _builder = builder; 
        }

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
//+
namespace Prototype
{
    // Создаем прототип и на его онове клоны
    interface IClone 
    {
        IClone Clone();
    }

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

        public Sphere(int radius)
        {
            _radius = radius;
        }

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
//+
namespace Singleton
{
    class Singleton
    {
        private Singleton() { }

        private static Singleton singleton = new Singleton();

        public static Singleton GetSingleton() 
        {
            return singleton;
        }
    }
}

// СТРУКТУРНЫЕ
//+
namespace Adapter
{
    class EuropeanRozetka
    {
        public string GetElectricity()
        {
            return "European rozetka";
        }
    }

    class JapaneseRozetka
    {
        public string GetElectricity()
        {
            return "Japanese rozetka";
        }
    }

    interface IEuropeanRozetka
    {
        string GetElectricity();
    }

    class Adapter : IEuropeanRozetka

    {
        private readonly JapaneseRozetka _japaneseRozetka;

        public Adapter(JapaneseRozetka japaneseRozetka)
        {
            _japaneseRozetka = japaneseRozetka;
        }

        public string GetElectricity()
        {
            return _japaneseRozetka.GetElectricity();
        }
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
    // Разделяет абстракцию и реализацию, позволяя менять их независимо друг от друга
    // Храним информацию об цвете геометрической фигуры в отдельном классе
    // При добавлении новых цветов не нужно расширять классы фигур

    interface IColor
    {
        string Color();
    }

    class RedColor : IColor
    {
        public string Color()
        {
            return "RedColor";
        }
    }

    class BlueColor : IColor
    {
        public string Color()
        {
            return "BlueColor";
        }
    }

    class Figure
    {
        protected IColor _color;

        public Figure(IColor color)
        {
            _color = color;
        }

        public virtual string Paint()
        {
            return "Paint all figures with " + _color.Color();
        }
    }

    class Sphere : Figure
    {
        public Sphere(IColor color)
            : base(color) { }

        public override string Paint()
        {
            return "Paint sphere with " + _color.Color();
        }
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
    // Есть коробка, в которую ложим игрушки и еще одну коробку с игрушкой
    // Можем посчитать стоимость всех игрушек, обойдя эту древовидную структуру

    abstract class Gift
    {
        public string Name { get; }

        public int Price { get; set; }

        protected Gift(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public abstract int TotalPrice();
    }

    interface IGiftOperations
    {
        void Add(Gift gift);
    }

    class Box : Gift, IGiftOperations
    {
        private List<Gift> _gifts = new List<Gift>();

        public Box(string name, int price)
            : base(name, price) { }

        public void Add(Gift gift)
        {
            _gifts.Add(gift);
        }

        public override int TotalPrice()
        {
            int totalPrice = 0;

            Console.WriteLine();

            Console.WriteLine($"{Name} contains: ");

            foreach (var gift in _gifts)
            {
                totalPrice += gift.TotalPrice();
            }

            return totalPrice;
        }
    }

    class ConcreteGift : Gift
    {
        public ConcreteGift(string name, int price)
            : base(name, price) { }

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
//+
namespace Decorator
{
    // Пиццерия готовит разные пиццы с разными добавками
    // В зависимости от комбинации меняется стоимость

    abstract class Pizza
    {
        public string Name { get; set; }

        public Pizza(string name)
        {
            Name = name;
        }

        public abstract int GetCost();
    }

    class ItalianPizza : Pizza
    {
        public ItalianPizza()
            : base("Italian pizza") { }

        public override int GetCost() { return 10; }
    }

    abstract class PizzaDecorator : Pizza
    {
        // Ссылка на декорируемый объект
        protected Pizza Pizza;

        public PizzaDecorator(string name, Pizza pizza)
            : base(name)
        {
            Pizza = pizza;
        }
    }

    class TomatoPizza : PizzaDecorator
    {
        public TomatoPizza(Pizza pizza)
            : base(pizza.Name + ", with tomatos", pizza) { }

        public override int GetCost()
        {
            return Pizza.GetCost() + 3;
        }
    }

    class CheesePizza : PizzaDecorator
    {
        public CheesePizza(Pizza pizza)
            : base(pizza.Name + ", with cheeze", pizza) { }

        public override int GetCost()
        {
            return Pizza.GetCost() + 5;
        }
    }

    class Program
    {
        static void Main_()
        {
            Pizza italianPizza = new ItalianPizza();
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");

            // Декорируем пиццу томатом
            italianPizza = new TomatoPizza(italianPizza);
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");

            // Декорируем пиццу сыром
            italianPizza = new CheesePizza(italianPizza);
            Console.WriteLine($"{italianPizza.Name}, price {italianPizza.GetCost()}");
        }
    }
}
//+
namespace Facade
{
    // Если-бы для управления авто нужно было подать питание с аккумулятора на инжектор
    // и нажать кнопку включения генератора - это было-бы сложно
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

    class SubsystemA
    {
        public void OperationA()
        {
            Console.WriteLine("Operation A");
        }
    }

    class SubsystemB
    {
        public void OperationB()
        {
            Console.WriteLine("Operation B");
        }
    }

    class SubsystemC
    {
        public void OperationC()
        {
            Console.WriteLine("Operation C");
        }
    }

    class Program
    {
        static void Main_()
        {
            var facade = new Facade();

            facade.OperationABC();
        }
    }
}

namespace Flyweight
{
    // Если играем в шутер, то нет смысла создавать для каждой пули новый объект
    // Лучше создать несколько и переиспользовать их

    interface IBullet
    {
        abstract void Shoot();
    }

    class Bullet : IBullet
    {
        public string Name { get; set; }

        public Bullet(string name)
        {
            Name = name;

            Console.WriteLine($"{name} was created");
        }

        public void Shoot()
        {
            Console.WriteLine($"Shoot {Name}");
        }
    }

    class BulletFactory
    {
        // Фабрика возвращает запрошенные объекты
        // Если такого нет - создает его и возвращает

        List<Bullet> allBullets = new List<Bullet>();

        public BulletFactory()
        {
            allBullets.Add(new Bullet("Bullet 1"));
            allBullets.Add(new Bullet("Bullet 2"));
        }

        public IBullet GetBullet(string key)
        {
            if (!allBullets.Any(z => z.Name == key))
            {
                var newBullet = new Bullet(key);

                allBullets.Add(newBullet);

                return newBullet;
            }

            return allBullets.FirstOrDefault(z => z.Name == key);
        }
    }

    class Program
    {
        static void Main_()
        {
            var bulletFactory = new BulletFactory();

            IBullet bullet1 = bulletFactory.GetBullet("Bullet 1");
            bullet1.Shoot();

            IBullet bullet2 = bulletFactory.GetBullet("Bullet 2");
            bullet2.Shoot();

            IBullet bullet3 = bulletFactory.GetBullet("Bullet 3");
            bullet3.Shoot();
        }
    }
}
//+
namespace Proxy
{
    // Сначала отрабатывает прокси, потом основной объект и опять прокси
    // Прокси может замещать или дополнять объект

    interface ISubject
    {
        void Request();
    }

    class Client 
    { 
        public void ClientCode(ISubject subject) 
        { 
            subject.Request(); 
        } 
    }

    class RealObject : ISubject 
    {
        public void Request() 
        {
            Console.WriteLine("Real subject"); 
        } 
    }

    class Proxy : ISubject
    {
        private RealObject _realSubject;

        public Proxy(RealObject realSubject) 
        { 
            _realSubject = realSubject; 
        }

        public void Request()
        {
            ProxyBegin();

            _realSubject.Request();

            ProxyEnd();
        }

        public void ProxyBegin()
        {
            Console.WriteLine("Proxy begin");
        }

        public void ProxyEnd()
        {
            Console.WriteLine("Proxy end");
        }
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

// ПОВЕДЕНЧЕСКИЕ

namespace ChainOfResponsibility
{
    // Требуется получить справку из банка, но не ясно, кто должен ее дать
    // В банке направляют в другое отделение, там в региональное и там получаем справку
    // Запрос может быть обработан в первом отделении, втором или нескольких

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
            if (_nextHandler is not null)
            { 
                return _nextHandler.Handle(request);
            }
            
            return null;
        }
    }

    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Banana")
            {
                return $"Monkey: I love {request}";
            }

            return base.Handle(request);
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Nut")
            {
                return $"Sqiurrel: I love {request}";
            }

            return base.Handle(request);
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Meat")
            {
                return $"Dog: I love {request}";
            }

            return base.Handle(request);
        }
    }

    class Client
    {
        public static void ClientCode(AbstractHandler handler)
        {
            var myList = new List<string> { "Nut", "Banana", "Meat" };

            foreach (var food in myList)
            {
                Console.Write($"Who wants {food}?");

                var result = handler.Handle(food);

                if (result is not null)
                {
                    Console.WriteLine($" {result}");
                }
                else
                {
                    Console.WriteLine($" {food} left untouched");
                }
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

namespace Command
{
    // Клиент в ресторане говорит официанту заказ, тот записывает его и передает повару, а он готовит блюда

    class Chief
    {
        public void CookFirstDish(string a)
        {
            Console.WriteLine($"Chief: Cooking {a}");
        }

        public void CookSecondDish(string b)
        {
            Console.WriteLine($"Chief: Also cooking {b}");
        }
    }

    interface ICommand
    {
        void GiveOrderToChief();
    }

    class Waiter : ICommand
    {
        private Chief _chief;
        private string _a;
        private string _b;

        public Waiter(Chief chief, string a, string b)
        {
            _chief = chief;
            _a = a;
            _b = b;
        }

        public void GiveOrderToChief()
        {
            _chief.CookFirstDish(_a);
            _chief.CookSecondDish(_b);
        }
    }

    // Отправитель связан с одной или несколькими командами
    class Client
    {
        private ICommand _onStart;
        private ICommand _onFinish;

        public void OrderFirstDishes(ICommand command)
        {
            _onStart = command;
        }

        public void OrderSecondDishes(ICommand command)
        {
            _onFinish = command;
        }

        public void DoSomethingImportant()
        {
            _onStart.GiveOrderToChief();
            _onFinish.GiveOrderToChief();
        }
    }

    class Program
    {
        static void Main_()
        {
            var cient = new Client();
            var chief = new Chief();

            cient.OrderFirstDishes(new Waiter(chief, "Meat", "Salad"));
            cient.OrderSecondDishes(new Waiter(chief, "Dessert", "Drinks"));

            cient.DoSomethingImportant();
        }
    }
}

namespace Iterator
{
    // Не важно, какой класс и из каких учеников построен, должны быть общие правила подсчета

    interface IStudentsIterator
    {
        string Current { get; }

        bool MoveNext();
    }

    class StudentsIterator : IStudentsIterator
    {
        private List<string> _students;

        private int _position;

        public StudentsIterator(List<string> students)
        {
            _students = students;
            _position = -1;
        }

        public string Current => _students[_position];

        public bool MoveNext()
        {
            if (++_position == _students.Count)
            {
                return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main_()
        {
            var students = new List<string> { "Student 1", "Student 2", "Student 3" };

            var iterator = new StudentsIterator(students);

            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current);
            }
        }
    }
}
//+
namespace Mediator
{
    // Самолеты не общаются напрямую, их координирует диспетчер (медиатор)

    interface IDispatcher
    {
        void Notify(string destination);
    }

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

        public Planes(IDispatcher dispatcher = default)
        {
            _dispatcher = dispatcher;
        }

        public void SetDispatcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
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
//+
namespace Memento
{
    // Просим друга запомнить номер, что диктуют по телефону
    // Он запоминает
    // Нам диктуют новый и старый мы забываем
    // Можем попросить друга напомнить его

    class Man
    {
        public string? PhoneNumber { get; set; }

        public void RestoreNumber(DataToRemember phoneNumber)
        {
            PhoneNumber = phoneNumber.PhoneNumber;
        }

        public DataToRemember GetNumber()
        {
            return new DataToRemember(PhoneNumber);
        }
    }

    class DataToRemember
    {
        public string? PhoneNumber { get; private set; }

        public DataToRemember(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }

    class Friend
    {
        public DataToRemember? PhoneNumber { get; set; }
    }

    class Program
    {
        static void Main_()
        {
            var man = new Man();
            var friend = new Friend();

            man.PhoneNumber = "000-000-0000";       // Получаем номер телефона
            friend.PhoneNumber = man.GetNumber();   // Просим друга запомнить номер
            man.PhoneNumber = "777-777-7777";       // Получаем другой номер, старый забываем
            man.RestoreNumber(friend.PhoneNumber);  // Просим друга напомнить старый номер
        }
    }
}

namespace Observer
{
    // Подписчики подписались на рассылку издателя и, в зависимости от типа рассылки, реагируют по разному

    interface IObserver
    {
        void ReactToPublisherEvent(IPublisher publisher);
    }

    interface IPublisher
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void NotifySubscribers();
    }

    class Publisher : IPublisher
    {
        public int State { get; set; }

        private List<IObserver> _subscribers = new List<IObserver>();

        public void Subscribe(IObserver subscriber)
        {
            Console.WriteLine("Attached subscriber " + subscriber.GetType().Name);

            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IObserver subscriber)
        {
            _subscribers.Remove(subscriber);

            Console.WriteLine("Detached subscriber");
        }

        public void NotifySubscribers()
        {
            Console.WriteLine("Notifying all subscribers");

            foreach (var subscriber in _subscribers)
            {
                subscriber.ReactToPublisherEvent(this);
            }
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
            {
                Console.WriteLine("Subscriber A reacted to the event");
            }
        }
    }

    class SubscriberB : IObserver
    {
        public void ReactToPublisherEvent(IPublisher publisher)
        {
            if ((publisher as Publisher).State == 0 || (publisher as Publisher).State >= 2)
            {
                Console.WriteLine("Subscriber B reacted to the event");
            }
        }
    }

    class Program
    {
        static void Main_()
        {
            var publisher = new Publisher();
            var subscriberA = new SubscriberA();
            publisher.Subscribe(subscriberA);

            var subscriberB = new SubscriberB();
            publisher.Subscribe(subscriberB);

            publisher.SomeBusinessLogic();
            publisher.SomeBusinessLogic();

            publisher.Unsubscribe(subscriberB);
        }
    }
}

namespace State
{
    // Если мы устали, то на фразу "Сходи в магазин" скажем "Не пойду"
    // А если нужно сходить за пивом - скажем "Уже бегу"
    // Человек тот-же, а поведение разное

    interface IState
    {
        public abstract void Handle(Context context);
    }

    class StateBuyFood : IState
    {
        public StateBuyFood() 
        { 
            Console.WriteLine("Buy food"); 
        }

        public void Handle(Context context) 
        { 
            context.State = new StateBuyFood(); 
        }
    }

    class StateBuyBeer : IState
    {
        public StateBuyBeer() 
        { 
            Console.WriteLine("Buy beer"); 
        }

        public void Handle(Context context) 
        { 
            context.State = new StateBuyBeer(); 
        }
    }

    class Context
    {
        public IState State { get; set; }

        public Context(IState state) 
        { 
            State = state; 
        }

        public void Request() 
        { 
            State.Handle(this); 
        }
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
    // Что делал человек - мы не знаем, но задаем начальные условия, а он решает, как себя вести

    interface IStrategy 
    { 
        public abstract void AlgorithmInterface(); 
    }
    
    class SlowLicense : IStrategy 
    { 
        public void AlgorithmInterface() 
        { 
            Console.WriteLine("Make driver's license slow"); 
        } 
    }
    
    class FastLicence : IStrategy 
    { 
        public void AlgorithmInterface() 
        { 
            Console.WriteLine("Make driver's license fast"); 
        } 
    }

    class Context
    {
        IStrategy _strategy;

        public Context(IStrategy strategy) 
        { 
            _strategy = strategy; 
        }

        public void ContextInterface() 
        { 
            _strategy.AlgorithmInterface(); 
        }
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
//+
namespace TemplateMethod
{
    // Определяем основу алгоритма и позволяем подклассам переопределить шаги, не меняя структуру

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
        public void Step1()
        {
            Console.WriteLine("PrimitiveOperation1");
        }

        public void Step2()
        {
            Console.WriteLine("PrimitiveOperation2");
        }
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
    // Есть студенты - сначала их посещает доктор, а потом продавец книг
    // Каждый выполняет с каждым студентом определенные действия (проверяет здоровье | дает книгу)

    interface IElement
    {
        void Accept(IVisitor visitor);
    }

    class Student : IElement
    {
        public string Name { get; set; }

        public Student(string name)
        {
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    interface IVisitor
    {
        void Visit(IElement element);
    }

    class Doctor : IVisitor
    {
        public string Name { get; set; }

        public Doctor(string name)
        {
            Name = name;
        }

        public void Visit(IElement element)
        {
            var student = (Student)element;

            Console.WriteLine(Name + " checked student: " + student.Name);
        }
    }

    class Salesman : IVisitor
    {
        public string Name { get; set; }

        public Salesman(string name)
        {
            Name = name;
        }

        public void Visit(IElement element)
        {
            var student = (Student)element;

            Console.WriteLine(Name + " gave book to student: " + student.Name);
        }
    }

    class School
    {
        private static List<IElement> students;

        static School()
        {
            students = new List<IElement>
            {
                new Student("Ram"),
                new Student("Sara")
            };
        }
        public void PerformOperation(IVisitor visitor)
        {
            foreach (var kid in students)
            {
                kid.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main_()
        {
            var school = new School();

            var doctor = new Doctor("Doctor");
            school.PerformOperation(doctor);

            Console.WriteLine();

            var salesman = new Salesman("Salesman");
            school.PerformOperation(salesman);
        }
    }
}

// ДРУГИЕ ПАТТЕРНЫ

namespace UnitOfWork
{
    /*     
        Объединяет несколько операций над данными в одну единицу работы,
        чтобы выполнить несколько изменений (добавление, обновление, удаление) в БД одновременно
        
        Управляет транзакциями, чтобы все изменения применились только при успешном завершении всех операций
        Если одна из операций не удалась, все изменения будут отменены
    */

    public interface IRepository<TEntity>
    {
        public void Add(TEntity entity);
    }

    public class MyDbContext : DbContext
    {
    }

    public class User
    {
        public string? Name { get; set; }
    }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    // Создаем экземпляр UnitOfWork
    // Выполняем операции с репозиториями
    // Сохраняем все изменения методом Save
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }

    class Program
    {
        static void Main_()
        {
            using (var unitOfWork = new UnitOfWork(new MyDbContext()))
            {
                var userRepository = unitOfWork.Repository<User>();

                var newUser = new User
                {
                    Name = "John Doe"
                };

                userRepository.Add(newUser);

                unitOfWork.Save();
            }
        }
    }    
}

namespace HttpClientFactory
{
    /*
        Упрощает создание и управление экземплярами HttpClient

        Помогает избежать проблем, связанных с использованием HttpClient
        - истощение сокетов
        - неправильное управление временем жизни экземпляров

        Преимущества HttpClientFactory:
        - HttpClient - дорогостоящий объект для создания, его следует использовать повторно
          HttpClientFactory управляет временем жизни экземпляров, что позволяет избежать проблем с исчерпанием сокетов
        - можно централизованно настраивать параметры HttpClient:
            - базовый адрес
            - заголовки
            - обработчики
        - можно добавлять общие обработчики (например, для аутентификации или логирования) для всех запросов
        - можно создавать несколько настроек HttpClient для различных сценариев использования

        Как использовать HttpClientFactory:
        - установка пакета Microsoft.Extensions.Http
        - настройка Startup.cs
        - использование IHttpClientFactory для получения экземпляров HttpClient в классах

        Пример:
        - сервис MyService использует IHttpClientFactory для создания экземпляра HttpClient,
          настроенного для работы с API GitHub
        - добавим заголовок User-Agent, чтобы соответствовать требованиям GitHub API
    */

    class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация HttpClientFactory
            services.AddHttpClient();

            // Регистрация именованного клиента
            services.AddHttpClient("GitHub", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApp", "1.0"));
            });
        }

        public class MyService
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public MyService(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task<string> GetGitHubDataAsync()
            {
                var client = _httpClientFactory.CreateClient("GitHub");
                var response = await client.GetAsync("repos/dotnet/aspnetcore");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
