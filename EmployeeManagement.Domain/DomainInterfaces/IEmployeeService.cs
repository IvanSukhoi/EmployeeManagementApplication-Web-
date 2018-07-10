using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetAll();
        EmployeeModel GetById(int employeeId);
        void Save(EmployeeModel employeeModel);
        void Create(EmployeeModel employeeModel);
        void Delete(int id);
        List<EmployeeModel> GetByManagerId(int managerId);
        List<EmployeeModel> GetByDepartmentId(int departmentId);
    }
}
