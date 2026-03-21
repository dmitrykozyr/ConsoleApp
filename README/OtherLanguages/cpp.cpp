Основы
Классы
Массивы
Наследование
Указатели
Виртуальные функции
Шаблонные функции

// ============================ ОСНОВЫ ============================
// Преобразование типов
float x = 5.6f;
(int)x;

// Циклы
for (int i = 0, j = 100; i < 50; i++, j--) {}

int n = 1;
while (n != 0) {}

do {} while (n != 0);

min = (i < j) ? i : j;				// Если правда, min = i, иначе min = j
enum MyEnum { One = 3, Two, Three };
inline void MyFunc() {} 			// Встраиваемая ф-я должна содержать 1-2 оператора
void MyFunc(int& x, float b = 0.5f) { x = 5; };	// Аргументы по умолчанию указываются в конце, & - передача адреса переменной

// Структура
struct MyStruct { int a; float c; };
MyStruct ms;
ms.a = 5;
ms = { 1, 2 };

// Рекурсия для вычисления факториала
unsigned long factFunc(unsigned long n)
{
	if (n > 1) return n * factFunc(n - 1);
	else return 1;
}

// Возвращение значения по ссылке позволяет не копировать большие объемы данных и
// дает возможность вызывать ф-ю в качестве левого операнда операции присваивания
int x;
int& SetX() { return x; }

int main()
{
	SetX() = 5;
	std::cout << x << std::endl;
	return 0;
}

// Константные аргументы нельзя изменить
void MyFunc(int& a, const int& b)
{
	a = 5;	// Правильно
	b = 7; 	// Ошибка
}

// ============================ КЛАССЫ ============================
class A
{
public:
	A() : _x(0), _y(0) {}
	A(int x, int y) : _x(x), _y(y) {}

	int _x;
	int _y;

	// Константный метод не позволяет менять поля своего класса
	// Модификатор const указывают при объявлении и определении функции
	void MyFunc() const
	{
		yCo = 0; // Ошибка
	}
};

int main()
{
	A objA(5, 7);
	A objB;
	A objC = objA; 	// Конструктор копирования
	const A objD;	// Константный объект может вызывать лишь константные методы
	return 0;
}

// ============================ МАССИВЫ ============================
// Передача массива в ф-ю
const int i = 2, j = 2;
void MyFunc(int arr[i][j])
{
	for (int iarr = 0; iarr < i; iarr++)
		for (int jarr = 0; jarr < j; jarr++)
			cout << arr[iarr][jarr];
}

// Массив структур
struct MyStruct { int a; int b; };

int main()
{
	const int size = 5;
	int arr1[size];
	for (int i = 0; i < size; i++) {}

	int arr2[] = { 1, 2, 3, 4, 5 };
	int arr3[5] = { 1, 2, 3, 4 };	// 5й элемент будет установлен в 0

	int arr4[2][2] = { { 1, 2 },{ 3, 4 } };	// Передача массива в ф-ю
	MyFunc(arr4);

	MyStruct structArr[5];		// Массив структур
	structArr[0].a = 5;

	const int MAX = 80;		// Строковые переменные
	char str1[MAX];
	cin >> str1;
	cout << str1;
	char str2[] = "ABCD";
	return 0;
}

// ============================ НАСЛЕДОВАНИЕ ============================
// Если один и тот-же метод существует в базовом и производном классе, то будет вызван метод производного класса
// Для абстрактного класса нельзя создать экземпляры, от него можно только наследоваться
class A
{
private:    int a;
protected:  int b;
public:     int c;
};

class B : public A
{
public:
	void MyFunc()
	{
		a = 0; // Ошибка
		b = 0;
		c = 0;
	}
};

class C : private A
{
	void MyFunc()
	{
		a = 0; // Ошибка
		b = 0;
		c = 0;
	}
};

int main()
{
	B objB;
	objB.a = 0; // Ошибка
	objB.b = 0; // Ошибка
	objB.c = 0;

	C objC;
	objC.a = 0; // Ошибка
	objC.b = 0; // Ошибка
	objC.c = 0; // Ошибка
	return 0;
}

// Множественное наследование
class A {};
class B {};
class C : public A, public B {};

// ============================ УКАЗАТЕЛИ ============================
// Ссылка & возвращает адрес, указатель * возвращает значение
// Нельзя использовать неинициализированные указатели, присваивать им хотя бы null при объявлении
// Можно сравнивать лишь те указатели, что ссылаются на элементы одного массива
float* p = 0;
if (!p) {}		// Проверка на нулевой указатель 

int a = 57;
cout << &a << endl; 	// Адрес

int* b;             	// Указатель на int
				// 'int* b' идентично 'int *b' 
				// int *a, *b, *c;

b = &a;             	// Присвоим 'b' значение адреса 'a'
cout << b << endl;  	// Адрес
cout << *b << endl; 	// Значение '57' - разыменование

int a, b;
int* c;
c = &a;
*c = 5; 		// То же самое, что a = 5
b = *c; 		// То же самое, что b = c
cout << a << b << *c << endl; // 555

// Присваивание с указателями
int* p, num;
p = &num;
*p = 100;
cout << num << endl; 	// 100
(*p)++;
cout << num << endl; 	// 101

// Адрес, который помещается в указатель, должен быть одного с ним типа
int d;
float e = &d; 		// Ошибка

// Доступ к элементу массива через указатель
int arr[5] = { 1, 2, 3, 4, 5 };
for (int i = 0; i < 5; i++)
{
	*arr = i; 		// Можно
	arr++; 			// Ошибка, указатель на первый элемент модифицировать нельзя
	cout << *(arr + i); 	// Выражение *(arr + i) эквивалентно arr[i]
}

// Передача аргумента по ссылке
void MyFunc(int& b) { b = 7; }

int main()
{
	int a = 5;
	MyFunc(a);
	cout << a; // 7
	return 0;
}

// Передача аргумента по указателю
void MyFunc(int* b) { *b = 7; }

int main()
{
	int a = 5;
	MyFunc(&a);
	cout << a; // 7
	return 0;
}

// Передача массива по указателю
void MyFunc(int* arr)
{
	for (int i = 0; i < 5; i++)
		*arr++ *= 2; // Инкремент индекса массива и изменение значения
}

int main()
{
	int arr[5] = { 1, 2, 3, 4, 5 };
	MyFunc(arr);
	for (int i = 0; i < 5; i++)
		cout << arr[i] << endl;
	return 0;
}

// new delete
// new вызывают в конструкторе, а delete - в деструкторе
int* a = new int;
delete a;

char* b = new char[5];
delete[] b;

// Операция '->' работает с указателями на объекты так же, как операция '.' работает с объектами
class A
{
public:
	void MyFunc() {}
};

int main()
{
	A* objA = new A;
	objA->MyFunc();		// Ссылка на метод класса из объекта, на который указывает objA
	(*objA).MyFunc(); 	// То же самое
	return 0;
}

int** a; 	// Указатель на указатель указывает на указатель, указывающий на int

int x, * p, ** q;
x = 10;
p = &x;
q = &p;
cout << **q; // 10

// Ошибки при работе с указателями
int x, * p; 	// Ошибка - неинициализированный указатель
x = 10;
*p = x; 	// На что указывает p?

// ============================ ВИРТУАЛЬНЫЕ ФУНКЦИИ ============================
class A
{
public:
	void MyFunc() { cout << "A" << endl; }
};

class B : public A
{
public:
	void MyFunc() { cout << "B" << endl; }
};

class C : public A
{
public:
	void MyFunc() { cout << "C" << endl; }
};

int main()
{
	// Компилятор не смотрит на содержимое указателя ptr, а выбирает метод, удовлетворяющий типу указателя
	A* ptr;
	B objB;
	C objC;

	ptr = &objB;
	ptr->MyFunc(); 	// Выведет 'A'

	ptr = &objC;
	ptr->MyFunc(); 	// Выведет 'A'

	return 0;
}

class A
{
public:
	virtual void MyFunc() { cout << "A" << endl; }
};

class B : public A
{
public:
	void MyFunc() { cout << "B" << endl; }
};

class C : public A
{
public:
	void MyFunc() { cout << "C" << endl; }
};

int main()
{
	A* ptr;
	B objB;
	C objC;

	ptr = &objB;
	ptr->MyFunc(); 	// Выведет 'B'

	ptr = &objC;
	ptr->MyFunc(); 	// Выведет 'C'

	return 0;
}

class A
{
public:
	// Если класс содержит чистую виртуальную функцию, то он абстрактный, его экземпляр создать нельзя
	virtual void MyFunc() = 0;
};

int main()
{
	A objA; // ошибка
	return 0;
}

// ============================ ШАБЛОННЫЕ ФУНКЦИИ ============================
// Позволяют использовать одну запись для работы с разными типами данных
// Шаблонными могут быть тип возвращаемого значения и аргументы,
// Количество экземпляров шаблонной ф-и в памяти резко растет с увеличением числа аргументов
// Макросы редко используют, т.к. они не осуществляют проверку типов
// Шаблонные типы должны быть определены, а нешаблонные могут использовать аргументы по умолчанию
template<class T>
void MyFunc(T var) {}

int main()
{
	int a = 5;
	float b = 5.7;
	MyFunc(a);
	MyFunc(b);

	return 0;
}

template<class T, class Z>
void MyFunc(T var1, Z var2, int var3 = 2) {}

int main()
{
	int a = 5;
	float b = 5.7;
	MyFunc(a, 2);
	MyFunc(b, a);
	MyFunc(1, 2, 3);
	return 0;
}