using System.Linq;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;

        public UserService(IMapperWrapper mapperWrapper, IQueryableDbProvider queryableDbProvider)
        {
            _mapperWrapper = mapperWrapper;
            _queryableDbProvider = queryableDbProvider;
        }

        public UserModel GetUserModel(string login, string password)
        {
            var user = _queryableDbProvider.Set<User>().Include("Settings").FirstOrDefault(x => (x.Login == login && x.Password == password));

            return _mapperWrapper.Map<User, UserModel>(user);
        }
    }
}
