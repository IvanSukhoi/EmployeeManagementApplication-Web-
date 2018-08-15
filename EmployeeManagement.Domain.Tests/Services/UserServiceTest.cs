//using System.Collections.Generic;
//using System.Linq;
//using EmployeeManagement.DataEF;
//using EmployeeManagement.DataEF.Entities;
//using EmployeeManagement.DataEF.Interfaces;
//using EmployeeManagement.Domain.Mappings;
//using EmployeeManagement.Domain.Models;
//using EmployeeManagement.Domain.Services;
//using FakeItEasy;
//using Xunit;

//namespace EmployeeManagement.Domain.Tests.Services
//{
//    public class UserServiceTest
//    {
//        private readonly IMapperWrapper _mapperWrapper;
//        private readonly IQueryableDbProvider _queryableDbProvider;
//        private readonly UserService _userService;
//        private List<User> _users;

//        public UserServiceTest()
//        {
//            _mapperWrapper = A.Fake<IMapperWrapper>();
//            _queryableDbProvider = A.Fake<IQueryableDbProvider>();
//            _userService = new UserService(_mapperWrapper, _queryableDbProvider);

//            Init();
//        }

//        [Fact]
//        public void GetUserModel_ReturnUser_Correct()
//        {
//            var user = _userService.GetUserModel("Login1", "Password1");

//            Assert.Equal(1, user.Id);
//            Assert.Equal("Login1", user.Login);
//            Assert.Equal("Password1", user.Password);
//        }

//        private void Init()
//        {
//            _users = new List<User>
//            {
//                new User
//                {
//                    Id = 1,
//                    Login = "Login1",
//                    Password = "Password1"
//                },
//                new User
//                {
//                    Id = 2,
//                    Login = "Login2",
//                    Password = "Password2"
//                }
//            };

//            A.CallTo(() => _mapperWrapper.Map<User, UserModel>(A<User>.Ignored)).ReturnsLazily((User user) => new UserModel
//            {
//                Id = user.Id,
//                Login = user.Login,
//                Password = user.Password
//            });

//            A.CallTo(() => _queryableDbProvider.Set<User>()).Returns(_users.AsQueryable());
//        }
//    }
//}
