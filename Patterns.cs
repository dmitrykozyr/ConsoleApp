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
    abstract class Creator
    {
        Product product;
        public abstract Product FactoryMethod();
        public void AnOperation() { product = FactoryMethod(); }
    }

    class ConcreteCreator : Creator
    {
        public override Product FactoryMethod() { return new ConcreteProduct(); }
    }

    abstract class Product { }

    class ConcreteProduct : Product
    {
        public ConcreteProduct() { Console.WriteLine(this.GetHashCode()); }
    }

    class A
    {
        static void Main_()
        {
            Creator creator = new ConcreteCreator();
            Product product = creator.FactoryMethod();
            creator.AnOperation();
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
    class Client
    {
        private AbstractWater water;
        private AbstractBottle bottle;

        public Client(AbstractFactory factory)
        {
            water = factory.CreateWater();
            bottle = factory.CreateBottle();
        }

        // Здесь методы должны вызываться в определенной последовательности
        public void Run() { bottle.Interact(water); }
    }

    abstract class AbstractFactory
    {
        // Фабричные методы
        public abstract AbstractWater CreateWater();
        public abstract AbstractBottle CreateBottle();
    }

    abstract class AbstractBottle { public abstract void Interact(AbstractWater water); }

    class AbstractWater { }

    // Фабрика по созданию воды и бутылки для Кока-Колы
    class CocaColaFactory : AbstractFactory
    {
        public override AbstractWater CreateWater() { return new CocaColaWater(); }
        public override AbstractBottle CreateBottle() { return new CocaColaBottle(); }
    }

    class CocaColaBottle : AbstractBottle
    {
        public override void Interact(AbstractWater water) { Console.WriteLine(this + " interacts with " + water); }
    }

    class CocaColaWater : AbstractWater { }
        
    class PepsiColaFactory : AbstractFactory // Фабрика по созданию воды и бутылки для Пепси-Колы
    {
        public override AbstractWater CreateWater() { return new PepsiColaWater(); }
        public override AbstractBottle CreateBottle() { return new PepsiColaBottle(); }
    }

    class PepsiColaBottle : AbstractBottle
    {
        public override void Interact(AbstractWater water) { Console.WriteLine(this + " interacts with " + water); }
    }

    class PepsiColaWater : AbstractWater { }

    class Program
    {
        static void Main_()
        {
            Client client = null;

            client = new Client(new CocaColaFactory());
            client.Run();

            client = new Client(new PepsiColaFactory());
            client.Run();
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
    abstract class Builder
    {
        public abstract void BuildBassement();
        public abstract void BuildStorey();
        public abstract House GetResult();
    }

    class ConcreteBuilder : Builder
    {
        House house = new House();
        public override void BuildBassement() { house.Add(new Bassement()); }
        public override void BuildStorey() { house.Add(new Storey()); }
        public override House GetResult() { return house; } // Возвращаем построенный продукт
    }

    class Bassement { public Bassement() { Console.WriteLine("Bassement build successful"); } }
    class Storey { public Storey() { Console.WriteLine("Storey build successful"); } }

    class House
    {
        ArrayList partsOfHouse = new ArrayList();
        public void Add(object part)
        { 
            partsOfHouse.Add(part); 
        }
    }

    class Foreman
    {
        Builder builder;
        public Foreman(Builder builder) { this.builder = builder; }

        public void Construct()       // Через Foreman (прораба) вызываем Builder (строителя), который будет строить House
        {            
            builder.BuildBassement(); // Вызываем методы в правильном порядке
            builder.BuildStorey();
        }
    }

    class Program
    {
        static void Main_()
        {
            Builder builder = new ConcreteBuilder();
            Foreman foreman = new Foreman(builder); // Какого builder подставим, такой результат (House) и получим
            foreman.Construct();                    // Конструируем House
            House house = builder.GetResult();      // Возвращаем результат
        }
    }
}
//!
namespace Prototype
{
    // Есть пустой пакет (прототип), а нам нужен полный с соком
    // Говорим пакету об этом, он создает свою копию и заполняет соком
    // Позволяет создавать объекты на основе созданных ранее объектов-прототипов, то есть клоны
    // Вместо данного паттерна лучше использовать интерфейс ICloneable
    abstract class Prototype
    {
        public int Id { get; private set; }
        public Prototype(int id) { this.Id = id; }
        public abstract Prototype Clone(); // Метод порождает подобных себе
    }

    class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(int id) : base(id) { }
        public override Prototype Clone() { return new ConcretePrototype1(Id); }
    }

    class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(int id) : base(id) { }
        public override Prototype Clone() { return new ConcretePrototype2(Id); }
    }

    class A
    {
        static void Main_()
        {
            Prototype prototype = null;
            Prototype clone = null;
            prototype = new ConcretePrototype1(1);  // Создаем прототип
            clone = prototype.Clone();              // На осове прототипа создаем клон
            Console.WriteLine(clone);
            prototype = new ConcretePrototype2(2);  // Создаем другой прототип
            clone = prototype.Clone();              // На осове прототипа создаем клон 
            Console.WriteLine(clone);
        }
    }
}
//+
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
//+
namespace Adapter
{
    interface ITarget { void Request(); }

    // Адаптер уровня классов
    // Преобразует интерфейс одного класса в интерфейс другого, как переходник меджу обычными и китайскими розетками
    class Adaptee
    {
        public void SpecificRequest() { Console.WriteLine("Specific request"); }
    }

    // Недостаток - не всегда есть свободный слот для наследования от класса,
    // ведь наследоваться можно лишь от 1 класса
    class Adapter : Adaptee, ITarget
    {
        public void Request() { SpecificRequest(); } // Через адаптер вызываем нужный метод
    }

    class A
    {
        static void Main_()
        {
            ITarget target = new Adapter();
            target.Request();
        }
    }
}
//+
namespace Adapter_2
{
    // Адаптер уровня объектов
    class Adaptee
    {
        public void SpecificRequest() { Console.WriteLine("Specific request"); }
    }

    abstract class Target { public abstract void Request(); }

    class Adapter : Target
    {
        public override void Request()
        {
            Adaptee adaptee = new Adaptee();
            adaptee.SpecificRequest(); // Через адаптер вызываем нужный метод
        }
    }

    class A
    {
        static void Main_()
        {
            // Создается впечатление, что мы вызываем Request, а не SpecificRequest
            // Пользователь может и не должен знать, что работает с адаптером
            Target target = new Adapter();
            target.Request();
        }
    }
}
//+
namespace Bridge
{
    // Если требуется работать на разных автомобилях, то садясь в новый автомобиль нужно знать, как им управлять
    // Есть абстракция в виде руля
    // Мы задаем правила изготовления автомобилей,
    // но за счет сохранения общих правил взаимодействия можем одинаково управлять каждым
    // Мостом является пара двух объектов - конкретного автомобиля и правил взаимодействия с ним
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
    // Минимизирует различия в управлении как группами объектов, так и индивидуальными объектами
    // Например, существует алгоритм управления роботами, определяющий способ управления
    // Не важно, кому отдается команда - одному роботу или группе
    // В алгориитм нельзя включить команду, которую может
    // исполнить только один робот, но не может исполнить группа или наоборот
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
    // Расширяет исходный объект до требуемого вида
    // Можем считать декоратором человека с кистью и красной краской
    // Какой бы объект мы не передали декоратору, на выходе будем получать красные объекты
    // Динамически добавляет объекту новые обязанности
    // Например, есть Карлсон, на него надеваем комбинезон, а на него пропеллер
    abstract class Component { public abstract void Operation(); }

    // Карлсон
    class ConcreteComponent : Component
    {
        public override void Operation() { Console.WriteLine("Concrete component"); }
    }

    // Декорации Карлсона
    abstract class Decorator : Component
    {
        public Component Component { protected get; set; }

        public override void Operation()
        {
            if (Component != null)
                Component.Operation();
        }
    }
        
    class ConcreteDecoratorA : Decorator // Декторатор Карлсона - Комбинезон
    {
        string addedState = "SomeState";

        public override void Operation()
        {
            base.Operation(); // Вызываем метод базового класса
            Console.WriteLine(addedState);
        }
    }
        
    class ConcreteDecoratorB : Decorator // Декторатор Карлсона - Попеллер
    {
        void AddedBehaviour() { Console.WriteLine("Behaviour"); }

        public override void Operation()
        {
            base.Operation(); // Вызываем метод базового класса
            AddedBehaviour(); // Вызываем дополнительный метод
        }
    }

    class A
    {
        static void Main_()
        {
            Component component = new ConcreteComponent();   // Создаем Карлсона
            Decorator decoratorA = new ConcreteDecoratorA(); // Создаем комбинезон
            Decorator decoratorB = new ConcreteDecoratorB(); // Создаем пропеллер

            decoratorA.Component = component;   // Комбинезон ссылается на Карлсона
            decoratorB.Component = decoratorA;  // Пропеллер ссылается на комбинезон
            decoratorB.Operation();             // Запускаем пропеллер
        }
    }
}
//+ Несколько методов вызываем в одном методе
namespace Facade
{
    // Делает сложные вещи простыми
    // Если бы управление автомобилем происходило по-другому - нажать одну кнопку чтобы подать питание с аккумулятора,
    // другую чтобы подать питание на инжектор, третью чтобы включить генератор, это было бы сложно
    // Такие сложные наборы действий заменяются простыми, поворот ключа зажигания и будет фасадом
    // Фасад определяет интерфейс более высокого уровня, который упрощает использование подсистемы
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
//+ Актер может исполнить несколько ролей
namespace Flyweight
{
    // Один актер может исполнить много разных ролей, чтобы не нанимать много актеров и не платить им много денег,
    // так и здесь один объект может использоваться для разных ролей
    // Когда применяется:
    // - Когда приложение использует большое количество однообразных объектов, из-за чего происходит выделение большого количества памяти
    // - Когда часть состояния объекта, которое является изменяемым, можно вынести во вне
    //   Вынесение внешнего состояния позволяет заменить множество объектов небольшой группой общих разделяемых объектов
    abstract class Flyweight { public abstract void Greeting(string speech); }

    // Актер - может сыграть разные роли
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
    // Сотрудникам одного из подразделений фирмы требуется информация, какого числа бухгалтерия выплатит зарплату
    // Каждый из них может ездить в бухгалтерию для выяснения этого вопроса или подразделение может выбрать человека,
    // а потом выяснить эту информацию у него
    // Этот человек и будет прокси
    // Когда применяется:
    // - Когда надо осуществлять взаимодействие по сети, а объект-проси должен имитировать поведения объекта в
    //   другом адресном пространстве
    //   Прокси снижает издержки при передачи данных через сеть
    // - Когда нужно управлять доступом к ресурсу, создание которого требует больших затрат
    //   Реальный объект создается только когда он понадобится, до этого запросы к нему обрабатывает прокси-объект
    // - Когда необходимо разграничить доступ к вызываемому объекту в зависимости от прав вызывающего объекта
    // - Когда нужно вести подсчет ссылок на объект или обеспечить потокобезопасную работу с реальным объектом
    interface IHuman { void Request(); }
        
    class Operator : IHuman // Оператор, который подключится к суррогату через специальный интерфейс и будет им управлять
    {
        public void Request() { Console.WriteLine("Operator"); }
    }

    class Surrogate : IHuman // Суррогат, к которому подключается оператор
    {
        IHuman @operator;
        public Surrogate(IHuman @operator) { this.@operator = @operator; }
        public void Request() { this.@operator.Request(); }
    }

    class A
    {
        static void Main_()
        {
            IHuman Dima = new Operator();           // Создаем оператора суррогата
            IHuman surrogate = new Surrogate(Dima); // Создаем суррогата и передаем ему его оператора
            surrogate.Request();                    // Отправляем запрос суррогату, а получит его оператор
        }
    }
}

namespace Proxy_2
{
    abstract class Subject { public abstract void Request(); }

    class RealSubject : Subject
    {
        public override void Request() { Console.WriteLine("RealSubject"); }
    }

    class Proxy : Subject
    {
        RealSubject realSubject;

        public override void Request()
        {
            if (realSubject == null)
                realSubject = new RealSubject();

            realSubject.Request();
        }
    }

    class A
    {
        static void Main_()
        {
            Subject subject = new Proxy();
            subject.Request();
        }
    }
}

// Поведенческие

namespace ChainOfResponsibility
{
    // Требуется получить справку из банка, но не ясно, кто должен ее дать
    // Приходит в банк, там говорят что нужно идти в другое отделение
    // Идем в другое, там отвечают, что они этим не занимаются, идем в региональное отделение и там получаем справку
    // Так паттерн реализует цепочку, отдельные объекты которой должны обработать запрос
    // Запрос может быть обработан в первом отделении или в нескольких, в зависимости от запроса и обрабатывающих объектов
    // Позволяет избежать привязки отправителя запроса к получателю, давая шанс обработать запрос нескольким объектам
    // Когда применяется:
    // - Когда имеется более одного объекта, который может обработать запрос
    // - Когда надо передать запрос на выполнение одному из нескольких объект, точно не определяя, какому именно объекту
    abstract class Handler
    {
        public Handler Succesor { get; set; }
        public abstract void HandlerRequest(int request);
    }

    class ConcreteHandler1 : Handler
    {
        public override void HandlerRequest(int request)
        {
            if (request == 1)          Console.WriteLine("One");
            else if (Succesor != null) Succesor.HandlerRequest(request);
        }
    }

    class ConcreteHandler2 : Handler
    {
        public override void HandlerRequest(int request)
        {
            if (request == 2)          Console.WriteLine("Two");
            else if (Succesor != null) Succesor.HandlerRequest(request);
        }
    }

    class A
    {
        static void Main_()
        {
            Handler h1 = new ConcreteHandler1();
            Handler h2 = new ConcreteHandler2();
            h1.Succesor = h2;
            h1.HandlerRequest(1);
            h2.HandlerRequest(2);
        }
    }
}

namespace Command
{
    // Выключатели дома разъединяют/соединяют два провода, но выключателю неизвестно, что за ними стоит
    // Паттерн определяет общие правила для объектов, в виде соединения двух проводов для выполнения команды,
    // а что именно будет выполнено уже определяет объект
    // Можем включать одним типом выключателей как свет, так и пылесос
    // Когда применяется:
    // - Когда надо передавать в качестве параметров определенные действия, вызываемые в ответ на другие действия
    //   То есть когда необходимы функции обратного действия в ответ на определенные действия
    // - Когда необходимо обеспечить выполнение очереди запросов, а также их возможную отмену
    abstract class Command
    {
        protected ArithmeticUnit unit;      // Ссылка на арифметическое устройство
        protected int operand;
        public abstract void Execute();     // Выполнить
        public abstract void UnExecute();   // Отменить
    }

    class ArithmeticUnit
    {
        public int Register { get; private set; }

        public void Run(char operationCode, int operand)
        {
            switch (operationCode)
            {
                case '+': Register += operand; break;
                case '-': Register -= operand; break;
                case '*': Register *= operand; break;
                case '/': Register /= operand; break;
            }
        }
    }
    
    class Add : Command // Сложение - реализуем не методом, а объектом
    {
        public Add(ArithmeticUnit unit, int operand)
        {
            this.unit = unit;
            this.operand = operand;
        }   
        
        public override void Execute() { unit.Run('+', operand); }      // Сложение - выполнение        
        public override void UnExecute() { unit.Run('-', operand); }    // Вычитание - отмена сложения
    }
    
    class Div : Command // Деление - реализуем не методом, а объектом
    {
        public Div(ArithmeticUnit unit, int operand)
        {
            this.unit = unit;
            this.operand = operand;
        }  
        
        public override void Execute() { unit.Run('/', operand); }      // Деление - выполнить
        public override void UnExecute() { unit.Run('*', operand); }    // Умножение - отменить деление
    }
        
    class Calculator // .. аналогично реализуется вычитание и умножение
    {
        ArithmeticUnit arithmeticUnit;
        ControlUnit controlUnit;

        public Calculator()
        {
            arithmeticUnit = new ArithmeticUnit();
            controlUnit = new ControlUnit();
        }

        private int Run(Command command)
        {
            controlUnit.StoreCommand(command);
            controlUnit.ExecuteCommand();
            return arithmeticUnit.Register;
        }

        public int Add(int operand) { return Run(new Add(arithmeticUnit, operand)); }
        public int Div(int operand) { return Run(new Add(arithmeticUnit, operand)); }

        public int Undo(int levels) {
            controlUnit.Undo(levels);
            return arithmeticUnit.Register;
        }

        public int Redo(int levels)
        {
            controlUnit.Redo(levels);
            return arithmeticUnit.Register;
        }
    }

    class ControlUnit
    {
        private List<Command> commands = new List<Command>();
        private int current = 0;
        public void StoreCommand(Command command) { commands.Add(command); }

        public void ExecuteCommand() {
            commands[current].Execute();
            current++;
        }

        public void Undo(int levels)
        {
            for (int i = 0; i < levels; i++)
                if (current > 0)
                    commands[--current].UnExecute();
        }

        public void Redo(int levels)
        {
            for (int i = 0; i < levels; i++)
                if (current < commands.Count - 1)
                    commands[current++].Execute();
        }
    }

    class A
    {
        static void Main_()
        {
            var calculator = new Calculator();
            int result = 0;
            result = calculator.Add(5);
            Console.WriteLine(result);
            result = calculator.Div(3);
            Console.WriteLine(result);
            result = calculator.Undo(2);
            Console.WriteLine(result);
            result = calculator.Redo(1);
            Console.WriteLine(result);
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
//?
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
//?
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
//?
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
//+ Хочу права, есть много денег
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
//+
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
