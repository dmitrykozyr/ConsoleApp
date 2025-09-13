using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Interfaces.Repositories;

public interface IFileRepository
{
    FileStorageResponse GetFile(FileStorageRequest model);
}
