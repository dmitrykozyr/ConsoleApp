namespace Education.General;

public class Generics_
{
    class A<T>
    {
        public T x;

        public T param { get; init; }

        public A()
        {
        }

        public A(T _x)
        {
            x = _x;
        }
    }

    class B<T, Z>
    {
        public T x;

        public Z y;

        public T param1 { get; init; }

        public Z param2 { get; init; }

        public B(T _x, Z _y)
        {
            x = _x;
            y = _y;
        }
    }

    class C<T> : A<T>
    {
        T x;
        public C(T _x) : base(_x)
        {
            x = _x;
        }
    }

    // where означает, что тип T может быть только типа C, либо быть любым его наследником
    // where T : new() - у класса должен быть публичный конструктор без параметров
    class D<T, Z> where T : A<int>
                  where Z : new()
    {
        public T x;

        public Z y;

        public T param1 { get; init; }

        public Z param2 { get; init; }

        public D(T _x, Z _y)
        {
            x = _x;
            y = _y;
        }

        public T F1()
        {
            return x; // шаблонный тип можно возвращать
        }
    }

    delegate R Del<R, T>(T val);

    static int Displ(int val)
    {
        Console.WriteLine(val);

        return 0;
    }

    static void Main_()
    {
        var del = new Del<int, int>(Displ);
        del(1);

        var a1 = new A<int>(5);
        var a2 = new B<bool, string>(true, "false");
    }
}
