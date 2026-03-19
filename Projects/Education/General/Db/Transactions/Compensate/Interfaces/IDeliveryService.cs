using Education.General.Db.Transactions.Compensate.Models;

namespace Education.General.Db.Transactions.Compensate.Interfaces;

public interface IDeliveryService
{
    Task<Order> Schedule(int orderId);
}
