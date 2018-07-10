using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class SettingsService : ISettingsService
    {
        private readonly ManagementContext _managementContext;

        private readonly IMapperWrapper _mapperWrapper;

        public SettingsService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public SettingsModel GetById(int id)
        {
            var settings = _managementContext.Settings.Include("User").FirstOrDefault(x => x.UserID == id);

            return _mapperWrapper.Map<Settings, SettingsModel>(settings);
        }

        public void Save(SettingsModel settingsModel)
        {

            var settings = _mapperWrapper.Map<SettingsModel, Settings>(settingsModel);
            var dbEntry = _managementContext.Settings.FirstOrDefault(x => x.UserID == settings.UserID);

            if (dbEntry == null)
            {
                _managementContext.Settings.Add(settings);
            }
            else
            {
                _mapperWrapper.Map(settings, dbEntry);
                _managementContext.Entry(dbEntry).Reference(x => x.User).Load();
            }

            _managementContext.SaveChanges();
        }
    }
}
