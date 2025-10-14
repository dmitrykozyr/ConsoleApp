namespace Domain.Models.Db;

public class Order
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime OrderTime { get; set; }
}
