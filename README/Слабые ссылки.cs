

using System;

class Program
{
    static void Main()
    {
        
    }
}

class MyObject
{
    public MyObject()
        Console.WriteLine("Создан объект");

    ~MyObject()
        Console.WriteLine("Удален объект");
}
