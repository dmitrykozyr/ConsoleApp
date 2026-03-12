using Education.Patterns.Behavioral.Mediator.CustomMediator;

namespace Education.Patterns.Behavioral.Mediator;

// Самолеты не общаются напрямую, их координирует диспетчер (медиатор)
public class MediatorPattern
{
    public void Start()
    {
        var airportControlTower = new AirportControlTower();

        var boeing = new CommercialPlane(airportControlTower, "Boeing-747");
        var airbus = new CommercialPlane(airportControlTower, "Airbus-A320");

        airportControlTower.RegisterPlane(boeing);
        airportControlTower.RegisterPlane(airbus);

        // Боинг просит посадку, а Башня сама разрешает ситуацию с Аэробусом
        boeing.RequestLanding();
    }
}
