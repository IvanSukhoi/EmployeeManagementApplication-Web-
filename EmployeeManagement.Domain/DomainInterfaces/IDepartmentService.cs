using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IDepartmentService
    {
        List<DepartmentModel> GetAll();
        DepartmentModel GetById(int id);
        void Create(DepartmentModel departmentModel);
        void Save(DepartmentModel departmentModel);
        void Delete(int id);
    }
}
