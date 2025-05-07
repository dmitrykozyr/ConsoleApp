using Microservices.Orders.Models;

namespace Microservices.Orders.Interfaces;

public interface IOrdersProvider
{
    Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
}
