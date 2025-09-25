using AutoFixture;
using Domain.Interfaces.Repositories;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Domain.Services.API;
using FakeItEasy;
using Xunit;

namespace Tests.UnitTests.Service;

public class FilesServiceTests
{
    // Фейки
    private readonly IFileRepository fakeFileRepository;

    // Реальные объекты
    private readonly FilesService fileService;

    public FilesServiceTests()
    {
        fakeFileRepository = A.Fake<IFileRepository>();

        fileService = new FilesService(fakeFileRepository);
    }

    [Fact]
    public void GetFile_ShoultReturnFile()
    {
        // Arrange
        var fakeRequestModel = new Fixture().Create<FileStorageRequest>();
        var fakeResult = new Fixture().Create<FileStorageResponse>();

        A.CallTo(() => fakeFileRepository.GetFile(fakeRequestModel)).Returns(fakeResult);

        // Act
        var result = fileService.GetFile(fakeRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeResult, result);
        A.CallTo(() => fakeFileRepository.GetFile(fakeRequestModel)).MustHaveHappenedOnceExactly();
    }
}
