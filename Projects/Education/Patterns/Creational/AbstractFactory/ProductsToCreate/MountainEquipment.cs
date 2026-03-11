using Education.Patterns.Creational.AbstractFactory.Interfaces;

namespace Education.Patterns.Creational.AbstractFactory.ProductsToCreate;

public class MountainEquipment : ISportEquipment
{
    public void Clothes()
    {
        Console.WriteLine("Теплый горный костюм");
    }

    public void Equipment()
    {
        Console.WriteLine("Ледорубы");
    }
}
