using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;

namespace Domain.Services.API;

public class FileService : IFileService
{
    private readonly IFileRepository _fakeFileRepository;

    public FileService(IFileRepository fakeFileRepository)
    {
        _fakeFileRepository = fakeFileRepository;
    }

    public FileStorageResponse GetFile(FileStorageRequest model)
    {
        return null;
    }
}
