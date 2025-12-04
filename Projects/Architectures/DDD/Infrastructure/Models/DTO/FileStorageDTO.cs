using Domain.Models;

namespace Infrastructure.Models.DTO;

public class FileStorageDTO
{
    public Guid Guid { get; init; }

    public string? BucketPath { get; init; }

    public Token? Token { get; init; }
}
