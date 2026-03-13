namespace Education.Patterns.Structural.Flyweight.Factories;

public class TreeFactory
{
    private static readonly Dictionary<string, TreeType> _treeTypes = new();

    public static TreeType? GetTreeType(string name, string color)
    {
        bool canGetType = _treeTypes.TryGetValue(name, out TreeType? type);

        if (!canGetType)
        {
            type = new TreeType(name, color);

            _treeTypes[name] = type;
        }

        return type;
    }
}
