using Education.Patterns.Behavioral.Strategy;

namespace Education.Patterns.Behavioral.State.Interfaces;

public interface IState
{
    void GoToShop(Context context);

    void SeeBeer(Context context);
}
