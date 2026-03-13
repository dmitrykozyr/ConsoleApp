using Education.Patterns.Behavioral.Mediator.Interfaces;

namespace Education.Patterns.Behavioral.Mediator.Abstractions;

public abstract class Airplane
{
    public string FlightNumber { get; }

    protected IControlTower _tower;

    public Airplane(IControlTower controlTower, string flightNumber)
    {
        _tower = controlTower;

        FlightNumber = flightNumber;
    }

    public abstract void Land();

    public abstract void Wait();
}
