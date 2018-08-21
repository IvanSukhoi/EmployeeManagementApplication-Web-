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
        
        public async Task<UserModel> GetUserByIdAsync(string token)
        {
            return await _userManager.FindByIdAsync(_jsonWebTokenHandler.GetUserClaimByRefreshToken(token, "userId"));
        }

        public async Task<UserModel> GetUserByLoginAsync(string token)
        {
            return await _userManager.FindByNameAsync(_jsonWebTokenHandler.GetUserClaimByRefreshToken(token, "login"));
        }
    }
}
