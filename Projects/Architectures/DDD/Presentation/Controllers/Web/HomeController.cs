using Domain.Interfaces.Login;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Web;

public class HomeController : Controller
{
    private readonly ILoginService _loginService;
    private readonly IFilesService _filesService;
    private readonly IBucketService _bucketService;

    public HomeController(ILoginService loginService, IFilesService filesService, IBucketService bucketService)
    {
        _loginService = loginService;
        _filesService = filesService;
        _bucketService = bucketService;
    }

    public IActionResult Index()
    {
        var isAuthenticated = _loginService.AuthenticateDomainUser();
        if (!isAuthenticated)
        {
            return View("NotAuthorized");
        }

        return View();
    }

    // Вызывается при запуске
    [HttpGet]
    public ViewResult FileDownload()
    {
        var isAuthenticated = _loginService.AuthenticateDomainUser();
        if (!isAuthenticated)
        {
            return View("NotAuthorized");
        }

        var result = new ViewResult(); //!

        return View(result);
    }

    // Вызывается при отправке формы
    [HttpPost]
    public ViewResult FileDownload(FileStorageRequest model)
    {
        var isAuthenticated = _loginService.AuthenticateDomainUser();
        if (!isAuthenticated)
        {
            return View("NotAuthorized");
        }

        if (!ModelState.IsValid)
        {
            DbDataResponseModel buckets = _bucketService.GetBuckets();
            if (!string.IsNullOrEmpty(buckets.ErrorMessage))
            {
                var errorViewModel = new ErrorViewModel
                {
                    ErrorMessage = buckets.ErrorMessage
                };

                return View("Error", errorViewModel);
            }

            var result = new FileDownloadRequest
            {
                Guid        = default,
                BucketPath  = buckets.DbData
            };

            return View(result);
        }

        FileStreamResponse fileStreamResponse = _filesService.GetFileStream(model);

        if (fileStreamResponse is not null && fileStreamResponse.Stream is not null)
        {
            return File(fileStreamResponse.Stream, "application/octet-stream", fileStreamResponse.FileNameExtension);
        }
        else
        {
            var errorViewModel = new ErrorViewModel
            {
                ErrorMessage = "Файл не найден"
            };

            return View("Error", errorViewModel);
        }
    }
}
