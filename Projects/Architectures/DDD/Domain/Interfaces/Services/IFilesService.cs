using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Interfaces.Services;

public interface IFilesService
{
    FileStorageResponse GetFile(FileStorageRequest model);
}
