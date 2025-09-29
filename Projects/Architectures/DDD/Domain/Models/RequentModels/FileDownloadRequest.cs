namespace Domain.Models.RequentModels;

public class FileDownloadRequest
{
    public Guid Guid { get; init; }

    public Dictionary<long, string>? BucketPath { get; init; }
}
