using System.Data.Entity;
using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.DataEF.DAL
{
    public class ManagementContext : DbContext
    {
        public ManagementContext() : base("name=ManagementContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
