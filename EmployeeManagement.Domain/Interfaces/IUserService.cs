using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUserService
    {
        UserModel GetUserModel(string login, string password);
    }
}
