namespace Domain.Models.Dto;

public class LoadFileDto
{
    public string? BucketPath { get; set; }

    public string? DeathTime { get; set; }

    public int? LifeTimeHours { get; set; }

    public byte[]? File { get; set; }
}
