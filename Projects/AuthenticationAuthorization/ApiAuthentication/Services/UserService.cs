using ApiAuthentication.Models;
using ApiAuthentication.Repositories;
using ApiAuthentication.Services.Interfaces;

namespace ApiAuthentication.Services;

public class UserService : IUserService
{
    public User Get(UserLogin userLogin)
    {
        User user = UserRepository.Users.FirstOrDefault(z => 
                        z.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase) &&
                        z.Password.Equals(userLogin.Password));

        return user;
    }
}
