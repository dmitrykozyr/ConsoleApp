using Layered.Domain.Interfaces;
using Layered.Domain.Models;

namespace Layered.Service.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository;

    public WeatherForecastService(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public List<WeatherForecast> ProcessTemperature()
    {
        var forecasts = _repository.GetForecasts();

        var processed = new List<WeatherForecast>();

        foreach (var f in forecasts)
        {
            f.TemperatureF = 32 + (int)(f.TemperatureC / 0.5556);

            processed.Add(f);
        }

        return processed;
    }
}
