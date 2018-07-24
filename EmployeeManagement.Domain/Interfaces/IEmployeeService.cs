using System.Collections.Generic;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetAll();
        EmployeeModel GetById(int employeeId);
        void Save(EmployeeModel employeeModel);
        void Create(EmployeeModel employeeModel);
        void Delete(int id);
        List<EmployeeModel> GetByDepartmentId(int departmentId);
    }
}
