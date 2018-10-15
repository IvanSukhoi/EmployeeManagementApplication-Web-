using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.WebUI.Interfaces
{
    public interface IAccountService
    {
        Task<UserModel> GetUserByIdAsync(string token);
        Task<UserModel> GetUserByLoginAsync(string token);
    }
}
