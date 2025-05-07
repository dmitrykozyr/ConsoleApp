using JWTToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTToken.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    //! Метод, не требующий аутентификации и авторизации
    // https://localhost:7269/api/user/public
    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Public method");
    }

    // Метод, требующий аутентификацию
    // https://localhost:7269/api/user/admins
    // В Postman -> Auth -> Bearer Token вставляем токен,
    // сгенерированный эндпоинтом Login
    [HttpGet("Admins")]
    // В UserConstants.cs мы задали роли, этот эндпоинт может
    // вызвать только пользователь с указанной ролью, т.е.
    // нужно сгенерировать токен с его лоином и паролем
    [Authorize(Roles = "Administrator")]
    public IActionResult Admins()
    {
        var currentUser = GetCurrentUser();

        if (currentUser is null)
        {
            return NotFound();
        }

        return Ok($"Hi {currentUser.GivenName}, your role is {currentUser.Role}");
    }

    private UserModel GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is not null)
        {
            var userClaims = identity.Claims;

            return new UserModel
            {
                UserName = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.NameIdentifier)?.Value,
                EmailAddress = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.Email)?.Value,
                GivenName = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.GivenName)?.Value,
                Surname = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.Surname)?.Value,
                Role = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.Role)?.Value
            };
        }

        return null;
    }
}
