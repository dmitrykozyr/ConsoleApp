namespace Domain.Models.Db;

public class Order
{
    public long Id { get; init; }

    public string? Name { get; init; }

    public DateTime OrderTime { get; init; }
}
