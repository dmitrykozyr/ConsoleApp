using AutoFixture;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models.Options;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Services.API;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Xunit;

namespace Tests.UnitTests.Service;

public class FilesServiceTests
{
    // Фейки
    private readonly IOptions<GeneralOptions> fakeGeneralOptions;
    private readonly IOptions<FileStorageOptions> fakeFileStorageOptions;
    private readonly ILogging fakeLogging;
    private readonly IFileRepository fakeFileRepository;

    // Реальные объекты
    private readonly FilesService fileService;

    public FilesServiceTests()
    {
        fakeGeneralOptions = A.Fake<IOptions<GeneralOptions>>();
        fakeFileStorageOptions = A.Fake<IOptions<FileStorageOptions>>();
        fakeLogging = A.Fake<ILogging>();
        fakeFileRepository = A.Fake<IFileRepository>();

        fileService = new FilesService(fakeGeneralOptions, fakeFileStorageOptions, fakeLogging);
    }

    [Fact]
    public void GetFile_ShoultReturnFile()
    {
        // Arrange
        var fakeRequestModel = new Fixture().Create<FileStorageRequest>();
        var fakeResult = new Fixture().Create<FileStorageResponse>();

        A.CallTo(() => fakeFileRepository.GetFile(fakeRequestModel)).Returns(fakeResult);

        // Act
        var result = fileService.GetFileStream(fakeRequestModel);

        // Assert
        Assert.NotNull(result);
        A.CallTo(() => fakeFileRepository.GetFile(fakeRequestModel)).MustHaveHappenedOnceExactly();
    }
}
