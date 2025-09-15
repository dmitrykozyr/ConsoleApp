using Domain.Interfaces;
using Domain.Models.RequentModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Web;

public class HomeController : Controller
{
    private readonly ILoginService _loginService;

    public HomeController(ILoginService loginService)
    {
        _loginService = loginService;
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
            var result = new ViewResult(); //!

            return View(result);
        }

        return View("Thanks");
    }
}
