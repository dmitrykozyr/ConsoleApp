using Domain.Models.RequestModels;
using Domain.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services;

public interface IFilesService
{
    LoadFileResponse? GetFileByPath(FileStorageRequest model);

    FileStreamResponse GetFileStream(FileStorageRequest model);

    Task<Guid> LoadFileByBytesArray(LoadFileByBytesRequest model);

    Guid LoadFileFromFileSystemByPath(LoadFileByPathRequest model);

    Task<Guid> LoadFileFromFileSystemBySelection(LoadFileBySelectionRequest model, IFormFile file);

    bool DeleteFile(FileStorageRequest model);
}
