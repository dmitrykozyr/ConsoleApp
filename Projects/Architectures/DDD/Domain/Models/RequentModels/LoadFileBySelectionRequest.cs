namespace Domain.Models.RequentModels;

public class LoadFileBySelectionRequest
{
    public string? BucketPath { get; set; }

    public string? DeathTime { get; set; }

    public int? LifeTimeHours { get; set; }
}
