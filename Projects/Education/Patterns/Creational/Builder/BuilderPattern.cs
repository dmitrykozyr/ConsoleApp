using Education.Patterns.Creational.Builder.CustomBuilders;
using Education.Patterns.Creational.Builder.ObjectsToBuild;

namespace Education.Patterns.Creational.Builder;

// Паттерн Строитель (Builder) позволяет создать сложный объект в несколько последовательных шагов
// Мы определяем шаги и их последовательность
public class BuilderPattern
{
    public void Start()
    {
        var woodHouseBuilder = new WoodHouseBuilder();
        var brickHouseBuilder = new BrickHouseBuilder();

        // можно при необходимости пропустить ненужные шаги
        House woodHouse = woodHouseBuilder
            .BuildFoundation()
            .BuildWalls()
            .BuildRoof()
            .Build();

        House brickHouse = brickHouseBuilder
            .BuildWalls()
            .BuildRoof()
            .Build();

        woodHouse.Show();
        brickHouse.Show();
    }
}

