using JWTToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }

    // Генерация JWT-токена
    // https://localhost:7269/api/login
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);

        if (user is not null)
        {
            var token = GenerateToken(user);
            return Ok(token);
        }

        return NotFound("User not found");
    }

    private string GenerateToken(UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.GivenName, user.GivenName),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(15),
                            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel Authenticate(UserLogin userLogin)
    {
        var currentUser = UserConstants.Users.FirstOrDefault(z => 
                            z.UserName.ToLower() == userLogin.UserName.ToLower() &&
                            z.Password == userLogin.Password);

        return currentUser;
    }
}
