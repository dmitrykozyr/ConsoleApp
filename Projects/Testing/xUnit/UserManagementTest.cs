using FakeItEasy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using xUnit_.FunctionalityToTest.User_;

namespace xUnit_
{
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
            Assert.Equal(savedUser.firstName, "Amazing");
            Assert.Equal(savedUser.lastName, "User");
            Assert.False(savedUser.verifiedEmail);
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
            Assert.Equal(savedUser.Phone, "+123334445577");
        }
    }
}
