using Education.Patterns.Behavioral.State.Interfaces;

namespace Education.Patterns.Behavioral.State;

public class Context
{
    public IState State { get; set; }

    public Context(IState state)
    {
        State = state;
    }

    // Клиент вызывает эти методы, не зная о состояниях
    public void RequestGoToShop()
    {
        State.GoToShop(this);
    }

    public void RequestBeer()
    {
        State.SeeBeer(this);
    }
}
