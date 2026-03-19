namespace Education.General.Db.Transactions.OutboxPattern;

public class OutboxMessage
{
    public Guid Id { get; init; }

    public string? Message { get; init; }

    public bool IsSent { get; set; }
}
