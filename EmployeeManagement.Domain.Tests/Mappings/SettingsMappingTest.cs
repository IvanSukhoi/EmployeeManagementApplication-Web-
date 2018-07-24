using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class SettingsMappingTest
    {
        private readonly MapperWrapper _mapperWrapper;
        private SettingsModel _settingsModel;
        private Settings _settings;

        public SettingsMappingTest(MapperSetUp mapperSetUp)
        {
            _mapperWrapper = mapperSetUp.GetMapperWrapper();

            Init();
        }

        [Fact]
        public void AutoMapper_ConvertSettings_SettingsModel_Correct()
        {
            var settingsModel = _mapperWrapper.Map<Settings, SettingsModel>(_settings);

            AssertPropertyValue(_settings, settingsModel);
        }

        [Fact]
        public void AutoMapper_ConvertSettingsModel_Settings_Correct()
        {
            var settings = _mapperWrapper.Map<SettingsModel, Settings>(_settingsModel);

            AssertPropertyValue(settings, _settingsModel);
        }

        [Fact]
        public void AutoMapper_CopySettings_SettingsModel_Correct()
        {
            var settingsModel = new SettingsModel();

            _mapperWrapper.Map(_settings, settingsModel);

            AssertPropertyValue(_settings, settingsModel);
        }

        [Fact]
        public void AutoMapper_CopySettingsModel_Settings_Correct()
        {
            var settings = new Settings();

            _mapperWrapper.Map(_settingsModel, settings);

            AssertPropertyValue(settings, _settingsModel);
        }

        private void AssertPropertyValue(Settings settings, SettingsModel settingsModel)
        {
            Assert.Equal(settings.Language, settingsModel.Language);
            Assert.Equal(settings.Topic, settingsModel.Theme);
            Assert.Equal(settings.UserID, settingsModel.UserId);
        }

        private void Init()
        {
            _settings = new Settings
            {
                Language = Language.English,
                Topic = Theme.Dark,
                UserID = 1
            };

            _settingsModel = new SettingsModel
            {
                Language = Language.English,
                Theme = Theme.Dark,
                UserId = 1
            };
        }
    }
}
