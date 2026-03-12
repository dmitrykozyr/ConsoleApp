using Education.Patterns.Creational.FactoryMethod.CustomHouses;
using Education.Patterns.Creational.FactoryMethod.Interfaces;

namespace Education.Patterns.Creational.FactoryMethod.CustomDevelopers;

public class PanelDeveloper : IDeveloper
{
    public IHouse FactoryMethod()
    {
        return new PanelHouse();
    }
}
