using System.Web.Http;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class SettingsController : ApiController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public SettingsModel GetById(int id)
        {
            return _settingsService.GetById(id);
        }

        [HttpPost]
        public void Save(SettingsModel settings)
        {
            _settingsService.Save(settings);
        }
    }
}
