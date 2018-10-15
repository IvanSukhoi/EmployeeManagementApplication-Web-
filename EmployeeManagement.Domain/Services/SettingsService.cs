using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(IMapperWrapper mapperWrapper, IQueryableDbProvider queryableDbProvider, IUpdateDbProvider updateDbProvider, ILogger<SettingsService> logger)
        {
            _mapperWrapper = mapperWrapper;
            _queryableDbProvider = queryableDbProvider;
            _updateDbProvider = updateDbProvider;
            _logger = logger;
        }

        public SettingsModel GetByUserId(int id)
        {
            _logger.LogInformation("Start method GetByUserId in settings service");
            var settings = _queryableDbProvider.Set<Settings>().Include(x => x.User).FirstOrDefault(x => x.UserId == id);
            var settingsModels = _mapperWrapper.Map<Settings, SettingsModel>(settings);
            _logger.LogInformation("Method GetByUserId is complete");

            return settingsModels;
        }

        public void Save(SettingsModel settingsModel)
        {
            var dbEntry = _queryableDbProvider.Set<Settings>().FirstOrDefault(x => x.UserId == settingsModel.UserId);

            _mapperWrapper.Map(settingsModel, dbEntry);

            _updateDbProvider.Update(dbEntry);
        }
    }
}
