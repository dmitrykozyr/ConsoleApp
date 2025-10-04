using Domain.Interfaces;
using Domain.Interfaces.Login;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.API;

[ApiController]
//![Route("api/v{version:apiVersion}/[controller]")]
public class FilesController
{
    private readonly ILogging _logging;
    private readonly IFilesService _filesService;
    private readonly ILoginService _loginService;


    public FilesController(ILogging logging, IFilesService filesService, ILoginService loginService)
    {
        _logging = logging;
        _filesService = filesService;
        _loginService = loginService;
    }

    [HttpGet("GetFileByPath")]
    [ProducesResponseType(typeof(LoadFileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IResult GetFileByPath([FromQuery] string bucketPath, [FromQuery] Guid guid)
    {
        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            var model = new FileStorageRequest
            {
                BucketPath = bucketPath,
                Guid = guid
            };

            var validationResult = Validator<FileStorageRequest, FileStorageRequestValidator>.Validate(model);
            if (validationResult is not null)
            {
                return Results.BadRequest(validationResult);
            }

            LoadFileResponse? result = _filesService.GetFileByPath(model);
            if (result is not null)
            {
                return Results.Ok($"Файл скачан по пути: {result.FilePath}.{result.FileName}");
            }

            const string ERROR_MESSAGE = "Не удалось получить файл";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }


    [HttpPost("LoadFileByBytesArray")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IResult> LoadFileByBytesArray([FromBody] LoadFileByBytesRequest model)
    {
        // Здесь можно преобразовать строку в массив байт для тестирования данного запроса,
        // передав его в параметр 'file'
        //string original = "Hello, World";
        //byte[] bytesArr = Encoding.UTF8.GetBytes(original);
        //string base64String = Convert.ToBase64String(bytesArr);

        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            var validationResult = Validator<LoadFileByBytesRequest, LoadFileByBytesRequestValidator>.Validate(model);
            if (validationResult is not null)
            {
                return Results.BadRequest(validationResult);
            }

            var result = await _filesService.LoadFileByBytesArray(model);
            if (result != default)
            {
                return Results.Ok(result);
            }

            const string ERROR_MESSAGE = "Не удалось загрузить файл";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost("LoadFileFromFileSystemByPath")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IResult LoadFileFromFileSystemByPath([FromBody] LoadFileByPathRequest model)
    {
        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            var validationResult = Validator<LoadFileByPathRequest, LoadFileByPathRequestValidator>.Validate(model);
            if (validationResult is not null)
            {
                return Results.BadRequest(validationResult);
            }

            var result = _filesService.LoadFileFromFileSystemByPath(model);
            if (result != default)
            {
                return Results.Ok(result);
            }

            const string ERROR_MESSAGE = "Не удалось загрузить файл";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost("LoadFileFromFileSystemBySelection")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IResult> LoadFileFromFileSystemBySelection([FromQuery] string? bucketPath, [FromQuery] string? deathTime, [FromQuery] int? lifeTimeHours, IFormFile file)
    {
        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            var model = new LoadFileBySelectionRequest()
            {
                BucketPath = bucketPath,
                DeathTime = deathTime,
                LifeTimeHours = lifeTimeHours
            };

            var validationResult = Validator<LoadFileBySelectionRequest, LoadFileBySelectionRequestValidator>.Validate(model);
            if (validationResult is not null)
            {
                return Results.BadRequest(validationResult);
            }

            var result = await _filesService.LoadFileFromFileSystemBySelection(model, file);
            if (result != default)
            {
                return Results.Ok(result);
            }

            const string ERROR_MESSAGE = "Не удалось загрузить файл";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }


    [HttpDelete("DeleteFile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IResult DeleteFile([FromBody] FileStorageRequest model)
    {
        try
        {
            var isAuthenticated = _loginService.AuthenticateDomainUser();
            if (!isAuthenticated)
            {
                return Results.Unauthorized();
            }

            var validationResult = Validator<FileStorageRequest, FileStorageRequestValidator>.Validate(model);
            if (validationResult is not null)
            {
                return Results.BadRequest(validationResult);
            }

            var result = _filesService.DeleteFile(model);
            if (result)
            {
                return Results.Ok();
            }

            const string ERROR_MESSAGE = "Не удалось удалить файл из СХФ";
            _logging.LogToFile(ERROR_MESSAGE);
            return Results.BadRequest(ERROR_MESSAGE);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
