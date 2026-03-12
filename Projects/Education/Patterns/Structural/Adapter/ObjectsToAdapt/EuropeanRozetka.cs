using Education.Patterns.Structural.Adapter.Interfaces;

namespace Education.Patterns.Structural.Adapter.ObjectsToAdapt;

public class EuropeanRozetka : IEuropeanRozetka
{
    public string GetElectricity()
    {
        return "Европейская розетка (220V)";
    }
}
