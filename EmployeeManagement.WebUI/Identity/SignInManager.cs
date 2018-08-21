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
        public SignInManager(UserManager userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<UserModel> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<UserModel>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory,
            optionsAccessor, logger, schemes)
        {}
    }
}
