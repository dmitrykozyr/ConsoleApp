namespace Domain.Models.ResponseModels;

public class DbDataResponseModel
{
    public Dictionary<long, string>? DbData { get; init; }

    public string? ErrorMessage { get; init; }
}
