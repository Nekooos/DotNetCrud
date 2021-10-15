using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UserCrud.Models;
using UserCrud.Services;
using Xunit;

namespace UserCrudTests
{
    public class UserServiceTests
    {
        private Mock<UserContext> userContextMock;

        private UserService userService;

        public UserServiceTests()
        {
            var mockSet = new Mock<DbSet<User>>();

            userContextMock = new Mock<UserContext>();
            userContextMock.Setup(mock => mock.Users).Returns(mockSet.Object);
            userService = new UserService(userContextMock.Object);
        }

        [Theory (DisplayName ="GetById returns user")]
        [ClassData(typeof(TestData))]
        public void GetByIdTest(User testUser)
        {

            userContextMock.Setup(userContext =>
                userContext.Users.Find(It.IsAny<long>()))
                .Returns(() => testUser);

            User user = userService.GetById(testUser.Id);

            Assert.Equal(testUser.Id, user.Id);

            userContextMock.Verify(userContext => userContext.Users.Find(It.IsAny<long>()), Times.Once());
        }

        [Theory (DisplayName ="GetById returns null")]
        [ClassData(typeof(TestData))]
        public void GetByIdReturnsNullTest(User testUser)
        {
            userContextMock.Setup(userContext =>
                userContext.Users.Find(It.IsAny<long>()))
                .Returns<User>(null);

            User user = userService.GetById(testUser.Id);

            Assert.Null(user);

            userContextMock.Verify(userContext => userContext.Users.Find(It.IsAny<long>()), Times.Once());
        }

        [Theory (DisplayName ="Save user return user")]
        [ClassData(typeof(TestData))]
        public void SaveTest(User testUser)
        {
            userContextMock.Setup(userContext =>
                userContext.Users.Add(It.IsAny<User>()))
                .Verifiable();

            User savedUser = userService.Save(testUser);

            Assert.Equal(testUser.Id, savedUser.Id);

            userContextMock.Verify(userContext => userContext.SaveChanges(), Times.Once());
            userContextMock.Verify(userContext => userContext.Users.Add(It.IsAny<User>()), Times.Once());
        }

        [Theory (DisplayName ="Delete returns deleted user")]
        [ClassData(typeof(TestData))]
        public void DeleteTest(User testUser)
        {
            userContextMock.Setup(userContext =>
                userContext.Users.Remove(It.IsAny<User>()))
                .Verifiable();

            User savedUser = userService.Delete(testUser);

            Assert.Equal(testUser.Id, savedUser.Id);

            userContextMock.Verify(userContext => userContext.SaveChanges(), Times.Once());
            userContextMock.Verify(userContext => userContext.Users.Remove(It.IsAny<User>()), Times.Once());
        }

        [Fact(DisplayName = "GetAll returns a List of users")]
        public void GetAllTest()
        {

       
        }

        [Fact(DisplayName = "Put returns no content")]
        public void Put()
        {

        }
    }
}
