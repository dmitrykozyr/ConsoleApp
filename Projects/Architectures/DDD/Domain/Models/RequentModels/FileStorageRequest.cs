namespace Domain.Models.RequentModels;

public class FileStorageRequest
{
    public Guid Guid { get; init; }

    public string? BucketPath { get; init; }
}
