namespace Domain.Models.ResponseModels;

public class LoadFileResponse
{
    public string? FileName { get; init; }

    public string? FilePath { get; init; }

    public string? ErrorMessage { get; set; }
}
