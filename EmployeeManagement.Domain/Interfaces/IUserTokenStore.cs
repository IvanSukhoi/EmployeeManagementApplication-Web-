using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUserTokenStore
    {
        Task<TokenModel> GetTokenAsync(string userId);
        Task<IdentityResult> SetTokenAsync(TokenModel tokenModel);
    }
}
