using Education.Patterns.Creational.FactoryMethod.Interfaces;

namespace Education.Patterns.Creational.FactoryMethod.CustomHouses;

public class PanelHouse : IHouse
{
    public void Construct(string ownerName)
    {
        Console.WriteLine($"Панельный дом построен, имя владельца {ownerName}");
    }
}
