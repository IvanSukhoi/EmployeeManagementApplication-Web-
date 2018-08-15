using System;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Interfaces;

namespace EmployeeManagement.DataEF.DbProviders
{
    public class UpdateDbProvider : IUpdateDbProvider
    {
        private readonly ManagementContext _managementContext;

        public UpdateDbProvider(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        public void Add<T>(T entity) where T : class
        {
            if (entity == null) return;
            _managementContext.Set<T>().Add(entity);
            _managementContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            if (entity == null) return;
            _managementContext.Set<T>().Update(entity);
            _managementContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _managementContext.Set<T>().Remove(entity);
            _managementContext.SaveChanges();
        }
    }
}
