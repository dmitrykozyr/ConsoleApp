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


