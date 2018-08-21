using System;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.WebUI.Helpers
{
    public class SecurityStampValidator : SecurityStampValidator<UserModel>
    {
        public SecurityStampValidator(IOptions<SecurityStampValidatorOptions> options, SignInManager signInManager, ISystemClock clock) : base(options, signInManager, clock)
        {}
    }
}
