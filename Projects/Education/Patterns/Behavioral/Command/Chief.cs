namespace Education.Patterns.Behavioral.Command;

public class Chief
{
    public void CookDishes(string dish)
    {
        Console.WriteLine($"Повар готовит: {dish}");
    }
}
