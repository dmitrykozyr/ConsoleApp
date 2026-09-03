using Education.General.Db.Transactions.Compensate.Models;

namespace Education.General.Db.Transactions.Compensate.Interfaces;

public interface IPaymentService
{
    Task<Order> Charge(long userId, int amount);

    Task Refund(long userId, int amount);
}
