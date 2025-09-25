using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Services.API;

public class FilesService : IFilesService
{
    private readonly IFileRepository _fileRepository;

    public FilesService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public FileStorageResponse GetFile(FileStorageRequest model)
    {
        return null;
    }
}
