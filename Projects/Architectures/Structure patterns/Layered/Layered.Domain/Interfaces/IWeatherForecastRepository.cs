using Layered.Domain.Models;

namespace Layered.Domain.Interfaces
{
    public interface IWeatherForecastRepository
    {
        WeatherForecast[] GetForecasts();
    }
}
