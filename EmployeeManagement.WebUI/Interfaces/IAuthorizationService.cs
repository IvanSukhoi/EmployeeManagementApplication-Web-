using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.JsonWebTokenAuthentication;

namespace EmployeeManagement.WebUI.Interfaces
{
    public interface IAuthorizationService
    {
        Task<JsonWebToken> SignInAsync(UserModel userModel);
        Task<JsonWebToken> RefreshAccessTokenAsync(string token);
        void RevokeRefreshToken(string token);
        bool IsValidLifeTimeRefreshToken(string token);
    }
}
