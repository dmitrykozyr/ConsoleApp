namespace Education.Patterns.Structural.Flyweight;

public class TreeType
{
    private readonly string _name;
    private readonly string _color;

    public TreeType(string name, string color)
    {
        _name = name;
        _color = color;

        Console.WriteLine($"Создан тип дерева: {name} {color}");
    }

    // Внешнее состояние (X, Y) передается при отрисовке
    public void Display(int x, int y)
    {
        Console.WriteLine($"Отрисовка {_name} {_color} в точке ({x}, {y})");
    }
}
