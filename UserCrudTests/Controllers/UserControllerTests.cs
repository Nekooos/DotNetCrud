using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using UserCrud.Controllers;
using UserCrud.Models;
using UserCrud.Services;
using Xunit;

namespace UserCrudTests
{
    public class UserControllerTests
    {
        private UserController userController;
        private Mock<IUserService> userServiceMock;

        public UserControllerTests()
        {
            userServiceMock = new Mock<IUserService>(); 
            userController = new UserController(userServiceMock.Object);
        }

        [Fact(DisplayName = "GetUser returns null. 404 Not found")]
        public void GetUserNotFoundTest()
        {
            userServiceMock.Setup(userService => userService.GetById(It.IsAny<long>()))
                .Returns((User) null);

            ActionResult<User> result = userController.GetUser(1);

            Assert.IsType<NotFoundResult>(result.Result);

            userServiceMock.Verify(userService => userService.GetById(It.IsAny<long>()));
        }

        [Theory(DisplayName = "GetUser returns user. 200 OK")]
        [ClassData(typeof(TestData))]
        public void GetUserTest(User testUser)
        {
            userServiceMock.Setup(userService => userService.GetById(It.IsAny<long>()))
                .Returns(testUser);

            ActionResult<User> actionResult = userController.GetUser(1);

            OkObjectResult result = (OkObjectResult) actionResult.Result;
            User resultUser = (User) result.Value;

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.NotNull(result.Value);
            Assert.Equal(1, resultUser.Id);

            userServiceMock.Verify(userService => userService.GetById(It.IsAny<long>()));
        }

        [Theory(DisplayName ="Put different ids returns bad request")]
        [ClassData(typeof(TestData))]
        public void PutTest(User testUser)
        {
            IActionResult actionResult = userController.PutUser(2, testUser);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory(DisplayName = "Put user does not exist returns 404 Not found")]
        [ClassData(typeof(TestData))]
        public void PutNotFoundTest(User testUser)
        {
            userServiceMock.Setup(userService => userService.UserExists(It.IsAny<long>()))
                .Returns(false);
                
            IActionResult actionResult = userController.PutUser(1, testUser);

            Assert.IsType<NotFoundObjectResult>(actionResult);

            userServiceMock.Verify(userService => userService.UserExists(It.IsAny<long>()));
        }

        [Theory(DisplayName ="Put succesfull returns no content")]
        [ClassData(typeof(TestData))]
        public void putTest(User testUser)
        {
            userServiceMock.Setup(userService => userService.Put(It.IsAny<User>(), It.IsAny<long>()))
                .Verifiable();
            userServiceMock.Setup(userService => userService.UserExists(It.IsAny<long>()))
                .Returns(true);

            IActionResult actionResult = userController.PutUser(1, testUser);

            Assert.IsType<NoContentResult>(actionResult);

            userServiceMock.Verify(userService => userService.Put(It.IsAny<User>(), It.IsAny<long>()));
            userServiceMock.Verify(userService => userService.UserExists(It.IsAny<long>()));
        }


        [Theory(DisplayName = "Save User succesfull returns User")]
        [ClassData(typeof(TestData))]
        public void postUserTest(User testUser)
        {
            userServiceMock.Setup(userService => userService.Save(It.IsAny<User>()))
                .Returns(testUser);

            ActionResult<User> actionResult = userController.PostUser(testUser);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);

            userServiceMock.Verify(userService => userService.Save(It.IsAny<User>()));
        }

        [Theory(DisplayName = "Delete User succesfull returns deleted User")]
        [ClassData(typeof(TestData))]
        public void deleteUserTest(User testUser)
        {
            userServiceMock.Setup(userService => userService.Delete(It.IsAny<User>()))
                .Returns(testUser);
            userServiceMock.Setup(userService => userService.GetById(It.IsAny<long>()))
                .Returns(testUser);

            ActionResult<User> actionResult = userController.DeleteUser(It.IsAny<long>());

            Assert.IsType<OkObjectResult>(actionResult.Result);

            userServiceMock.Verify(userService => userService.Delete(It.IsAny<User>()));
            userServiceMock.Verify(userService => userService.GetById(It.IsAny<long>()));
        }

        [Fact(DisplayName = "Delete User not found returns 404 Not found")]
        public void deleteUserNotFoundTest()
        {
            userServiceMock.Setup(userService => userService.GetById(It.IsAny<long>()))
                .Returns((User) null);

            ActionResult<User> actionResult = userController.DeleteUser(It.IsAny<long>());

            Assert.IsType<NotFoundObjectResult>(actionResult.Result);

            userServiceMock.Verify(userService => userService.GetById(It.IsAny<long>()));
        }

        [Fact(DisplayName = "GetUsers returns List of Users")]
        public void GetAll()
        {
            userServiceMock.Setup(userService => userService.GetAll())
                .Returns(new List<User>() { new User(1, "name1", "test1@mail.com", 30, "password1") });

            ActionResult<IEnumerable<User>> actionResult = userController.getUsers();

            Assert.IsType<OkObjectResult>(actionResult.Result);

            userServiceMock.Verify(userService => userService.GetAll());
        }
    }
}
