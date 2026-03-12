using Education.Patterns.Creational.FactoryMethod.CustomHouses;
using Education.Patterns.Creational.FactoryMethod.Interfaces;

public class WoodDeveloper : IDeveloper
{
    public IHouse FactoryMethod()
    {
        return new WoodHouse();
    }
}
