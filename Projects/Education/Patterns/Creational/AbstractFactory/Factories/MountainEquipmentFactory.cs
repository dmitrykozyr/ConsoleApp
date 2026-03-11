using Education.Patterns.Creational.AbstractFactory.Interfaces;
using Education.Patterns.Creational.AbstractFactory.ProductsToCreate;

namespace Education.Patterns.Creational.AbstractFactory.Factories;

public class MountainEquipmentFactory : IAbstractFactory
{
    public ISportEquipment GetEquipment()
    {
        var mountainEquipment = new MountainEquipment();

        return mountainEquipment;
    }
}
