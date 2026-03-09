using Microsoft.AspNetCore.Mvc;
using Timer.Infrastructure.Interfaces;

namespace Timer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimelineInfoController
{
    //! Вынести в сервис
    private readonly ITimelineInfoRepository _timelineInfoRepository;

    public TimelineInfoController(ITimelineInfoRepository timelineInfoRepository)
    {
        _timelineInfoRepository = timelineInfoRepository;
    }

    [HttpGet("AddTimerEntry")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IResult> AddTimerEntry()
    {
        try
        {
            //var isAuthenticated = _loginService.AuthenticateDomainUser();
            //if (!isAuthenticated)
            //{
            //    return Results.Unauthorized();
            //}

            //var validationResult = Validator<LoadFileByBytesRequest, LoadFileByBytesRequestValidator>.Validate(model);
            //if (validationResult is not null)
            //{
            //    return Results.BadRequest(validationResult);
            //}

            await _timelineInfoRepository.AddTimerEntry();
            //if (result != default)
            //{
            //    return Results.Ok(result);
            //}

            ////const string ERROR_MESSAGE = "Не удалось загрузить файл";
            ////_logging.LogToFile(ERROR_MESSAGE);
            //return Results.BadRequest(ERROR_MESSAGE);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
