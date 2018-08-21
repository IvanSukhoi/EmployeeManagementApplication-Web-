using System;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("coockie")]
    public class CoockieAuthenticationController : Controller
    {
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;
        private readonly ILogger<JwtBearerAuthenticationController> _logger;

        public CoockieAuthenticationController(UserManager userManager, SignInManager signInManager, ILogger<JwtBearerAuthenticationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody]LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, false);

            _logger.LogInformation(Environment.NewLine + "Login attempt" + Environment.NewLine +
                                   "Id: {0}" + Environment.NewLine +
                                   "Login: {1}" + Environment.NewLine +
                                   "Result: {2}",
                model.Id, model.Login, result);

            if (!result.Succeeded) return BadRequest("Authentication failed");

            return Ok("Authentication success");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel model)
        {
            var userModel = new UserModel
            {
                Id = model.Id,
                Login = model.Login
            };

            var result = await _userManager.CreateAsync(userModel, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(userModel, true);

                return Ok("Authentication success");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest("Authentication failed");
        }

        [HttpGet("login/{returnUrl?}")]
        public string Login(string returnUrl = null)
        {
            return string.Format("Access failed." + " " + "ReturnUrl: {0}", returnUrl);
        }

        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}