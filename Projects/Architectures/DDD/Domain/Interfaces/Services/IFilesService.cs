using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Interfaces.Services;

public interface IFilesService
{
    LoadFileResponse? GetFileByPath(FileStorageRequest model);

    FileStreamResponse GetFileStream(FileStorageRequest model);

    Task<Guid> LoadFileByBytesArray(LoadFileByBytesRequest model);

    Guid LoadFileFromFileSystemByPath(LoadFileByPathRequest model);

    Task<Guid> LoadFileFromFileSystemBySelection(LoadFileBySelectionRequest model, IFormFile file); //! using Microsoft.AspNetCore.Http;

    bool DeleteFile(FileStorageRequest model);
}
