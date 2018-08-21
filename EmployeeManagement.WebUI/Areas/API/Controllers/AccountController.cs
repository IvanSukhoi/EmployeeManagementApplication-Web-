using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountSevice _accountSevice;
        private readonly UserManager _userManager;

        public AccountController(IAccountSevice accountSevice, UserManager userManager)
        {
            _accountSevice = accountSevice;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<UserModel> GetUserByLoginAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            return user;
        }

        [HttpPost("refreshtoken")]
        public async Task<UserModel> GetUserModelByRefreshTokenAsync([FromBody] string refreshToken)
        {
            return await _accountSevice.GetUserByIdAsync(refreshToken);
        }
    }
}
