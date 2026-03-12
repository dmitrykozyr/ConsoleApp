using Education.Patterns.Creational.FactoryMethod.Interfaces;

namespace Education.Patterns.Creational.FactoryMethod.CustomHouses;

public class WoodHouse : IHouse
{
    public void Construct(string ownerName)
    {
        Console.WriteLine($"Деревянный дом построен, имя владельца {ownerName}");
    }
}
