using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Controllers;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Areas.API.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly AccountController _userController;
        private List<UserModel> _userModels;

        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _userController = new AccountController(_userService);

            Init();
        }

        [Fact]
        public void GetUserModel_ReturnUserModel_Correct()
        {
            var user = _userController.GetUserByIdAsync(new[] { "Login1", "Password1" });

            Assert.Equal(_userModels.First().Id, user.Id);
        }

        private void Init()
        {
            _userModels = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Login = "Login1",
                    Password = "Password1"
                },
                new UserModel
                {
                    Id = 2,
                    Login = "Login2",
                    Password = "Password2"
                }
            };

            A.CallTo(() => _userService.GetUserModel(A<string>.Ignored, A<string>.Ignored)).ReturnsLazily(
                (string login, string password) =>
                    _userModels.FirstOrDefault(x => (x.Login == login && x.Password == password)));
        }
    }
}
