using IOptionsConfig.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IOptionsConfig.Controllers;

[Controller]
public class MyController //! Везде переделать, чтобы не наследоваться от ControllerBase
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
