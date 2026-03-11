using Education.Patterns.Creational.AbstractFactory.Interfaces;

namespace Education.Patterns.Creational.AbstractFactory;

public class Application
{
    private readonly ISportEquipment _sportEquipment;

    public Application(IAbstractFactory abstractFactory)
    {
        _sportEquipment = abstractFactory.GetEquipment();
    }

    public void GetClothes()
    {
        _sportEquipment.Clothes();
    }

    public void GetEquipment()
    {
        _sportEquipment.Equipment();
    }

    public void GetAllEquipment()
    {
        _sportEquipment.Clothes();
        _sportEquipment.Equipment();
    }
}
