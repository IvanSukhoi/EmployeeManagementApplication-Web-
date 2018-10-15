using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Identity;
using EmployeeManagement.WebUI.Interfaces;
using EmployeeManagement.WebUI.JsonWebTokenAuthentication;

namespace EmployeeManagement.WebUI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly JsonWebTokenHandler _jwtHandler;
        private readonly UserManager _userManager;
        private readonly SignInManager _signInManager;

        public AuthorizationService(UserManager userManager, JsonWebTokenHandler jwtHandler, SignInManager signInManager)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _signInManager = signInManager;
        }

        public async Task<JsonWebToken> SignInAsync(UserModel userModel)
        {
            var jsonWebToken = await GenerateJSonWebToken(userModel);

            return jsonWebToken;
        }

        public async Task<JsonWebToken> RefreshAccessTokenAsync(string token)
        {
            var userId = _jwtHandler.GetUserClaimByRefreshToken(token, ClaimTypes.NameIdentifier);

            var tokenDb = await _userManager.GetTokenByIdAsync(userId);

            if (tokenDb.JsonWebRefreshToken.Contains(token))
            {
                var userModel = await _userManager.FindByIdAsync(userId);

                return await GenerateJSonWebToken(userModel);
            }

            throw new InvalidOperationException("This token is not valid");
        }

        private async Task<JsonWebToken> GenerateJSonWebToken(UserModel userModel)
        {
            var claimsPrincipal = await _signInManager.ClaimsFactory.CreateAsync(userModel);

            var jSonWebToken = _jwtHandler.CreateJsonWebToken(claimsPrincipal.Claims.ToList());

            await _userManager.SetTokenAsync(new TokenModel
            {
                JsonWebRefreshToken = jSonWebToken.RefreshToken,
                UserId = userModel.Id
            });

            return jSonWebToken;
        }

        public bool IsValidLifeTimeRefreshToken(string token)
        {
            return _jwtHandler.IsValidLifeTimeRefreshToken(token);
        }

        public void RevokeRefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}