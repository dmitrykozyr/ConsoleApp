using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Services.API;

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public FileStorageResponse GetFile(FileStorageRequest model)
    {
        return null;
    }
}
