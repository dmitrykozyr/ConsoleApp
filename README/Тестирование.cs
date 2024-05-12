Unit Test Frameworks:

MS Test			| NUnit			| xUnit
-----------------------------------------------
много фич		| много фич	    | самый новый
медленный		| быстрый		| быстрый
старый		    | стабильный	| стабильный

xUnit
	AppToTest - проект, который будем тестировать
	На основе xUnit создаем проект xUnit_, в котором будут тесты
	В xUnit_ добавляем ссылку на тестируемый проект, чтобы он его видел (ПКМ -> Add Project Reference)

#region Юнит-тесты

    // Проверяют работу отдельных компонентов (методов, классов)
    // в изолированной части кода без зависимостей от внешних ресурсов (БД, сети)

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

    // Проверяют взаимодействие между различными компонентами или системами в рамках приложения,
    // интеграция компонентов с внешними сервисами, БД и другими системами

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

#region Fake

    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class CalculatorFake
    {
        public int Add(int a, int b)
        {
            return 100;                              // Фейковая реализация метода Add
        }
    }

    static void Main_()
    {
        var calculator = new Calculator();
        Console.WriteLine(calculator.Add(2, 3));     // Выведет 5

        var fakeCalculator = new CalculatorFake();
        Console.WriteLine(fakeCalculator.Add(2, 3)); // Выведет 100
    }

#endregion

