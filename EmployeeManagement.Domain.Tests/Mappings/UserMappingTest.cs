﻿using EmployeeManagement.DataEF;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class UserMappingTest
    {
        private readonly MapperWrapper _mapperWrapper;
        private UserModel _userModel;
        private User _user;

        public UserMappingTest(MapperSetUp mapperSetUp)
        {
            _mapperWrapper = mapperSetUp.GetMapperWrapper();

            Init();
        }

        [Fact]
        public void AutoMapper_ConvertUser_UserModel_Correct()
        {
            var userModel = _mapperWrapper.Map<User, UserModel>(_user);

            AssertPropertyValue(_user, userModel);
        }

        [Fact]
        public void AutoMapper_ConvertUserModel_User_Correct()
        {
            var user = _mapperWrapper.Map<UserModel, User>(_userModel);

            AssertPropertyValue(user, _userModel);
        }

        [Fact]
        public void AutoMapper_CopyUser_UserModel_Correct()
        {
            var userModel = new UserModel();

            _mapperWrapper.Map(_user, userModel);

            AssertPropertyValue(_user, userModel);
        }

        [Fact]
        public void AutoMapper_CopyUserModel_User_Correct()
        {
            var user = new User();

            _mapperWrapper.Map(_userModel, user);

            AssertPropertyValue(user, _userModel);
        }

        private void AssertPropertyValue(User user, UserModel userModel)
        {
            Assert.Equal(user.Login, userModel.Login);
            Assert.Equal(user.Password, userModel.Password);
            Assert.Equal(user.ID, userModel.Id);
        }

        private void Init()
        {
            _user = new User
            {
                ID = 1,
                Login = "Login1",
                Password = "Password1",
            };

            _userModel = new UserModel
            {
                Id = 2,
                Login = "Login2",
                Password = "Password2"
            };
        }
    }
}
