using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Identity.Stores;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.WebUI.Identity
{
    public class UserManager : UserManager<UserModel>
    {
        private readonly UserStore _userStore;

        public UserManager(UserStore store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserModel> passwordHasher, IEnumerable<IUserValidator<UserModel>> userValidators, IEnumerable<IPasswordValidator<UserModel>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserModel>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = store;
        }

        public Task<TokenModel> GetTokenByIdAsync(string userId)
        {
            return _userStore.GetTokenAsync(userId);
        }

        public Task<IdentityResult> SetTokenAsync(TokenModel tokenModel)
        {
            return _userStore.SetTokenAsync(tokenModel);
        }
    }
}
