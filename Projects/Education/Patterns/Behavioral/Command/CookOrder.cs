using Education.Patterns.Behavioral.Command.Interfaces;

namespace Education.Patterns.Behavioral.Command;

public class CookOrder : ICommand
{
    private Chief _chief;
    private string _dish;

    public CookOrder(Chief chief, string dish)
    {
        _chief = chief;
        _dish = dish;
    }

    public void StartCooking()
    {
        _chief.CookDishes(_dish);
    }
}
