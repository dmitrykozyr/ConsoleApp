using Education.Patterns.Creational.Builder.Interfaces;
using Education.Patterns.Creational.Builder.ObjectsToBuild;

namespace Education.Patterns.Creational.Builder.CustomBuilders;

public class WoodHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public IHouseBuilder BuildFoundation()
    {
        _house.Add("Деревянный фундамент");
        return this;
    }

    public IHouseBuilder BuildWalls()
    {
        _house.Add("Деревянные стены");
        return this;
    }

    public IHouseBuilder BuildRoof()
    {
        _house.Add("Деревянная крыша");
        return this;
    }


    public House Build()
    {
        House result = _house;

        // Очищаем строителя для нового использования
        _house = new House();

        return result;
    }
}
