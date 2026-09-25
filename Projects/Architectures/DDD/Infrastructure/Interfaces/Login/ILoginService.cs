namespace Infrastructure.Interfaces.Login;

public interface ILoginService
{
    Task<bool> AuthenticateDomainUser();
}
