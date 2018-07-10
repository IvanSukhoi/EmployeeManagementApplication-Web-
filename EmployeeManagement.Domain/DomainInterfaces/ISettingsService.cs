using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface ISettingsService
    {
        SettingsModel GetById(int id);
        void Save(SettingsModel settings);
    }
}
