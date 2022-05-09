using Layered.Domain.Models;

namespace Layered.Domain.Interfaces
{
    public interface IWeatherForecastService
    {
        List<WeatherForecast> ProcessTemperature();
    }
}
