using System;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Interfaces;

namespace EmployeeManagement.DataEF.DbProviders
{
    public class QueryableDbProvider : IQueryableDbProvider
    {
        private readonly ManagementContext _managementContext;

        public QueryableDbProvider(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return _managementContext.Set<T>();
        }
    }
}
