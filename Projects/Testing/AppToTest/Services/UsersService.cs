using AppToTest.Interfaces;
using AppToTest.Models;

namespace AppToTest.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public int AddUser(User user)
    {
        var result = _usersRepository.AddUser(user);

        return result;
    }

    public int UpdatePhone(User user)
    {
        var result = _usersRepository.UpdatePhone(user);

        return result;
    }

    public int VerifyEmail(int userId)
    {
        var result = _usersRepository.VerifyEmail(userId);

        return result;
    }
}
