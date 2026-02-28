using Domain.Interfaces;
using Infrastructure.Models.RequestModels;
using Infrastructure.Models.ResponseModels;

namespace Infrastructure.Services.API;

public interface IFilesService
{
    LoadFileResponse? GetFileByPath(FileStorageRequest model);

    FileStreamResponse GetFileStream(FileStorageRequest model);

    Task<Guid> LoadFileByBytesArray(LoadFileByBytesRequest model);

    Guid LoadFileFromFileSystemByPath(LoadFileByPathRequest model);

    Task<Guid> LoadFileFromFileSystemBySelection(LoadFileBySelectionRequest model, IFileUpload file);

    bool DeleteFile(FileStorageRequest model);
}
