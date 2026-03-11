using Education.Patterns.Creational.AbstractFactory.Interfaces;

namespace Education.Patterns.Creational.AbstractFactory.ProductsToCreate;

public class WaterEquipment : ISportEquipment
{
    public void Clothes()
    {
        Console.WriteLine("Водолазный костюм");
    }

    public void Equipment()
    {
        Console.WriteLine("Акваланг");
    }
}
