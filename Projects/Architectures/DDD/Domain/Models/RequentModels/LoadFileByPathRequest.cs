namespace Domain.Models.RequentModels;

public class LoadFileByPathRequest
{
    public string? BucketPath { get; set; }

    public string? DeathTime { get; set; }

    public int? LifeTimeHours { get; set; }

    public string? FilePathInFileSystem { get; set; }
}
