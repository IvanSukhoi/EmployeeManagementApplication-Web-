using System;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IAuthorizationService = EmployeeManagement.WebUI.Interfaces.IAuthorizationService;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("jwt")]
    public class JwtBearerAuthenticationController : Controller
    {
        private readonly UserManager _userManager;
        private readonly SignInManager _signInManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<JwtBearerAuthenticationController> _logger;

        public JwtBearerAuthenticationController(UserManager userManager, SignInManager signInManager,
            IAuthorizationService authorizationService, ILogger<JwtBearerAuthenticationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            _logger.LogInformation(Environment.NewLine + "Login attempt" + Environment.NewLine +
                                   "Id: {0}" + Environment.NewLine +
                                   "Login: {1}" + Environment.NewLine + 
                                   "Result: {2}", 
                model.Id, model.Login, result);

            if (result.Succeeded)
            {
                return Ok(await _authorizationService.SignInAsync(user));
            }

            return BadRequest("Could not verify username and password");
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] string token)
        {
            if (_authorizationService.IsValidLifeTimeRefreshToken(token))
            {
                return Ok(await _authorizationService.RefreshAccessTokenAsync(token));
            }

            return BadRequest("Could not verify refresh token");
        }

        [HttpPost("deactivate")]
        public void Deactivate([FromBody] UserModel userModel)
        {
            _logger.LogInformation(Environment.NewLine + "Deactivate user" + Environment.NewLine + 
                                   "Id: {0}" + Environment.NewLine +
                                   "Login: {1}",
                userModel.Id, userModel.Login);

            _userManager?.UpdateSecurityStampAsync(userModel);
        }
    }
}