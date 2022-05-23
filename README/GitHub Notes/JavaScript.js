Разное
Функции
Селекторы
Изменение элементов
События
JSON, localStorage
Классы
Оператор Spread
Promise

//============================ РАЗНОЕ ============================
console.log(string1 + ' ' + string2);
console.warn('');
console.error('');
alert('');
prompt('');
confirm('');

const var1 = 5;
const var10 = `Number: ${var1} and ${var1}`;    // В шаблонную строку можно вставлять переменные

let sum = .1 + .2;
console.log(sum.toFixed(3));                    // Отображение трех чисел после запятойб вернется строка '0.300'
console.log(+sum.toFixed(3));                   // Вернется число 0.3
console.log(typeof var1);                       // Определение типа данных

let obj1 = new Object();                        // Создание объекта
let obj2 = {};
let obj3 = {
    name: 'Dima',
    age: 29,
    'is Dima amazing?': true                    // Имя свойства может содержать пробелы
};

console.log(obj3.name);
console.log(obj3["is Dima amazing?"]);

const input = prompt('Enter a property');
const hasProp = input in obj3;                  // Если в браузере ввести свойство name, то в переменную запишется его значение
// in - если в obj3 есть такое свойство, то вернется true, иначе - false
if (hasProp)
    delete obj3[input];                         // Если такое свойство есть, то удалим его
else
    obj3[input] = null;                         // Если св-ва нет, оно автоматически создастся, запишем в него null

let obj1 = {                                    // Копирование типов
    age: 18
};
let obj2 = obj1;
obj1.age = 20;
console.log(obj2.age);                          // 20 скопировалась ссылка на исходный тип

let obj3 = {
    age: 5
};
let obj4 = Object.assign({}, obj3);
obj3.age = 7;
console.log(obj4.age);                          // 5, новый тип скопировался в новый объект и не зависит от исходного типа

// Object.assign добавляет в объект новые св-ва. Если св-во уже есть, оно будет перезаписано
let obj5 = { width: 1 };
Object.assign(obj5, { width: 2, height: 3 });
console.log(obj5);

// Деструктуризация позволяет записывать значения отдельных св-в в отдельные переменные
// Имена переменных должны совпадать, порядок не важен
let obj7 = {
    prop1: 1,
    prop2: 2,
    prop3: 3,
    prop4: 4,
    prop5: {                                        // Вложенный объект
        prop6: 6,
        prop7: 7
    }
};
let { prop3, prop2 = 5, prop1 } = obj7;         // Можно задать значение по умолчанию
let { prop4, ...obj8 } = obj7;                  // Можно записать часть св-в в переменные, а оставшиеся в новый объект obj8

// Массивы
const arr1 = [1, 2, 3];
arr1.push(4);                                   // Добавление элемента в конец массива
arr1.unshift(0);                                // Добавление элемента в начало массива
arr1[10] = 10;                                  // Добавление элемента в конкретный индекс, недостающие элементы будут undefined
delete arr1[2];                                 // Удаление элемента, вместо него будет undefined
arr1.splice(1, 1);                              // Удажение элемента, индексы остальных элементов сдвинутся влево на 1
const val1 = arr1.pop();                        // Извлечение из массива последнего элемента и сохранение в переменную
const val2 = arr1.shift();                      // Извлечение из массива первого элемента и сохранение в переменную

const arr3 = [1, 2];
const arr4 = [3, 4];
const arr5 = arr3.concat(arr4);                 // Объединение двух массивов
const arr6 = arr5.concat(5, 6);                 // Добавление элментов в массив

const arr7 = arr6.concat();                     // Массив - ссылочный тип, concat копирует его в новый участок памяти
const val3 = arr7.join(' ');                    // Объединение всех элементов массива в строку, в скобках указываем разделитель

arr7.forEach(function (i) {
    console.log(i);
});

let val4 = arr7.find(function (el) {             // find находит и возвращает первое совпадение по заданному условию
    return el > 3;
});

var managers = [                                // filter возвращает все совпадения
    { "vacancyId": 2194, "managerINN": 1 },
    { "vacancyId": 2400, "managerINN": 2 },
    { "vacancyId": 2400, "managerINN": 3 }
];
var searchData2 = managers.filter(function (z) { return z.vacancyId == 2400; });

var result = searchData2.map(z => z.managerINN);    // map выбирает из коллекции объектов одно поле

localStorage.setItem("number", 1);              // Сохраняем для ключа 'number' значение 1 в localStorage
// Посмотреть: в браузере F12 -> Application -> LocalStorage
console.log(localStorage.getItem("number"));    // Получить значение по ключу
localStorage.removeItem("number");              // Удалить ключ
localStorage.clear();                           // Очистить все хранилище

try {
    console.log('Good 1');
    console.log(a);                             // Пытаемся вывести несуществующую переменную
    console.log('Good 2');                      // Это не вызовется
} catch (error) {
    console.log(error);
} finally {
    console.log('Good 3');
}

// Геттеры / Сеттеры
function User(age) {
    let userAge = age;
    this.getAge = function () { return userAge; }
    this.setAge = function (age) {
        if (typeof age === 'number' && age > 0 && age < 110)
            userAge = age;
    }
}
let user = new User(1);
console.log(user.getAge()); // 1
user.setAge(2);
console.log(user.getAge()); // 2

window.addEventListener('scroll', F1);          // Ф-я вызывается во время скролла
function F1() { console.log('scroll'); }

//============================ ФУНКЦИИ ===========================
function sum(a, b) { return a + b; }
const f1 = function (a) { return a + 1; }       // Анонимная ф-я
const f2 = (a) => { return a + 1; }             // Стрелочная ф-я
console.log(f2(1));

function f3() {
    let var2 = 'value1';
    return function f4() {
        console.log(var2);
    };
};
const var1 = f3();
let var2 = 'value2';
var1();                                         // Ф-ю можно записать в переменную и потом вызывать ее через эту переменную
// В консоль выведет 'value1'

function f1() { console.log('+') };
const var1 = setTimeout(f1, 1000);              // Асинхронный вызов функции через 1 секунду
clearTimeout(var1);                             // Отмена асинхронного вызова ф-ии с идентификатором var1

function f2(a, b) { console.log(a + b) };
const var2 = setTimeout(f2, 1000, 1, 2);        // 1 и 2 - аргументы

setInterval(() => { console.log('+'), 1000 });  // Многократный вызов через определенный интервал

// Асинхронная callback ф-я
const f1 = (cb) => {                            // cb - это callback ф-я
    console.log('1');
    setTimeout(() => {
        console.log('2');
        const data = { text: 'Hello' };
        cb(data);                               // В callback ф-ю можно передать аргумент
    }, 2000);
}

const f2 = (data) => { console.log('3', data); }
f1(f2);                                         // Передаем ф-ю F2 как аргумент, тогда она запустится там, где вызывается cb()
// Выведет '1 2 3 Hello'

function f2() { console.log(2); }               // Способы вызова callback - ф-ии
function f3(var1, var2) {
    console.log(var1);
    var2();
}
f3(1, f2);
f3("1", function () {
    console.log('3');
});

f1((data) => { console.log('4', data); });      // Аналогичный вызов через анонимную ф-ю

// В Promise передаем ф-ю с 2мя параметрами
// Если ф-я отработала успешно, вызываем resolve, иначе reject
const var1 = new Promise((resolve, reject) => {
    setTimeout(() => {
        resolve();
        //reject();
    }, 2000);
});

var1.then(() => { console.log('1'); },          // Соответствует вызову resolve()
    () => { console.log('2'); });         // Соответствует вызову reject()
// `1`

// Контекст вызова ф-ии
// Внутри типа var1 определили поле name ф-ю f2(), в которой использовали данное поле
// Это поле так-же можно изменять
const var1 = {
    name: 'Dima Amazing',
    f1() { console.log(`Hi ${this.name}`); },
    f2() { this.name = 'I love burgers'; }
}
var1.f2();
var1.f1();                                      // `Hi I love burgers`

// Одна и та-же ф-я может возвращать разные значения, в зависимости от контекста
function f3() { console.log(`Hi ${this.name}`); }
const var2 = { name: 'N2' };
const var3 = { name: 'N3' };
var2.f = f3;    // f - просто идентификатор, может быть любым
var3.f = f3;
var2.f();       // `Hi N2`
var3.f();       // `Hi N3`

const calc = {
    a: 1,
    b: 2,
    sum() { console.log(this.a + this.b) }      // При обращении к внутренним полям, обязательно указываем this
}
calc.sum();

// Нужно вызвать ф-ю f1() для объекта var2, у которого нет этой ф-ии
const var1 = {
    name: 'N1',
    f1() { console.log(`f1 ${this.name}`); }
}
const var2 = {
    name: 'N2'
}
// Можно объекту var2 присвоить ф-ю f1() и вызвать ее
var2.f1 = var1.f1;
var2.f1();

// Чтобы не изменять var2, можно использовать ф-ю call
var1.f1.call(var2);

// call позволяет вызвать ф-ю, которая не является частью какого-либо объекта
function f2() { console.log(`f2 ${this.name}`); }
f2.call(var2);

const var3 = { num: 45 }
function sum(a) { return a + this.num; }
const var4 = sum.call(var3, 5);
const var5 = sum.apply(var3, [5]);              // apply работает как call, но аргументы передаются массивом
console.log(var4);  // `50`

// bind позволяет изменять контекст, не вызывая ф-ю
// Можно записать куда-то эту ф-ю и в дальнейшем вызвать, она будет работать с тем контекстом, который мы передадим
const var6 = { num: 1 }
function f6(a, b) { return (a + this.num) * b; }
const var7 = f6.bind(var6);
const var8 = var7(1, 2);
console.log(var8);  // `4`

//============================ СЕЛЕКТОРЫ =========================
// Можем выбрать из HTML любой объект и изменить его
const var2 = document.querySelector('.last');   // div          селектор по тегу        <div>
var2.style.color = '#f00';                      // .className   селектр по классу       <li class="last">Third</li>
console.log(var2);                              // #block       селектор по id          <li id="last">Third</li>
// [type-text]  селектор по аттрибуту   <input type="text">

<body>
    <ul>
        <li>First</li>
        <li>Second</li>
        <li class="last">Third</li>
    </ul>
    <script src="./script.js"></script>         // Скрипт нужно подключить после объявления элементов li, чтобы работать с ними
</body>

//============================ ИЗМЕНЕНИЕ ЭЛЕМЕНТОВ ===============
const var1 = prompt('Enter a word');
const var2 = document.querySelector('.title');
var2.innerText = var1;                          // innerText перезаписывает значение внутри тега
var2.innerHTML = `<i>${var1}</i>`;              // innerHTML перезаписывает значение тега и его содержимого

<body>
    <h1 class="title">hello</h1>
    <script src="./script.js"></script>
</body>

const el = document.createElement('p');         // Чтобы добавить новый элемент, его нужно создать
outer.appent(el);                               // а затем положить внутрь другого элемента
el.remove();                                    // Удаление элемента
el.hidden = true;                               // Вместо удаления элемент можно скрыть

//----------------------------------------------------------------
// В HTML есть 'div' с именем 'messages', в него добавим тег 'p' а в него введенное пользователем значение
const input = prompt('Enter text');
const message = document.createElement('p');
message.innerText = input;
const messageBox = document.querySelector('.messages');
messageBox.append(message);

setTimeout(hideMessage, 2000);                  // Через 2 сек вызовем ф-ю, которая скроет сообщение
function hideMessage() { message.hidden = true; }
function removeMessage() { message.remove(); }

<body>
    <div class="messages">
    </div>
    <script src="./script.js"></script>
</body>

//============================ СОБЫТИЯ ===========================
function buttonClick() {                        // Обработка нажатия кнопки
    const text = getInputText();
    addElement(text);
    clearInput();
}

function getInputText() {                       // Ищем поле ввода и считываем его значение
    const input = document.querySelector('.text-field');
    return input.value;
}

function addElement(text) {                     // Создание нового элемента с введенным текстом
    const listItem = document.createElement('li');
    listItem.innerText = text;
    const list = document.querySelector('.list');
    list.append(listItem);
}

function clearInput() {                         // Очистка поля ввода после нажатия на кнопку
    const inputField = document.querySelector('.text-field');
    inputField.value = '';
}

<body>
    <input class="text-field" type="text">
        <button onclick="buttonClick()">
            Send
        </button>
        <ul class="list">
        </ul>
        <script src="./script.js"></script>
</body>

//----------------------------------------------------------------
    const button = document.querySelector('.button');
    button.addEventListener('click', hideText);     // К стандартному событию 'click' привязываем нашу ф-ю

    function hideText() {
    const text = document.querySelector('.text');
    text.hidden = true;

    const button = document.querySelector('.button');
    button.removeEventListener('click', hideText);  // Удаляем обработчик событий
    button.addEventListener('click', showText);     // и добавляем кнопке новый
}

    function showText() {
    const text = document.querySelector('.text');
    text.hidden = false;

    const button = document.querySelector('.button');
    button.removeEventListener('click', showText);
    button.addEventListener('click', hideText);
}

    <body>
        <p class="text">Some text</p>
        <button class="button">Hide text</button>
        <script src="./script.js"></script>
    </body>

//============================ JSON, LOCALSTORAGE ================
    document.cookie = 'user=Dima;max-age=10';       // Записываем в браузер cookie, которые удалятся через 10 сек
    console.log(document.cookie);

    localStorage.setItem('username', 'Dima2');      // Записываем и получаем данные из локального хранилища браузера
    const name = localStorage.getItem('username');

    localStorage.removeItem('username');            // Удалить элемент из хранилища
    localStorage.clear();                           // Полностью очистить хранилище

// sessionStorage работает как и localStorage, но хранит данные, пока открыт браузер

// Если помещаем что-то в локаольное хранилище, то оно преобразуется в строку
// чтобы преобразовать объект в строку, используется JSON
//{
//    "name": "Dima Kozyr",
//    "age": 29
//}

const obj = {
        name: 'Dima Kozyr',
    number: 25
}

    let str1 = JSON.stringify(obj);                 // Преобразование объекта в JSON
    let str2 = JSON.parse(str1);                    // Преобразование JSON в объект

    //----------------------------------------------------------------
    const var1 = 'Dima';
    const obj2 = {
        name: var1
}

    const jsonObj = JSON.stringify(obj2);           // Преобразовали объект в строку формата JSON
    localStorage.setItem('username2', jsonObj);     // Записали строку в локальное хранилище

    const var2 = localStorage.getItem('username2');
    const var3 = JSON.parse(var2);
    console.log(var2);
    console.log(var3.name);                         // Выполнили обратное преобразование и обращаемся к полю 'name'

    //============================ КЛАССЫ ============================
    class Rectangle {
        constructor(height, width = 4){
        this.height = height;
    this.width = width;
    }
    calcArea() {
        return this.width * this.height;
    }
}

    const square = new Rectangle(3, 5);
    console.log(square.calcArea());

    //============================ ОПЕРАТОР SPREAD ===================
    let video = ['v1', 'v2'],
    internet = [...video, 'fb'];    // Оператор ... отобразит содержимое массива
    console.log(internet);

    function f2(a, b, c) {
        console.log(a);
    console.log(b);
    console.log(c);
}
    let numbers = [1, 2, 3];
    f2(...numbers);                     // Ф-я принимает три атрумента и оператор ... позволяет передать массив с тремя значениями,
    // а не каждое значение по отдельности

    //============================ PROMISE ===========================
    let drink = 0;
    function shoot(arrow) {
        console.log('Вы сделали выстрел...');

    // Создание обещания
    let promise = new Promise(function(resolve, reject){    // resolve - обезание выполнилось, reject - не выполнилось
        setTimeout(function () {
            Math.random() > 0.5 ? resolve({}) : reject("Вы промахнулись");
        }, 1000);
    });
    return promise;
}

    function win() {
        console.log('Вы победили');
    (drink == 1) ? buyBeer() : giveMoney();
}

    function loose() {console.log('Вы проиграли'); }
    function buyBeer() {console.log('Вам купили пиво'); }
    function giveMoney() {console.log('Вам дали деньги'); }

    shoot({ })
    .then(mark => console.log('Вы попали в цель'))  // Если от promise получили resolve, то выполнится then, потом слеюущий then и т.д.
    .then(win)
    .catch(loose);                                  // Если получили reject - выполнится catch
                                                    // Если после catch будет еще один then, то он выполнится в любом случае