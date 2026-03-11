using Education.Patterns.Creational.AbstractFactory.Factories;
using Education.Patterns.Creational.AbstractFactory.Interfaces;

namespace Education.Patterns.Creational.AbstractFactory;

public class AbstractFactoryPattern
{
    public void Start()
    {
        IAbstractFactory mountainEquipmentFactory = new MountainEquipmentFactory();
        IAbstractFactory waterEquipmentFactory = new WaterEquipmentFactory();

        var mountainEquipmentApplication = new Application(mountainEquipmentFactory);
        var waterEquipmentApplication = new Application(waterEquipmentFactory);

        mountainEquipmentApplication.GetAllEquipment();
        waterEquipmentApplication.GetAllEquipment();
    }
}
