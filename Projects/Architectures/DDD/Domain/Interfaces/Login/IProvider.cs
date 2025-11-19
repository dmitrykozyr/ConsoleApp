using Domain.Models.Login;

namespace Domain.Interfaces.Login;

public interface IProvider
{
    AppRoleInfo[] GetPersonAppRoles(PersonInfo person);
}
