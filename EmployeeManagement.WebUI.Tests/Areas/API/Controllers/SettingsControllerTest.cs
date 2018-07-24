using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Controllers;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Areas.API.Controllers
{
    public class SettingsControllerTest
    {
        private readonly ISettingsService _settingsService;
        private readonly SettingsController _settingsController;
        private List<SettingsModel> _settingsModels;

        public SettingsControllerTest()
        {
            _settingsService = A.Fake<ISettingsService>();
            _settingsController = new SettingsController(_settingsService);

            Init();
        }

        [Fact]
        public void GetById_ReturnSettingsById_Correct()
        {
           var settings = _settingsController.GetByUserId(1);

            Assert.Equal(1, settings.UserId);
            Assert.Equal(Language.English, settings.Language);
        }

        [Fact]
        public void Save_SaveEmployee_Correct()
        {
            var settings = new SettingsModel();

            _settingsController.Save(settings);

            A.CallTo(() => _settingsService.Save(settings)).MustHaveHappenedOnceExactly();
        }

        private void Init()
        {
            _settingsModels = new List<SettingsModel>
            {
                new SettingsModel
                {
                    UserId = 1,
                    Language = Language.English
                }
            };

            A.CallTo(() => _settingsService.GetByUserId(A<int>.Ignored))
                .ReturnsLazily((int userId) => _settingsModels.FirstOrDefault(x => x.UserId == userId));
        }
    }
}
