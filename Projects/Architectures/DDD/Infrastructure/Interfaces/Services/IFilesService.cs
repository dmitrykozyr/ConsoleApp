using Domain.Interfaces;
using Infrastructure.Models.RequestModels;
using Infrastructure.Models.ResponseModels;

namespace Infrastructure.Services.API;

public interface IFilesService
{
    Task<LoadFileResponse?> GetFileByPath(FileStorageRequest model);

    Task<FileStreamResponse> GetFileStream(FileStorageRequest model);

    Task<Guid> LoadFileByBytesArray(LoadFileByBytesRequest model);

    Task<Guid> LoadFileFromFileSystemByPath(LoadFileByPathRequest model);

    Task<Guid> LoadFileFromFileSystemBySelection(LoadFileBySelectionRequest model, IFileUpload file);

    Task<bool> DeleteFile(FileStorageRequest model);
}
