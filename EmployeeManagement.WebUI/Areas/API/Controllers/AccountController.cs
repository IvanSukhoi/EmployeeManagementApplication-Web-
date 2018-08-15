using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Filters;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("account")]
    [LoggingFilter]
    public class AccountController : Controller
    {
        private readonly IAccountSevice _accountSevice;

        public AccountController(IAccountSevice accountSevice)
        {
            _accountSevice = accountSevice;
        }

        [HttpPost]
        public async Task<UserModel> GetUserModel([FromBody] UserModel userModel)
        {
            return await _accountSevice.GetUserModelAsync(userModel.Login, userModel.Password);
        }

        [HttpPost("refreshtoken")]
        public async Task<UserModel> GetUserModelByRefreshTokenAsync([FromBody] string refreshToken)
        {
            return await _accountSevice.GetUserModelByRefreshTokenAsync(refreshToken);
        }
    }
}
