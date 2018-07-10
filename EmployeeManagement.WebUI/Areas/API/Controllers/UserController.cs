using System.Web.Http;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public UserModel GetUserModel(string[] userInformation)
        {
            return  _userService.GetUser(userInformation[0], userInformation[1]);
        }
    }
}
