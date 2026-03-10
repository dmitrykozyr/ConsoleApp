namespace Education.Patterns.Creational.Builder.ObjectsToBuild;

public class House
{
    public List<string> HouseParts = new List<string>();

    public void Add(string part)
    {
        HouseParts.Add(part);
    }

    public void Show()
    {
        Console.WriteLine($"Состав дома: {string.Join(", ", HouseParts)}");
    }
}
