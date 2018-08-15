using System;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using EmployeeManagement.WebUI.JsonWebTokenAuthentication;

namespace EmployeeManagement.WebUI.Services
{
    public class AccountService: IAccountSevice
    {
        private readonly JsonWebTokenHandler _jsonWebTokenHandler;
        private readonly UserManager _userManager;

        public AccountService(JsonWebTokenHandler jsonWebTokenHandler, UserManager userManager)
        {
            _jsonWebTokenHandler = jsonWebTokenHandler;
            _userManager = userManager;
        }

        public async Task<UserModel> GetUserModelByRefreshTokenAsync(string token)
        {
            return await _userManager.FindByIdAsync(_jsonWebTokenHandler.GetUserIdByRefreshToken(token));
        }

        public async Task<UserModel> GetUserModelAsync(string login, string password)
        {
            var userModel = await _userManager.FindByNameAsync(login);

            if (userModel.Password.Contains(password))
            {
                return userModel;
            }

            throw new InvalidOperationException("Incorrect password");
        }
    }
}
