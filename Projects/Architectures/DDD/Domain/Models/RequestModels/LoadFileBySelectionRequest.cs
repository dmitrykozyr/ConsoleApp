namespace Domain.Models.RequestModels;

public class LoadFileBySelectionRequest
{
    public string? BucketPath { get; init; }

    public string? DeathTime { get; init; }

    public int? LifeTimeHours { get; init; }
}
