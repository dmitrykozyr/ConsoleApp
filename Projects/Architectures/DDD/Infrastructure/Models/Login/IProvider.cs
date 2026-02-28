namespace Infrastructure.Models.Login;

public interface IProvider
{
    AppRoleInfo[] GetPersonAppRoles(PersonInfo person);
}
