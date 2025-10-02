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

    public HomeController(ILoginService loginService, IFilesService filesService)
    {
        _loginService = loginService;
        _filesService = filesService;
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
            var result = new FileDownloadRequest
            {
                Guid = default
            };

            return View(result);
        }

        FileStreamResponse fileStreamResponse = _filesService.GetFileStream(model);

        if (fileStreamResponse is not null && fileStreamResponse.Stream is not null)
        {
            //!return File(fileStreamResponse.Stream, "application/octet-stream", fileStreamResponse.FileNameExtension);
            return default;
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
