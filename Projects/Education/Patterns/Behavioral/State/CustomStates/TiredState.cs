using Education.Patterns.Behavioral.State.Interfaces;

namespace Education.Patterns.Behavioral.State.CustomStates;

public class TiredState : IState
{
    public void GoToShop(Context context)
    {
        Console.WriteLine("Не пойду, я устал");
    }

    public void SeeBeer(Context context)
    {
        Console.WriteLine("О, пиво! Я сразу взбодрился");

        // Смена состояния
        context.State = new EnergeticState();
    }
}
