MS Test			| NUnit			| xUnit
-----------------------------------------------
много фич		| много фич	    | самый новый
медленный		| быстрый		| быстрый
старый		    | стабильный	| стабильный

#region Юнит-тесты

    /*
        Хороший юнит тест должен быть:
        -быстрым
        - изолированным от файловой системы и БД
        - должен возвращать одинаковый результат, если в коде ничего не менялось

        Название теста должно состоять из 3 частей:
        -имя тестируемого метода
        - успешный сценарий
        - ожидаемое поведение
        Пример: Add_SingleNumber_ReturnsSameNumber

        Избегать логики в тестах:
        - if
        - while
        - for
        - switch

        Не прописывать в конструкторе много логики для всех тестах, лучше прописать в каждом тесте отдельно для меньшей связности
        В каждом тесте должен быть один act
        Не тестировать private -методы - это внутренняя реализация, лучше протестировать public метод, который вызывает этот private метод
    */

    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_WhenCalled_ReturnsSum()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            int result = calculator.Add(3, 5);

            // Assert
            Assert.AreEqual(8, result);
        }
    }

#endregion

#region Функциональные тесты

    // Проверяют работу программы с точки зрения взаимодействия между компонентами

    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_ValidCredentials_SuccessfulLogin()
        {
            // Arrange
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://example.com");

            // Act
            var usernameField = driver.FindElement(By.Id("username"));
            usernameField.SendKeys("testuser");
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("password123");
            var loginButton = driver.FindElement(By.Id("loginButton"));
            loginButton.Click();

            // Assert
            Assert.IsTrue(driver.Url.Contains("dashboard"));

            driver.Quit();
        }
    }

#endregion

#region Интеграционные тесты

    /*
        Задействуют:
        - БД
        - файловую систему
        - сеть
        - реальные компоненты системы, используемые в продакшене, не фейки
        - проверяют, что два и более компонента системы работают вместе корректно
    */

    public interface IDataService
    {
        string GetData();
    }

    public class DataService : IDataService
    {
        public string GetData()
        {
            return "Mocked data";
        }
    }

    public class DataProcessor
    {
        private readonly IDataService _dataService;

        public DataProcessor(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string ProcessData()
        {
            return _dataService.GetData();
        }
    }

    public class DataProcessorTests
    {
        [Fact]
        public void ProcessData_WhenDataServiceReturnsData_ReturnsData()
        {
            // Arrange
            var mockDataService = new Mock<IDataService>();
            mockDataService.Setup(x => x.GetData()).Returns("Mocked data");

            var dataProcessor = new DataProcessor(mockDataService.Object);

            // Act
            string result = dataProcessor.ProcessData();

            // Assert
            Assert.Equal("Mocked data", result);
        }
    }

#endregion


// FAKE IT EASY

#region СОЗДАНИЕ ФЕЙКОВЫХ ОБЪЕКТОВ

    // Создание фейкового объекта
    var foo = A.Fake<IFoo>();


    // Создание фейка-делегата
    var func = A.Fake<Func<string, int>>();


    // Создание коллекции фейков
    var foos = A.CollectionOfFake<Foo>(10);


            // Если тип фейка заранее неизвестен, можно использовать non-generic методы
            using FakeItEasy.Sdk;
            var type = GetTypeOfFake();
    object fake = Create.Fake(type);
    IList<object> fakes = Create.CollectionOfFake(type, 10);


    // Передача аргументов в фейковый конструктор, при этом настоящий конструктор не будет вызван
    var foo1 = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(() =>
    {
    new FooClass("foo", "bar"))
            });

    var foo2 = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(
        new object[]
        {
                    "foo",
                    "bar"
        }));

    var foo3 = A.Fake<FooClass>(x => x.Implements<IFoo>());

#endregion

#region A.Call

    // Когда в следующий раз будет вызван метод GetTopSellingCandy(), он вернет значение result
    A.CallTo(() => fakeShop.GetTopSellingCandy())
        .Returns(result);


    // Когда в следующий раз обратимся к свойству Address, вернется указанное значение
    A.CallTo(() => fakeShop.Address)
        .Returns("123 Fake Street");


    // Делегаты, вызов Invoke здесь опционален
    var deepThought = A.Fake<Func<string, int>>();

    A.CallTo(() => deepThought.Invoke("String 1"))
        .Returns(1);

    A.CallTo(() => deepThought("String 2"))
        .Returns(2);


    // Создаем фейковый объект, через который можем вызывать методы интерфейса ICandyShop
    // Затем настаиваем, что при вызове метода GetTopSellingCandy() вернется result
    var fakeShop = A.Fake<CandyShop>(options => options.Implements<ICandyShop>());

    A.CallTo(() => ((ICandyShop)fakeShop).GetTopSellingCandy())
        .Returns(result);


    // При вызове любого метода через объект fakeShop, вызовется исключение
    A.CallTo(fakeShop)
        .Throws(new Exception());


    // При вызове метода с возвращаемым типом void через объект fakeShop, вызовется исключение
    A.CallTo(fakeShop)
        .WithVoidReturnType()
        .Throws("some message");


    // При вызове метода с возвращаемым типом string через объект fakeShop, вернется сообщение
    A.CallTo(fakeShop)
        .WithReturnType<string>()
        .Returns("some message");


    // При вызове метода с возвращаемым типом НЕ void через объект fakeShop, вернется сообщение
    A.CallTo(fakeShop)
        .WithNonVoidReturnType()
        .Returns("some message");


    // При вызове любого метода с более чем 4 аргументами через объект fakeShop, вызовется исключение
    A.CallTo(fakeShop)
        .Where(call => call.Arguments.Count > 4)
        .Throws(new Exception("some message");


    // При вызове метода с именем MethodName и типом возвращаемого значения double через объект fakeShop, вернется '4741.71'
    A.CallTo(fakeShop)
        .Where(call => call.Method.Name == "MethodName")
        .WithReturnType<double>()
        .Returns(4741.71);


    // При вызове метода с именем MethodName через объект fakeShop, вызовется исключение
    A.CallTo(fakeShop)
        .Where(call => call.Method.Name == "MethodName")
        .Throws(new Exception("some message"));

#endregion

#region A.CallToSet

    // Когда значение св-ва будет равно указанному, будет вызван метод CallsBaseMethod()
    A.CallToSet(() => fakeShop.Address)
        .To("123 Fake Street")
        .CallsBaseMethod();

    A.CallToSet(() => fakeShop.Address)
        .To(() => A<string>.That.StartsWith("123"))
        .DoesNothing();

    A.CallToSet(() => fakeShop.Address)
        .DoesNothing();

#endregion

#region События и делегаты

    // При добавлении обработчика события "MyEvent" будет выполнен указанный делегат
    A.CallTo(fake, EventAction.Add("MyEvent"))
        .Invokes((EventHandler h) => ...);


    // При вызове удаления обработчика события "MyEvent" будет выполнен указанный делегат
    A.CallTo(fake, EventAction.Remove("MyEvent"))
        .Invokes((EventHandler h) => ...);


    // При вызове добавления обработчика события без конкретного имени будет выполнен указанный делегат
    A.CallTo(fake, EventAction.Add())
        .Invokes(...);


    // При вызове удаления любого обработчика события будет выполнен указанный делегат
    A.CallTo(fake, EventAction.Remove())
        .Invokes(...);

#endregion

#region Возвращаемые значения

    A.CallTo(() => fakeShop.GetTopSellingCandy())
        .Returns(result);


    A.CallTo(() => fakeShop.Address)
        .Returns("some message");


    A.CallTo(() => fakeShop.SellSweetFromShelf())
        .ReturnsNextFromSequence(result, smarties, wineGums);

    // При вызове метода NumberOfSweetsSoldToday() будет возвращено текущее значение sweetsSold, увеличенное на 1
    // ReturnsLazily означает, что значение будет вычисляться каждый раз при вызове метода
    int sweetsSold = 0;
    A.CallTo(() => fakeShop.NumberOfSweetsSoldToday())
        .ReturnsLazily(() => ++sweetsSold);


    // Если переданная дата — воскресенье, метод вернет 0 (то есть, в это время сладости не продаются), иначе вернет 200
    // ReturnsLazily позволяет динамически определять возвращаемое значение в зависимости от переданной даты
    A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(A<DateTime>.Ignored))
        .ReturnsLazily((DateTime theDate) => theDate.DayOfWeek == DayOfWeek.Sunday ? 0 : 200);


    // Возвращаемое значение определяется с помощью функции calculateReturnFrom, которая принимает objectCall
    // Это позволяет выполнять произвольные вычисления на основе входных данных при каждом вызове
    A.CallTo(() => fakeShop.SomeCall(…))
        .ReturnsLazily(objectCall => calculateReturnFrom(objectCall));

#endregion

#region Throwing Exceptions

    // Для конструктора с параметрами
    A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(DateTime.MaxValue))
        .Throws(new InvalidDateException("the date is in the future"));


    // Для конструктора без параметров 
    A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(DateTime.MaxValue))
        .Throws<InvalidDateException>();


    // Для асинхронного метода
    A.CallTo(() => fakeShop.OrderSweetsAsync("cheeseburger"))
        .ThrowsAsync(new ArgumentException("'cheeseburger' isn't a valid sweet category"));

#endregion

#region Doing Nothing

    // Если нужно, чтобы вызов метода был проигнорирован
    A.CallTo(() => aFake.SomeVoidMethodThatShouldDoNothing())
        .DoesNothing();

#endregion

#region Присвоение ref и out параметров

    A.CallTo(() => aFake.AMethod(anInt, ref aRef, out anOut))
        .Returns(true)
        .AssignsOutAndRefParameters("new aRef value", "new anOut value");


    // Если значения параметров ref и out неизвестны до вызова метода
    string theValue;

    A.CallTo(() => aFake.AMethod(anInt, ref aRef, out anOut))
        .Returns(true)
        .AssignsOutAndRefParametersLazily((int someInt, string someRef, string someOut) =>
        {
            new[]
            {
                        "new aRef value: " + someInt, "new anOut value"
            }

        });

#endregion
