namespace Domain.Models.Dto;

public class FileStorageDto
{
    public Guid Guid { get; init; }

    public string? BucketPath { get; init; }

    public Token? Token { get; init; }
}
