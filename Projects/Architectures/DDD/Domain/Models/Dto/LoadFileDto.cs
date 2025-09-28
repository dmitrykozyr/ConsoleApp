namespace Domain.Models.Dto;

public class LoadFileDto
{
    public string? BucketPath { get; init; }

    public string? DeathTime { get; init; }

    public int? LifeTimeHours { get; init; }

    public byte[]? File { get; init; }
}
