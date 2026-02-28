using Infrastructure.Models.Login;

namespace Infrastructure.Interfaces.Login;

public interface IProvider
{
    AppRoleInfo[] GetPersonAppRoles(PersonInfo person);
}
