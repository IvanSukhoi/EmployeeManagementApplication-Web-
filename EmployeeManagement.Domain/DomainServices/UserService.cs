using System.Linq;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using UserModel = EmployeeManagement.Domain.Models.UserModel;

namespace EmployeeManagement.Domain.DomainServices
{
    public class UserService : IUserService
    {
        private readonly ManagementContext _managementContext;
        private readonly IMapperWrapper _mapperWrapper;

        public UserService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public UserModel GetUser(string login, string password)
        {
            var user = _managementContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            return _mapperWrapper.Map<User, UserModel>(user);
        }
    }
}
