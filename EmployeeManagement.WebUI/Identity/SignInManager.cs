using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.WebUI.Identity
{
    public class SignInManager : SignInManager<UserModel>
    {
        private readonly UserManager<UserModel> _userManager;

        public SignInManager(UserManager userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<UserModel> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<UserModel>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory,
            optionsAccessor, logger, schemes)
        {
            _userManager = userManager;
        }

        public override Task<SignInResult> PasswordSignInAsync(UserModel user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (user.Password.Equals(password))
            {
                return Task.FromResult(SignInResult.Success);
            }

            return Task.FromResult(SignInResult.Failed);
        }

        public override async Task<UserModel> ValidateSecurityStampAsync(ClaimsPrincipal principal)
        {
            var securityStamp = principal.Claims.SingleOrDefault(x => x.Type == "securityStamp")?.Value;
            var userId = principal.Claims.SingleOrDefault(x => x.Type == "userId")?.Value;
            
            var userModel = await _userManager.FindByIdAsync(userId);

            if(userModel.SecurityStamp.Contains(securityStamp))
            {
                return userModel;
            }

            return null;
        }
    }
}
