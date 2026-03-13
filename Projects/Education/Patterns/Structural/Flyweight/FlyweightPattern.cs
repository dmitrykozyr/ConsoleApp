using Education.Patterns.Structural.Flyweight.Factories;

namespace Education.Patterns.Structural.Flyweight;

// Если в лесу в игре 10.000 деревьев, нам не нужно 10.000 тяжелых 3D-моделей
// Нужна одна модель и 10.000 наборов координат
public class FlyweightPattern
{
    public void Start()
    {
        var factory = new TreeFactory();

        // Создаем 3 объекта контекста, но в памяти только 1 тяжелый TreeType
        var forest = new List<Tree>
        {
            new Tree(10, 20, TreeFactory.GetTreeType("Дуб", "Зеленый")),
            new Tree(55, 12, TreeFactory.GetTreeType("Дуб", "Зеленый")),
            new Tree(33, 88, TreeFactory.GetTreeType("Дуб", "Зеленый"))
        };

        foreach (Tree tree in forest)
        {
            tree.Render();
        }
    }
}
