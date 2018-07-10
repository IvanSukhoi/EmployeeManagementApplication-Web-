using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.Domain.DataInterfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee Get(int employeeId);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(int employeeId);
        IEnumerable<Employee> GetByManagerId(int managerId);
        IEnumerable<Employee> GetByDepartmentId(int departmentId);
    }
}
