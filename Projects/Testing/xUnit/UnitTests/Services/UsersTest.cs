using AppToTest.Interfaces;
using AppToTest.Models;
using AppToTest.Services;
using AutoFixture;
using FakeItEasy;
using Xunit;

namespace xUnit_.UnitTests.Services;

public class UsersTest
{
    private readonly IUsersRepository fakeUsersRepository;
    private readonly UsersService usersService;

    public UsersTest()
    {
        fakeUsersRepository = A.Fake<IUsersRepository>();
        usersService = new UsersService(fakeUsersRepository);
    }

    [Fact]
    public void AddUser_ShouldResurnOne()
    {
        // arrange
        var fakeUser = new Fixture().Create<User>();

        A.CallTo(() => fakeUsersRepository.AddUser(fakeUser)).Returns(1);

        // act
        var result = usersService.AddUser(fakeUser);

        // assert
        Assert.True(result == 1);
        A.CallTo(() => fakeUsersRepository.AddUser(fakeUser)).MustHaveHappenedOnceExactly();
    }
}
