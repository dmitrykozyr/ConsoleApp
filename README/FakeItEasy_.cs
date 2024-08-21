// Юнит тесты
/*
    Хороший юнит тест должен быть:
    - быстрым
    - изолированным от файловой системы и БД
    - повторяемым - должен возвращать один и тот-же результат, если в коде ничего не менялось

    Типы фейков:
    - Mock - файковый объект, который определяет, прошел-ли тест проверку или нет
    - Stub - контроллируемая замена существующей зависимости

    Название теста должно состоять из 3 частей, чтобы понимать его суть, не глядя на код самого теста:
    - имя тестируемого метода
    - успешный сценарий
    - ожидаемое поведение, когда запускается сценарий
    Пример: Add_SingleNumber_ReturnsSameNumber

    Следует избегать логики в тестах:
    - if
    - while
    - for
    - switch

    Не прописывать в конструкторе много логики для всех тестах,
    лучше прописать в каждом тесте отдельно, чтобы тесты были менее связаны

    В каждом тесте должен быть один act

    Не тестировать private - методы, т.к. это внутренняя реализация
    Лучше протестировать public - метод, который вызывает этот private - метод
*/

// Интеграционные тесты
/*
    - задействуют БД
    - задействуют файловую систему
    - задействуют сеть
    - задействуют реальные компоненты системы, используемые в продакшене, не фейки
    - проверяют, что два и более компонента системы работают вместе корректно
*/

//###################### СОЗДАНИЕ ФЕЙКОВЫХ ОБЪЕКТОВ ######################

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
// Способ 1
var foo = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(() => new FooClass("foo", "bar")));
// Способ 2
var foo = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(new object[] { "foo", "bar" }));
// Способ 3
var foo = A.Fake<FooClass>(x => x.Implements<IFoo>());

//################################ A.Call ################################

//########## A.CallTo

// Когда в следующий раз будет вызван метод GetTopSellingCandy(), он вернет значение result
A.CallTo(() => fakeShop.GetTopSellingCandy()).Returns(result);

// Когда в следующий раз обратимся к свойству Address, вернется указанное значение
A.CallTo(() => fakeShop.Address).Returns("123 Fake Street");

// Делегаты, вызов Invoke здесь опционален
var deepThought = A.Fake<Func<string, int>>();
A.CallTo(() => deepThought.Invoke("String 1")).Returns(1);
A.CallTo(() => deepThought("String 2")).Returns(2);

// Создаем фейковый объект, через который можем вызывать методы интерфейса ICandyShop
// Затем настаиваем, что при вызове метода GetTopSellingCandy() вернется result
var fakeShop = A.Fake<CandyShop>(options => options.Implements<ICandyShop>());
A.CallTo(() => ((ICandyShop)fakeShop).GetTopSellingCandy()).Returns(result);

// При вызове любого метода через объект fakeShop, вызовется исключение
A.CallTo(fakeShop).Throws(new Exception());

// При вызове метода с возвращаемым типом void через объект fakeShop, вызовется исключение
A.CallTo(fakeShop).WithVoidReturnType().Throws("some message");

// При вызове метода с возвращаемым типом string через объект fakeShop, вернется сообщение
A.CallTo(fakeShop).WithReturnType<string>().Returns("some message");

// При вызове метода с возвращаемым типом НЕ void через объект fakeShop, вернется сообщение
A.CallTo(fakeShop).WithNonVoidReturnType().Returns("some message");

// При вызове любого метода с более чем 4 аргументами через объект fakeShop, вызовется исключение
A.CallTo(fakeShop).Where(call => call.Arguments.Count > 4).Throws(new Exception("some message");

// При вызове метода с именем MethodName и типом возвращаемого значения double через объект fakeShop, вернется '4741.71'
A.CallTo(fakeShop).Where(call => call.Method.Name == "MethodName").WithReturnType<double>().Returns(4741.71);

// При вызове метода с именем MethodName через объект fakeShop, вызовется исключение
A.CallTo(fakeShop).Where(call => call.Method.Name == "MethodName").Throws(new Exception("some message"));

//########## A.CallToSet

// Когда значение св-ва будет равно указанному, будет вызван метод CallsBaseMethod()
A.CallToSet(() => fakeShop.Address).To("123 Fake Street").CallsBaseMethod();
A.CallToSet(() => fakeShop.Address).To(() => A<string>.That.StartsWith("123")).DoesNothing();
A.CallToSet(() => fakeShop.Address).DoesNothing();

//########## События и делегаты

// При добавлении обработчика события "MyEvent" будет выполнен указанный делегат
A.CallTo(fake, EventAction.Add("MyEvent")).Invokes((EventHandler h) => ...);

// При вызове удаления обработчика события "MyEvent" будет выполнен указанный делегат
A.CallTo(fake, EventAction.Remove("MyEvent")).Invokes((EventHandler h) => ...);

// При вызове добавления обработчика события без конкретного имени будет выполнен указанный делегат
A.CallTo(fake, EventAction.Add()).Invokes(...);

// При вызове удаления любого обработчика события будет выполнен указанный делегат
A.CallTo(fake, EventAction.Remove()).Invokes(...);

//########## Возвращаемые значения
A.CallTo(() => fakeShop.GetTopSellingCandy()).Returns(result);
A.CallTo(() => fakeShop.Address).Returns("some message");
A.CallTo(() => fakeShop.SellSweetFromShelf()).ReturnsNextFromSequence(result, smarties, wineGums);

// При вызове метода NumberOfSweetsSoldToday() будет возвращено текущее значение sweetsSold, увеличенное на 1
// ReturnsLazily означает, что значение будет вычисляться каждый раз при вызове метода
int sweetsSold = 0;
A.CallTo(() => fakeShop.NumberOfSweetsSoldToday()).ReturnsLazily(() => ++sweetsSold);

// Если переданная дата — воскресенье, метод вернет 0 (то есть, в это время сладости не продаются), иначе вернет 200
// ReturnsLazily позволяет динамически определять возвращаемое значение в зависимости от переданной даты
A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(A<DateTime>.Ignored))
    .ReturnsLazily((DateTime theDate) => theDate.DayOfWeek == DayOfWeek.Sunday ? 0 : 200);

// Возвращаемое значение определяется с помощью функции calculateReturnFrom, которая принимает objectCall
// Это позволяет выполнять произвольные вычисления на основе входных данных при каждом вызове
A.CallTo(() => fakeShop.SomeCall(…)).ReturnsLazily(objectCall => calculateReturnFrom(objectCall));

//########################## Throwing Exceptions #########################

// Генерация исключения

// Для конструктора с параметрами
A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(DateTime.MaxValue))
 .Throws(new InvalidDateException("the date is in the future"));

// Для конструктора без параметров 
A.CallTo(() => fakeShop.NumberOfSweetsSoldOn(DateTime.MaxValue))
 .Throws<InvalidDateException>();

// Для асинхронного метода
A.CallTo(() => fakeShop.OrderSweetsAsync("cheeseburger"))
 .ThrowsAsync(new ArgumentException("'cheeseburger' isn't a valid sweet category"));

//############################# Doing Nothing ############################

// Если нужно, чтобы вызов метода был проигнорирован
A.CallTo(() => aFake.SomeVoidMethodThatShouldDoNothing())
 .DoesNothing();

//#################### Присвоение ref и out параметров ###################

A.CallTo(() => aFake.AMethod(anInt, ref aRef, out anOut))
 .Returns(true)
 .AssignsOutAndRefParameters("new aRef value", "new anOut value");

// Если значения параметров ref и out неизвестны до вызова метода
string theValue;
A.CallTo(() => aFake.AMethod(anInt, ref aRef, out anOut))
 .Returns(true)
 .AssignsOutAndRefParametersLazily((int someInt, string someRef, string someOut) =>
     new[] { "new aRef value: " + someInt, "new anOut value" });

//########################## Invoking Custom Code ########################

// Иногда недостаточно стандартного поведения фейков в виде
// - определения возвращаемного значения
// - генерации исключения
// - назвачении параметров ref и out
// - ничего не делания

// После вызова метода SellSmarties() вызовется метод OrderMoreSmarties()
A.CallTo(() => fakeShop.SellSmarties())
 .Invokes(() => OrderMoreSmarties()) // simulate Smarties stock falling too low
 .Returns(20);

A.CallToSet(() => fakeShop.OpeningHours).Invokes(TimeRange newTimes) =>
{
    // have the getter return the new times when called
    A.CallTo(() => fakeShop.OpeningHours).Returns(newTimes);

    // custom action - notify listeners of the change
    fakeShop.OpeningHoursChanged += Raise.With(new HoursChangedEvent(newTimes));
}

//########################## Calling base methods ########################

// Для одного метода
A.CallTo(() => fakeShop.SellSmarties())
 .CallsBaseMethod();

// Для нескольких методов
var fakeShop = A.Fake<CandyShop>(options => options.CallsBaseMethods());

//##################### Changing behavior between calls ##################

A.CallTo(() => fakeShop.Address).Returns("123 Fake Street").Once();
A.CallTo(() => fakeShop.Address).Returns("123 Fake Street").Twice();
A.CallTo(() => fakeShop.Address).Returns("123 Fake Street").NumberOfTimes(17);

// Configure the method to throw an exception once, then succeed forever
A.CallTo(() => fakeService.DoSomething())
    .Throws<Exception>().Once()
    .Then
    .Returns("SUCCESS");

// set up an action that can run forever, unless superseded
A.CallTo(() => fakeService.DoSomething()).Returns("SUCCESS");

// set up a one-time exception which will be used for the first call
A.CallTo(() => fakeService.DoSomething()).Throws<Exception>().Once();


























