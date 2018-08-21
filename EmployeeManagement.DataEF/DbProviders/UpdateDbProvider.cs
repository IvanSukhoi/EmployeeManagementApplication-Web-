using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Interfaces;
using Microsoft.EntityFrameworkCore;

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


        //public async Task AddOrUpdate<T>(T entity) where T : class
        //{
        //    if (entity == null) return;
        //    {
        //        await _managementContext.AddOrUpdate(entity);
        //        await _managementContext.SaveChangesAsync();
        //    }
        //}

        public void Delete<T>(T entity) where T : class
        {
            _managementContext.Set<T>().Remove(entity);
            _managementContext.SaveChanges();
        }
    }

    //public static class ContextExtensions
    //{
    //    public static async Task AddOrUpdate<T>(this DbContext context, T entity) where T : class 
    //    {
    //        var dbEntry = context.Entry(entity);
    //        switch (dbEntry.State)
    //        {
    //            case EntityState.Detached:
    //                await context.AddAsync(entity);
    //                break;
    //            case EntityState.Modified:
    //                context.Update(entity);
    //                break;
    //            case EntityState.Added:
    //                await context.AddAsync(entity);
    //                break;
    //            case EntityState.Unchanged:
    //                //item already in db no need to do anything  
    //                break;

    //            default:
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }
    //}
}
