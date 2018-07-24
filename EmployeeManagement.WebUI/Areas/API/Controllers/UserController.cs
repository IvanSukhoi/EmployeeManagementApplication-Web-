using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/user")]
        public UserModel GetUserModel([FromBody]string[] userInformation)
        {
            return _userService.GetUserModel(userInformation[0], userInformation[1]);
        }
    }
}
