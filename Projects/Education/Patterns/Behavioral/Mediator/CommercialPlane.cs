using Education.Patterns.Behavioral.Mediator.Abstractions;
using Education.Patterns.Behavioral.Mediator.Interfaces;

namespace Education.Patterns.Behavioral.Mediator;

public class CommercialPlane : Airplane
{
    public CommercialPlane(IControlTower controlTower, string flightNumber)
     : base(controlTower, flightNumber)
    {
    }

    public void RequestLanding()
    {
        _tower.RequestLanding(this);
    }

    public override void Land()
    {
        Console.WriteLine($"{FlightNumber}: Совершаю посадку");
    }

    public override void Wait()
    {
        Console.WriteLine($"{FlightNumber}: Понял, кружу над аэропортом");
    }
}
