using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager _userManager;

        public AccountController(IAccountService accountService, UserManager userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<UserModel> GetUserByLoginAsync([FromBody]string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            return user;
        }

        [HttpPost("refreshtoken")]
        public async Task<UserModel> GetUserModelByRefreshTokenAsync([FromBody] string refreshToken)
        {
            return await _accountService.GetUserByIdAsync(refreshToken);
        }
    }
}
