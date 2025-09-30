namespace Domain.Models.ResponseModels;

public class FileStreamResponse
{
    public Stream? Stream { get; init; }

    public string? FileNameExtension { get; init; }
}
