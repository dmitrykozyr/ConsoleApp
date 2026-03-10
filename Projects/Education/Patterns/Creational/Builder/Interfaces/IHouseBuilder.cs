using Education.Patterns.Creational.Builder.ObjectsToBuild;

namespace Education.Patterns.Creational.Builder.Interfaces;

public interface IHouseBuilder
{
    IHouseBuilder BuildFoundation();

    IHouseBuilder BuildWalls();

    IHouseBuilder BuildRoof();


    House Build();
}
