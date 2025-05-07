using FakeItEasy;
using System.Linq;
using Xunit;
using xUnit_.FunctionalityToTest.User_;

namespace xUnit_;

// Имя класса соответствует имени класса в проекте AppToTest, который будем тестировать
public class UserManagementTest
{
    [Fact]
    public void Add_CreateUser()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.AddUser(new ("Amazing", "User"));

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);

        Assert.NotNull(savedUser);
        Assert.Equal("Amazing", savedUser.firstName);
        Assert.Equal("User", savedUser.lastName);
        Assert.False(savedUser.VerifiedEmail);
    }
    
    [Fact]
    public void Update_UpdateMobileNumber()
    {
        var userManagement = new UserManagement();
        
        userManagement.AddUser(new ("Amazing", "User"));

        var firstUser = userManagement.AllUsers.First();

        firstUser.Phone = "+123334445577";

        userManagement.UpdatePhone(firstUser);

        var savedUser = Assert.Single(userManagement.AllUsers);

        Assert.NotNull(savedUser);
        Assert.Equal("+123334445577", savedUser.Phone);
    }

    // FakeItEasy
    [Fact]
    public void FakeItEasy()
    {
        // Arrange
        var fakeDependency = A.Fake<IDependency>();

        var myService = new MyService(fakeDependency);

        // Act
        myService.ProcessData();

        // Assert
        A.CallTo(() => fakeDependency.SomeMethod()).MustHaveHappened();
    }
}

public interface IDependency
{
    void SomeMethod();
}

public class MyService
{
    private readonly IDependency _dependency;

    public MyService(IDependency dependency)
    {
        _dependency = dependency;
    }

    public void ProcessData()
    {
        _dependency.SomeMethod();
    }
}
