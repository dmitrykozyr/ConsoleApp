namespace Domain.Models.RequentModels;

public class FileStorageRequest
{
    public Guid Guid { get; set; }

    public string? BucketPath { get; set; }
}
