using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.WebUI.Interfaces
{
    public interface IAccountSevice
    {
        Task<UserModel> GetUserModelByRefreshTokenAsync(string token);
        Task<UserModel> GetUserModelAsync(string login, string password);
    }
}
