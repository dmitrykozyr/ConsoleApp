using Education.Patterns.Structural.Adapter.Interfaces;
using Education.Patterns.Structural.Adapter.ObjectsToAdapt;

namespace Education.Patterns.Structural.Adapter;

public class AdapterPattern
{
    public void Start()
    {
        IEuropeanRozetka europeanRozetka = new EuropeanRozetka();
        PrintPower(europeanRozetka);

        // Используем адаптер для японской розетки
        IJapaneseRozetka japaneseRozetka = new JapaneseRozetka();
        IEuropeanRozetka adapter = new Adapter(japaneseRozetka);
        PrintPower(adapter);
    }

    public void PrintPower(IEuropeanRozetka europeanRozetka)
    {
        Console.WriteLine($"Питание получено через: {europeanRozetka.GetElectricity()}");
    }
}
