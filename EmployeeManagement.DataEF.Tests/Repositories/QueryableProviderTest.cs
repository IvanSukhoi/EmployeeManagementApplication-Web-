using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.DbProviders;
using EmployeeManagement.DataEF.Entities;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagement.DataEF.Tests.Repositories
{
    public class QueryableProviderTest
    {
        private QueryableDbProvider _queryableDbProvider;
        private ManagementContext _managementContext;

        public QueryableProviderTest()
        {
            Init();
        }

        [Fact]
        public void Get_ReturnEmployees_Corretct()
        {
           var list = _queryableDbProvider.Set<Employee>().ToList();

            //Assert.Equal(2, list.Count);
        }

        private void Init()
        {
            _managementContext = A.Fake<ManagementContext>();

            _queryableDbProvider = new QueryableDbProvider(_managementContext);
            _managementContext.Employees = A.Fake<DbSet<Employee>>();

            _managementContext.Employees.Add(new Employee{ID = 1, DepartmentID = 2});

            A.CallTo(() => _managementContext.Set<Employee>()).Returns();
        }
    
    }
}
