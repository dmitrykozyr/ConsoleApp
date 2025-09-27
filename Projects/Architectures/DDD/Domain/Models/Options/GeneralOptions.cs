namespace Domain.Models.Options;

public class GeneralOptions
{
    public string? LogFile { get; init; }

    public int CommandTimeout { get; init; }

    public string? PostUrl { get; set; }
}
