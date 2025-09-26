namespace Domain.Models.RequentModels;

public class LoadFileByBytesRequest
{
    public string? BucketPath { get; set; }

    public string? DeathTime { get; set; }

    public int? LifeTimeHours { get; set; }

    // Массив байт в строке, представляющий файл
    public string? File { get; set; }

    public string? FileName { get; set; }
}
