namespace Domain.Models.Options;

public class DatabaseOptions
{
    public string? DepositaryConnStr { get; init; }

    public string? SqlCommandTimeout { get; init; }
}
