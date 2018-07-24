using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.Services;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Services
{
    public class SettingsServiceTest
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;
        private readonly SettingsService _settingsService;
        private List<Settings> _settings;

        public SettingsServiceTest()
        {
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _queryableDbProvider = A.Fake<IQueryableDbProvider>();
            _updateDbProvider = A.Fake<IUpdateDbProvider>();
            _settingsService = new SettingsService(_mapperWrapper, _queryableDbProvider, _updateDbProvider);

            Init();
        }

        [Fact]
        public void GetByUserId_ReturnSettingsByUserId_Correct()
        {
            var settings = _settingsService.GetByUserId(1);

            Assert.Equal(1, settings.UserId);
            Assert.Equal(Language.English, settings.Language);
        }

        [Fact]
        public void Save_SaveSettings_Correct()
        {
            var settingsModel = new SettingsModel()
            {
                UserId = 1,
                Language = Language.Russian
            };

            _settingsService.Save(settingsModel);

            Assert.Equal(Language.Russian, _settings.First().Language);
        }

        private void Init()
        {
            _settings = new List<Settings>
            {
                new Settings
                {
                    UserID = 1,
                    Language = Language.English
                },
                new Settings
                {
                    UserID = 2,
                    Language = Language.Russian
                },
                new Settings
                {
                    UserID = 3,
                    Language = Language.English
                }
            };

            A.CallTo(() => _mapperWrapper.Map<Settings, SettingsModel>(A<Settings>.That.Matches(x => x != null))).ReturnsLazily(
                (Settings settings) => new SettingsModel()
                {
                    UserId = settings.UserID,
                    Language = settings.Language
                });

            A.CallTo(() => _mapperWrapper.Map(A<SettingsModel>.Ignored, A<Settings>.Ignored)).Invokes(
                (SettingsModel settingsModel, Settings settings) =>
                {
                    settings.UserID = settingsModel.UserId;
                    settings.Language = settingsModel.Language;
                });

            A.CallTo(() => _queryableDbProvider.Set<Settings>()).Returns(_settings.AsQueryable());

            A.CallTo(() => _updateDbProvider.Update(A<Settings>.Ignored)).Invokes((Settings settings) =>
            {
                var entity = _settings.FirstOrDefault(x => x.UserID == settings.UserID);
                if (entity != null) entity.Language = settings.Language;
            });
        }
    }
}
