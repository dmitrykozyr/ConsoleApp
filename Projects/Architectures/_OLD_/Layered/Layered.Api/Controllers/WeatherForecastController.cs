using Layered.Domain.Interfaces;
using Layered.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Layered.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(
        IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _service.ProcessTemperature();
    }
}