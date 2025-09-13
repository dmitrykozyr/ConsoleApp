using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Interfaces.Services;

public interface IFileService
{
    FileStorageResponse GetFile(FileStorageRequest model);
}
