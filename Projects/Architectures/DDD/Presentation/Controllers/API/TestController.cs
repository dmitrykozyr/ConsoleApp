using Domain.Interfaces.Cache;
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
            var result1 = _redisService.GetCache("key 1");
            var result2 = _redisService.PutCache("key 1", "val 1");
            var result3 = _redisService.GetCache("key 1");

            return Results.Ok(result3);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}
