using Education.Patterns.Behavioral.State.Interfaces;
using Education.Patterns.Behavioral.Strategy;

namespace Education.Patterns.Behavioral.State.CustomStates;

public class EnergeticState : IState
{
    public void GoToShop(Context context)
    {
        Console.WriteLine("Уже бегу за продуктами");
    }

    public void SeeBeer(Context context)
    {
        Console.WriteLine("Уже бегу, и возьму побольше");
    }
}
