using Education.Patterns.Behavioral.Command.Interfaces;

namespace Education.Patterns.Behavioral.Command;

public class Waiter
{
    private List<ICommand> _orders = new List<ICommand>();

    public void AddOrderToList(ICommand order)
    {
        _orders.Add(order);
    }

    public void SendOrdersToKitchen()
    {
        foreach (ICommand order in _orders)
        {
            order.StartCooking();
        }

        _orders.Clear();
    }
}
