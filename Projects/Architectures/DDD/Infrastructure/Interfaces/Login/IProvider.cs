using Infrastructure.Models.Login;

namespace Infrastructure.Interfaces.Login;

public interface IProvider
{
    Task<AppRoleInfo[]> GetPersonAppRoles(PersonInfo person);
}
