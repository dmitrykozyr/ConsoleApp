namespace Domain.Models.RequestModels;

public class FileDownloadRequest
{
    public Guid Guid { get; init; }

    public Dictionary<long, string>? BucketPath { get; init; }
}
