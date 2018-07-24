using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface ISettingsService
    {
        SettingsModel GetByUserId(int id);
        void Save(SettingsModel settings);
    }
}
