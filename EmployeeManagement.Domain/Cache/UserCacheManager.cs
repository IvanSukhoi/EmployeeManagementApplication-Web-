using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Cache
{
    public class UserCacheManager: IUserCacheManager
    {
        private readonly ICacheManager _cacheManager;

        public UserCacheManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Set(UserModel userModel)
        {
            _cacheManager.Set(userModel.Id, userModel);
        }

        public void Remove(int userId)
        {
            _cacheManager.Remove(userId);
        }

        public bool TryGetValue(int userId, UserModel userModel)
        {
            return _cacheManager.TryGetValue(userId, userModel);
        }

        public UserModel Get(int userId)
        {
            return _cacheManager.Get(userId) as UserModel;
        }
    }
}
