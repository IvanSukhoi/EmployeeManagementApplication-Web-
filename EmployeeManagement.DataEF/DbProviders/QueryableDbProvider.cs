using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Interfaces;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.DataEF.DbProviders
{
    public class QueryableDbProvider : IQueryableDbProvider
    {
        private readonly ManagementContext _managementContext;
        private readonly ILogger<QueryableDbProvider> _logger;

        public QueryableDbProvider(ManagementContext managementContext, ILogger<QueryableDbProvider> logger)
        {
            _managementContext = managementContext;
            _logger = logger;
        }

        public IQueryable<T> Set<T>() where T : class
        {
            _logger.LogInformation("Set query in database");
            var entities = _managementContext.Set<T>();
            _logger.LogInformation("Query is complete");

            return entities;
        }
    }
}
