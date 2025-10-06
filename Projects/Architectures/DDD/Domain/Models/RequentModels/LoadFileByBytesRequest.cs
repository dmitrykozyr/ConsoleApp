namespace Domain.Models.RequestModels;

public class LoadFileByBytesRequest
{
    public string? BucketPath { get; init; }

    public string? DeathTime { get; init; }

    public int? LifeTimeHours { get; init; }

    // Массив байт в строке, представляющий файл
    public string? File { get; init; }

    public string? FileName { get; init; }
}
