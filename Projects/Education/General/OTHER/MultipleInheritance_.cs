namespace Education.General;

public class MultipleInheritance_
{
    // Множественное наследование запрещено, но его можно реализовать через интерфейсы
    class BaseClass
    {
        public void BaseClassMethod() => Console.WriteLine("BaseClassMethod");
    }

    interface IPrinter { void Print(); }

    interface IScanner { void Scan(); }

    class Printer : IPrinter
    {
        public void Print() => Console.WriteLine("Print");
    }

    class Scanner : IScanner
    {
        public void Scan() => Console.WriteLine("Scan");
    }

    class MultifunctionalDevice : BaseClass, IPrinter, IScanner
    {
        private readonly IPrinter _printer = new Printer();
        private readonly IScanner _scanner = new Scanner();

        public void Print() => _printer.Print();
        public void Scan() => _scanner.Scan();
    }

    static void Main_()
    {
        var multifunctionalDevice = new MultifunctionalDevice();

        multifunctionalDevice.BaseClassMethod();
        multifunctionalDevice.Print();
        multifunctionalDevice.Scan();
    }
}
