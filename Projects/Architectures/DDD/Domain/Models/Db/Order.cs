namespace Domain.Models.Db;

public class Order
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public DateTime OrderTime { get; set; }
}
