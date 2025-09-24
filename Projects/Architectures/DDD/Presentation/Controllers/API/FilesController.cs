using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Login;
using Domain.Interfaces.Services;
using Domain.Models.Options;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace Presentation.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FilesController
{
    private readonly ILogging _logging;
    private readonly IFileService _fileService;
    private readonly ILoginService _loginService;
    private readonly IFeatureManager _featureManager;

    public FilesController(ILogging logging, IFileService fileService, ILoginService loginService, IFeatureManager featureManager)
    {
        _logging = logging;
        _fileService = fileService;
        _loginService = loginService;
        _featureManager = featureManager;
    }

    [HttpPost("GetFile")]
    [ProducesResponseType(typeof(FileStorageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IResult> GetFile([FromBody] FileStorageRequest model)
    {
        try
        {
            bool isFeatureEnabled = await _featureManager.IsEnabledAsync(FeatureFlags.FeatureFlag1);
            if (!isFeatureEnabled)
            {
                return Results.BadRequest($"Фича {nameof(FilesController)} недоступна");
            }

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
