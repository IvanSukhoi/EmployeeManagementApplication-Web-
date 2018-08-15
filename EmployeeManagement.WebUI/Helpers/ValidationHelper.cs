using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.WebUI.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.WebUI.Helpers
{
    public class ValidationHelper
    {
        private UserSettingsModel _userSettingsModel;

        public async Task ValidationSecurityStamp(TokenValidatedContext context)
        {
            SetSettingsUserModel(context);

            if (DateTime.UtcNow.CompareTo(_userSettingsModel.PreviousValidationTime.AddMinutes(10)) >= 0)
            {
                var signInManager = context.HttpContext.RequestServices.GetService<SignInManager>();
                var result = await signInManager.ValidateSecurityStampAsync(context.Principal);

                if (result == null)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Fail("The claim security stamp is not present in the token");
                }
                else
                {
                    context.Success();
                }

                _userSettingsModel.PreviousValidationTime = DateTime.UtcNow;
            }
            else
            {
                context.Success();
            }
        }

        private void SetSettingsUserModel(TokenValidatedContext context)
        {
            var userIdClaim = context.Principal.Claims.FirstOrDefault(x => x.Type == "userId");

            if (userIdClaim == null) return;
            var userId = int.Parse(userIdClaim.Value);

            if (_userSettingsModel != null)
            {
                if (_userSettingsModel.UserId != userId)
                {
                    InitUserSettingsModel(userId);
                }
            }
            else
            {
                InitUserSettingsModel(userId);
            }
        }

        private void InitUserSettingsModel(int userId)
        {
            _userSettingsModel = new UserSettingsModel
            {
                UserId = userId,
                PreviousValidationTime = new DateTime()
            };
        }
    }
}
