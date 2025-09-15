using Domain.Services.Login;

namespace Domain.Interfaces.Login;

public interface IProvider
{
    AppRoleInfo[] GetPersonAppRoles(PersonInfo person);
}
