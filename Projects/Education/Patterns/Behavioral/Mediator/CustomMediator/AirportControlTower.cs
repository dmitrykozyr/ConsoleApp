using Education.Patterns.Behavioral.Mediator.Abstractions;
using Education.Patterns.Behavioral.Mediator.Interfaces;

namespace Education.Patterns.Behavioral.Mediator.CustomMediator;

public class AirportControlTower : IControlTower
{
    private List<Airplane> _planes = new List<Airplane>();

    public void RegisterPlane(Airplane plane)
    {
        _planes.Add(plane);
    }

    public void RequestLanding(Airplane requestingPlane)
    {
        Console.WriteLine($"[Башня]: Получен запрос на посадку от {requestingPlane.FlightNumber}");

        foreach (Airplane plane in _planes)
        {
            if (plane != requestingPlane)
            {
                // Диспетчер говорит остальным ждать
                plane.Wait();
            }
        }

        requestingPlane.Land();
    }
}
