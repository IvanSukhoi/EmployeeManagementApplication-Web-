using System.Collections.Generic;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
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
