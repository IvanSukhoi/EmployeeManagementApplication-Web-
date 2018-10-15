using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("settings")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ISettingsService settingsService, ILogger<SettingsController> logger)
        {
            _settingsService = settingsService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public SettingsModel GetByUserId(int id)
        {
            _logger.LogInformation("Start method GetByUserId in settings controller");
            var settings = _settingsService.GetByUserId(id);
            _logger.LogInformation("Method GetByUserId is complete");

            return settings;
        }

        [HttpPost]
        public void Save([FromBody]SettingsModel settings)
        {
            _settingsService.Save(settings);
        }
    }
}
