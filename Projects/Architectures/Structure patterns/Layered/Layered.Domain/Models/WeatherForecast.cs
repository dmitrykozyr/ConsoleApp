namespace Layered.Domain.Models;

public class WeatherForecast
{
    public DateTime Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF { get; set; }

    public string? Summary { get; init; }
}
