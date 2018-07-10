using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IUserService
    {
        UserModel GetUser(string login, string password);
    }
}
