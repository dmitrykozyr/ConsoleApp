using AppToTest.Models;

namespace AppToTest.Interfaces;

public interface IUsersRepository
{
    int AddUser(User user);

    int UpdatePhone(User user);

    int VerifyEmail(int userId);
}
