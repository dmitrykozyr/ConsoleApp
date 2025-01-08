namespace Microservices.Search.Interfaces;

public interface ICustomersService
{
    Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);
}
