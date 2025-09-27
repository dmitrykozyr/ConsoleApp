namespace Domain.Models.RequentModels;

public class FileDownloadRequest
{
    public Guid Guid { get; set; }

    public Dictionary<long, string>? BucketPath { get; set; }
}
