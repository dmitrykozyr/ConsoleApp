namespace Domain.Models.RequestModels;

public class LoadFileByPathRequest
{
    public string? BucketPath { get; init; }

    public string? DeathTime { get; init; }

    public int? LifeTimeHours { get; init; }

    public string? FilePathInFileSystem { get; init; }
}
