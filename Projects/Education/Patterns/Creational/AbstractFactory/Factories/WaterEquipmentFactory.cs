using Education.Patterns.Creational.AbstractFactory.Interfaces;
using Education.Patterns.Creational.AbstractFactory.ProductsToCreate;

namespace Education.Patterns.Creational.AbstractFactory.Factories;

public class WaterEquipmentFactory : IAbstractFactory
{
    public ISportEquipment GetEquipment()
    {
        var waterEquipment = new WaterEquipment();

        return waterEquipment;
    }
}
