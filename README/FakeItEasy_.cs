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

// Когда в следующий раз будет вызван метод GetTopSellingCandy(), он вернет значение lollipop
A.CallTo(() => fakeShop.GetTopSellingCandy()).Returns(lollipop);

// Когда в следующий раз обратимся к свойству Address, вернется указанное значение
A.CallTo(() => fakeShop.Address).Returns("123 Fake Street");

//########## A.CallToSet

// Когда значение св-ва будет равно указанному, будет вызван метод CallsBaseMethod()
A.CallToSet(() => fakeShop.Address).To("123 Fake Street").CallsBaseMethod();
A.CallToSet(() => fakeShop.Address).To(() => A<string>.That.StartsWith("123")).DoesNothing();
A.CallToSet(() => fakeShop.Address).DoesNothing();








