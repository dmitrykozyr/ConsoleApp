namespace Education.Patterns.Structural.Flyweight;

public class Tree
{
    private readonly int _x;
    private readonly int _y;

    // Ссылка на общий ресурс
    private readonly TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        _x = x;
        _y = y;

        _type = type;
    }

    public void Render()
    {
        _type.Display(_x, _y);
    }
}
