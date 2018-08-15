using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("settings")]
    [LoggingFilter]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("{id}")]
        public SettingsModel GetByUserId(int id)
        {
            return _settingsService.GetByUserId(id);
        }

        [HttpPost]
        public void Save([FromBody]SettingsModel settings)
        {
            _settingsService.Save(settings);
        }
    }
}
