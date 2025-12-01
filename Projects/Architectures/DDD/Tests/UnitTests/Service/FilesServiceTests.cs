using AutoFixture;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models.Options;
using Domain.Models.RequestModels;
using FakeItEasy;
using Infrastructure.Services.API;
using Microsoft.Extensions.Options;
using Xunit;

namespace Tests.UnitTests.Service;

public class FilesServiceTests
{
    // Фейки
    private readonly IOptions<GeneralOptions> fakeGeneralOptions;
    private readonly IOptions<FileStorageOptions> fakeFileStorageOptions;
    private readonly ILogging fakeLogging;
    private readonly ISqlProceduresRepository fakeFileRepository;

    // Реальные объекты
    private readonly FilesService fileService;

    public FilesServiceTests()
    {
        fakeGeneralOptions      = A.Fake<IOptions<GeneralOptions>>();
        fakeFileStorageOptions  = A.Fake<IOptions<FileStorageOptions>>();
        fakeLogging             = A.Fake<ILogging>();
        fakeFileRepository      = A.Fake<ISqlProceduresRepository>();

        fileService = new FilesService(fakeGeneralOptions, fakeFileStorageOptions, fakeLogging);
    }

    [Fact]
    public void GetFile_ShoultReturnFile()
    {
        // Arrange
        var fakeString  = new Fixture().Create<string>();
        var fakeStrings = new Fixture().Create<List<string>>();

        var fakeRequestModel = new Fixture().Create<FileStorageRequest>();

        A.CallTo(() => fakeFileRepository.GetDbDataListString(fakeString)).Returns(fakeStrings);

        // Act
        var result = fileService.GetFileStream(fakeRequestModel);

        // Assert
        Assert.NotNull(result);
        A.CallTo(() => fakeFileRepository.GetDbDataListString(fakeString)).MustHaveHappenedOnceExactly();
    }
}
