namespace Education.General.Db.Transactions.Compensate.Models;

public class Order
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public long UserId { get; set; }

    public long ProductId { get; set; }

    public int Amount { get; set; }

    public bool Success { get; set; }
}
