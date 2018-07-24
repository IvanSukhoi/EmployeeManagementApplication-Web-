using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        [Route("/settings/{id}")]
        public SettingsModel GetByUserId(int id)
        {
            return _settingsService.GetByUserId(id);
        }

        [HttpPost]
        [Route("/settings")]
        public void Save([FromBody]SettingsModel settings)
        {
            _settingsService.Save(settings);
        }
    }
}
