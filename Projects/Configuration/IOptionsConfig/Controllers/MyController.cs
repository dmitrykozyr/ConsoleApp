using IOptionsConfig.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IOptionsConfig.Controllers;

[Controller] //! Протестить получение опций
public class MyController
{
    private readonly IMyService _myService;

    public MyController(IMyService myService)
    {
        _myService = myService;
    }

    [HttpGet]
    public IResult GetData()
    {
        _myService.GetData();

        return Results.Ok();
    }
}
