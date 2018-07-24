using EmployeeManagement.DataEF.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataEF.DAL
{
    public class ManagementContext : DbContext
    {
        public ManagementContext() : base()
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EmployeeManagmentApp;Integrated Security=True");
        }
    }
}
