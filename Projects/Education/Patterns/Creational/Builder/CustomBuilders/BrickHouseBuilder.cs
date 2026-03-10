using Education.Patterns.Creational.Builder.Interfaces;
using Education.Patterns.Creational.Builder.ObjectsToBuild;

namespace Education.Patterns.Creational.Builder.CustomBuilders;

public class BrickHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public IHouseBuilder BuildFoundation()
    {
        return this;
    }

    public IHouseBuilder BuildWalls()
    {
        _house.Add("Кирпичные стены");
        return this;
    }

    public IHouseBuilder BuildRoof()
    {
        _house.Add("Кирпичная крыша");
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
