namespace Domain.Models.Dto;

public class FileStorageDto
{
    public Guid Guid { get; set; }

    public string? BucketPath { get; set; }

    public Token? Token { get; set; }
}
