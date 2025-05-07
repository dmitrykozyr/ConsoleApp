//========================== ПЕРЕМЕННЫЕ ==========================
var isDone = false;                           // Объявление переменной в js

let isDone: boolean = false;                  // Объявление переменной в ts
let decimal: number = 6;
let hex: number = 0xf00d;
let binary: number = 0b1010;
let octal: number = 0o744;
let list: number[] = [1, 2, 3];
let anotherList: Array<number> = [1, 2, 3];   // Generic
let unusable: void = void 0;                  // Прячем undefined

let color: string = "blue";
color = 'red';
console.log(color);

let fullName: string = `Bob Bobbington`;
let age: number = 37;
let sentence: string = `Hello, my name is ${fullName}.
I'll be ${age + 1} years old next month.`;

let data: [string, number];                   // Кортежи
data = ['hello', 10];

enum Color { Red, Green, Blue }
let c: Color = Color.Blue;

let n: any = 1;                                // Можно подставить любой тип, лучше не использовать
n = 'string';
n = false;

let obj: object | null;                        // obj может принимать object или null
obj = null;                                    // object - общий тип и лучше указывать более конкретный тип, а то нет типизации
obj = { n: 1 };

//=========================== ФУНКЦИИ ============================
function F1(): void {                          // Типизирование функции
    console.log('Hello');
}

let core: never = (() => {                     // never - ф-я никогда не может быть выполнена
    console.log(true);
    throw new Error('Some Error');
    // return 1;
})();

const show = (msg: string) => {
    console.log(msg);
};

// Функция с кортежами. Передаем массив, берем первое и второе значение, а затем типизируем их типом number
function f([first, second,]: [number, number]) {
    console.log(first);
    console.log(second);
}
f([1, 2]);

const resource = { a: 'a', b: 1 };
let { a, b }: { a: string, b: number } = resource;
console.log(a);
console.log(b);

type C = { a: string, b?: number };             // ? означает, что это необязательное св-во
function showProperties({ a, b }: C): void {
    console.log(a);
    console.log(b);
}
showProperties({ a: 'a', b: 9 });
showProperties({ a: 'a' });

function add(x: number, y: number): number { return x + y; }
const sum: number = add(1, 2);

let myAdd = function (x: number, y: number): number { return x + y; };
const total: number = myAdd(1, 2);

// Типовая сигнатура для ф-ии. Ф-я принимает два параметра number и возвращает number
type f = (baseValue: number, increment: number) => number;
// Для ф-ии ниже указываем сигнатуру <f> и она теперь должна полностью соответствовать этой сигнатуре
let increase = <f>function increase(x: number, y: number): number { return x + y; };
const updatedValue: number = increase(3, 1);
console.log(updatedValue);

// Проверка необязательного параметра на null
function buildName(firstName: string, lastName?: string) {
    if (lastName) return firstName + " " + lastName;
    else return firstName;
}
let result1 = buildName('Oliver');
let result2 = buildName('Oliver', 'Black');
console.log(result1);
console.log(result2);

function buildLetters(firstLetter: string, ...restOfLetters: [string, string, string]) {
    return firstLetter + ' ' + restOfLetters.join(' ');
}
let letters = buildLetters('a', 'b', 'c', 'd');

// Чтобы запретить использовать this, ему можно присвоить void
const run: (this: void, n: number) => void = function (n) {
    // this.n = n;                    // Будет ошибка
    console.log(n);
};
run(1);

// В одном случае ф-я возвращает число, а в другом - строку
function show(n: number): any {
    if (n < 5) { return 'Good'; }
    else { return 100; }
}
const myValue = show(5);

const f = (a: number, b: number): number => a + b;
type FType = (a: number, b: number) => number;
const sum: FType = f; // Указываем, что ф-я f должна соответствовать сигнатуре FType

// В обобщенную ф-ю можно передать и вернуть аргумент любого типа
const returnValueByGeneric = function <T>(arg: T): T {
    return arg;
};
const text: string = returnValueByGeneric<string>('str');
const n: number = returnValueByGeneric<number>(1);

//========================== КЛАССЫ ==============================
class Greeter {
    greeting: string;
    constructor(message: string) { this.greeting = message; }
    greet() { return 'Hello, ' + this.greeting; }
}
let greeter = new Greeter('world');
console.log(greeter.greet());

class Animal {
    move(distanceInMeters: number = 0) { console.log(`Animal moved ${distanceInMeters}m.`); }
}
class Dog extends Animal {
    bark() { console.log('Woof! Woof!'); }
}
const dog = new Dog();
dog.bark();     // Woof! Woof!
dog.move(10);   // Animal moved 10m.

class AnimalCore {
    name: string;
    constructor(theName: string) { this.name = theName; }
    move(distanceInMeters: number = 0) {
        console.log(`${this.name} moved ${distanceInMeters}m.`);
    }
}
class Snake extends AnimalCore {
    constructor(name: string) { super(name); }
    move(distanceInMeters = 5) {
        console.log("Slithering...");
        super.move(distanceInMeters);
    }
}
class Horse extends AnimalCore {
    constructor(name: string) { super(name); }
    move(distanceInMeters = 45) {
        console.log('Galloping...');
        super.move(distanceInMeters);
    }
}
let sam = new Snake('Sammy the Python');
let tom: Animal = new Horse('Tommy the Palomino');
sam.move();
tom.move(34);

// Модификатор доступа private
class AnimalBase {
    private name: string;
    constructor(theName: string) { this.name = theName; }
}
new AnimalBase('Cat').name;     // Ошибка

// Модификатор доступа protected
class Person {
    protected name: string;
    constructor(name: string) { this.name = name; }
}
class Customer extends Person {
    private department: string;
    constructor(name: string, department: string) {
        super(name);
        this.department = department;
    }
    public getElevatorPitch() {
        return `Hello, my name is ${this.name} and I work in ${this.department}.`;
    }
}
let howard = new Customer("Howard", 'Sales');
console.log(howard.getElevatorPitch());
// console.log(howard.name); // error

// Конструктор protected
class Base {
    protected name: string;
    protected constructor(theName: string) { this.name = theName; }
}
class Entity extends Base {
    private department: string;
    constructor(name: string, department: string) {
        super(name);
        this.department = department;
    }
    public getElevatorPitch() {
        return `Hello, my name is ${this.name} and I work in ${this.department}.`;
    }
}
let item1 = new Entity("Howard", "Sales");
// let item2 = new Base("John"); // Error: The 'Person' constructor is protected

// Модификатр readonly
class Octopus {
    readonly name: string;
    readonly numberOfLegs: number = 8;
    constructor(theName: string) {
        this.name = theName;
    }
}
let dad = new Octopus("Man with the 8 strong legs");
// dad.name = "Man with the 3-piece suit"; // error! name is readonly.

// Get Set
class Agent {
    private _fullName: string;
    private _secret: string;
    private _passcode: string;
    get fullName(): string { return this._fullName; }
    set fullName(newName: string) {
        if (this._passcode == this._secret) { this._fullName = newName; }
        else { console.log("Error: Unauthorized update of employee!"); }
    }
    constructor(_passcode: string) {
        this._passcode = _passcode;
        this._secret = 'secret passcode';
    }
}
let agent = new Agent('secret passcode');
agent.fullName = "Bob Smith";
if (agent.fullName) { console.log(agent.fullName); }

// Абстрактрый класс
abstract class Department {
    protected constructor(public name: string) { }
    getId(): string { return this.name + Math.random(); }
    abstract printId(): void;
}
class Item extends Department {
    constructor(public name: string) { super(name); }
    printId(): void {
        const id: string = super.getId();
        console.log(id);
    }
}
const item: Item = new Item('Oliver');          // У абстрактного класса могут быть абстрактные методы
item.printId();

//========================== ИНТЕРФЕЙСЫ ==========================
// Интерфейс можно расширить классом.. теперь я видел все..
class Point {
    x: number;
    y: number;
}
interface Point3d extends Point { z: number; }  // В этом интерфейсе быдут св-ва x, y
let point3d: Point3d = { x: 1, y: 2, z: 3 };
console.log(point3d);

interface LabeledValue { label: string; }
// В ф-ю в качестве агрумента передаем строго типизированное св-во
const printLabel = function (labeledObj: LabeledValue) { console.log(labeledObj.label); };
let myObj = { size: 10, label: "Size 10 Object" };
printLabel(myObj);

// Описываем интерфейс и присваиваем переменной его экзампляр, после этого этой переменной
// можно присвоить только ф-ю с той-же сигнатурой
interface SearchFunc { (source: string, subString: string): boolean; }
let mySearch: SearchFunc;
mySearch = function (src: string, sub: string): boolean {
    let result = src.search(sub);
    return result > -1;
}

// Класс реализует интерфейс и должен реализовать его методы
interface ClockInterface { currentTime: Date; }
class Clock implements ClockInterface {
    currentTime: Date = new Date();
    constructor(h: number, m: number) { }
}

// Создаем экзампляр интерфейса Square, который наследует Shape
// Мы можем описать только два св-ва из этих интерфейсов, потому что явно аказываем тип при создании экземпляра
interface Shape { color: string; }
interface Square extends Shape { sideLength: number; }
let square = <Square>{};
square.color = "blue";
square.sideLength = 10;

// Интерфейс может наследоваться от нескольких интерфейсов
interface Shape { color: string; }
interface PenStroke { penWidth: number; }
interface Square extends Shape, PenStroke { sideLength: number; }
let square = <Square>{};
square.color = "blue";
square.sideLength = 10;
square.penWidth = 5.0;

//========================== СЛОЖНЫЕ ТИПЫ ========================
const sum = (a: string, b: string | number) => { return a + b; };     // b может принимать как string так и number