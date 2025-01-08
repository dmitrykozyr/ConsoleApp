using ApiAuthentication.Models;

namespace ApiAuthentication.Services.Interfaces;

public interface IUserService
{
    public User Get(UserLogin userLogin);
}
