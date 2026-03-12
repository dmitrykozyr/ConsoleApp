using Education.Patterns.Structural.Adapter.Interfaces;

namespace Education.Patterns.Structural.Adapter;

public class Adapter : IEuropeanRozetka
{
    private readonly IJapaneseRozetka _japaneseRozetka;

    public Adapter(IJapaneseRozetka japaneseRozetka)
    {
        _japaneseRozetka = japaneseRozetka;
    }

    public string GetElectricity()
    {
        return $"{_japaneseRozetka.GetJapanesePower()} через переходник";
    }
}
