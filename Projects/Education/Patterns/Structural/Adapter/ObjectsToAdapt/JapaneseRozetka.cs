using Education.Patterns.Structural.Adapter.Interfaces;

namespace Education.Patterns.Structural.Adapter.ObjectsToAdapt;

public class JapaneseRozetka : IJapaneseRozetka
{
    public string GetJapanesePower()
    {
        return "Японская розетка (110V)";
    }
}
