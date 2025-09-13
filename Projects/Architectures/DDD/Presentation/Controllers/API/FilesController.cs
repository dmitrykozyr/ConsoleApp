using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.API;

[ApiController]
//[ApiVersion("1.0")]
//[Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
public class FilesController
{
    private readonly IFileService _fileService;
    private readonly ILogging _logging;
    private readonly ILoginService _loginService;

    public FilesController(IFileService fileService, ILogging logging, ILoginService loginService)
    {
        _fileService = fileService;
        _logging = logging;
        _loginService = loginService;
    }

    [HttpPost("GetFile")]
    [ProducesResponseType(typeof(FileStorageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IResult GetFile([FromBody] FileStorageRequest model)
    {
        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            //var validationResult = Validator<FileStorageRequest, FileStorageRequestValidator>.Validate(model);
            //if (validationResult is not null)
            //{
            //    return Results.BadRequest(validationResult);
            //}

            var result = _fileService.GetFile(model);
            if (result is not null)
            {
                _logging.LogToDB(RestMethods.GET, "Файл получен", model.FileGuid);

                return Results.Ok(result);
            }

            const string ERROR_MESSAGE = "Не удалось получить файл";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}
