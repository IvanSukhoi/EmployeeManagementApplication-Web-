using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Identity.Stores
{
    public class UserStore : IUserRoleStore<UserModel>, IUserTokenStore, IUserSecurityStampStore<UserModel>
    {
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IUserCacheManager _userCacheManager;

        public UserStore(IQueryableDbProvider queryableDbProvider, IUpdateDbProvider updateDbProvider, IMapperWrapper mapperWrapper, IUserCacheManager userCacheManager)
        {
            _queryableDbProvider = queryableDbProvider;
            _updateDbProvider = updateDbProvider;
            _mapperWrapper = mapperWrapper;
            _userCacheManager = userCacheManager;
        }

        public Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            _updateDbProvider.Add(_mapperWrapper.Map<UserModel, User>(user));

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            _userCacheManager.Remove(user.Id);
            _updateDbProvider.Update(_mapperWrapper.Map<UserModel, User>(user));

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            _userCacheManager.Remove(user.Id);
            _updateDbProvider.Delete(_mapperWrapper.Map<UserModel, User>(user));

            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var id = int.Parse(userId);

            var userModel = new UserModel();
            var result = _userCacheManager.TryGetValue(id, userModel);

            if (result)
            {
                _userCacheManager.Set(userModel);
                return userModel;
            }

            var user = await _queryableDbProvider.Set<User>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

            return _mapperWrapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _queryableDbProvider.Set<User>().Include(x => x.Settings).FirstOrDefaultAsync(x => x.Login == normalizedUserName, cancellationToken: cancellationToken);

            return _mapperWrapper.Map<User, UserModel>(user);
        }

        public Task<IList<string>> GetRolesAsync(UserModel user, CancellationToken cancellationToken)
        {
            var roles = _queryableDbProvider.Set<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.Role.Name);

            return Task.FromResult<IList<string>>(roles.ToList());
        }

        public Task<TokenModel> GetTokenAsync(string userId)
        {
            var token = _queryableDbProvider.Set<Token>().Last(x => x.UserId == int.Parse(userId));

            return Task.FromResult(_mapperWrapper.Map<Token, TokenModel>(token));
        }

        public Task<IdentityResult> SetTokenAsync(TokenModel tokenModel)
        {
            _updateDbProvider.Add(_mapperWrapper.Map<TokenModel, Token>(tokenModel));

            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetSecurityStampAsync(UserModel user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            _updateDbProvider.Update(_mapperWrapper.Map<UserModel, User>(user));

            return Task.CompletedTask;
        }

        public async Task<string> GetSecurityStampAsync(UserModel user, CancellationToken cancellationToken)
        {
            var dbEntry = await _queryableDbProvider.Set<User>().FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken: cancellationToken);

            return dbEntry.SecurityStamp;
        }
        public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public void Dispose()
        { }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task AddToRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<bool> IsInRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserModel>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
