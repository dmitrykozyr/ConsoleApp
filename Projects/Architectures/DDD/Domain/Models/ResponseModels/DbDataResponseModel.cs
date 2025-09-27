namespace Domain.Models.ResponseModels;

public class DbDataResponseModel
{
    public Dictionary<long, string>? DbData { get; set; }

    public string? ErrorMessage { get; set; }
}
