using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUserCacheManager
    {
        void Set(UserModel userModel);
        void Remove(int userId);
        bool TryGetValue(int userId, UserModel userModel);
        UserModel Get(int userId);
    }
}
