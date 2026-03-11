using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility;

public class Client
{
    public static void ClientCode(AbstractHandler handler)
    {
        var allFood = new List<string>
        {
            Food.NUT,
            Food.MEAT,
            Food.BANANA
        };

        foreach (var food in allFood)
        {
            Console.Write($"Кто хочет {food}?");

            var result = handler.HandleRequest(food);

            if (result is not null)
            {
                Console.WriteLine($" {result}");
            }
            else
            {
                Console.WriteLine($" {food} никто не съел");
            }
        }
    }
}
