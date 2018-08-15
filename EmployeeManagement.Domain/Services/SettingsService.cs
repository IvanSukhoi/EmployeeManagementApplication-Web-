using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;

        public SettingsService(IMapperWrapper mapperWrapper, IQueryableDbProvider queryableDbProvider, IUpdateDbProvider updateDbProvider)
        {
            _mapperWrapper = mapperWrapper;
            _queryableDbProvider = queryableDbProvider;
            _updateDbProvider = updateDbProvider;
        }

        public SettingsModel GetByUserId(int id)
        {
            var settings = _queryableDbProvider.Set<Settings>().Include(x => x.User).FirstOrDefault(x => x.UserId == id);

            return _mapperWrapper.Map<Settings, SettingsModel>(settings);
        }

        public void Save(SettingsModel settingsModel)
        {
            var dbEntry = _queryableDbProvider.Set<Settings>().FirstOrDefault(x => x.UserId == settingsModel.UserId);

            _mapperWrapper.Map(settingsModel, dbEntry);

            _updateDbProvider.Update(dbEntry);
        }
    }
}
