using Domain.Interfaces;
using Domain.Models.RequentModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.API;

public class TestController
{
    private readonly IRedisService _redisService;

    public TestController(IRedisService redisService)
    {
        _redisService = redisService;
    }

    [HttpGet("Test")]
    public async Task<IResult> Test()
    {
        try
        {
            var resul = await _redisService.GetCache("my message 1");

            return Results.Ok(resul);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}
