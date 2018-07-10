using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapperWrapper _mapper;
        private readonly ManagementContext _managementContext;

        public DepartmentService(ManagementContext managementContext, IMapperWrapper mapper)
        {
            _managementContext = managementContext;
            _mapper = mapper;
        }

        public void Create(DepartmentModel departmentModel)
        {
            var department = _mapper.Map<DepartmentModel, Department>(departmentModel);
            _managementContext.Departments.Add(department);
            _managementContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var department = _managementContext.Employees.FirstOrDefault(x => x.ID == id);
            if (department != null)
            {
                _managementContext.Employees.Remove(department);
                _managementContext.SaveChanges();
            }
            else
            {
                throw new ObjectDisposedException("Object with such Id was not found");
            }
        }

        public DepartmentModel GetById(int id)
        {
            return _mapper.Map<Department, DepartmentModel>(
                _managementContext.Departments.Include("Employees").FirstOrDefault(x => x.ID == id));
        }

        public List<DepartmentModel> GetAll()
        {
            var employees = _managementContext.Departments.Include("Employees").ToList();

            return _mapper.Map<List<Department>, List<DepartmentModel>>(employees).ToList();
        }

        public void Save(DepartmentModel departmentModel)
        {
            var dbEntry = _managementContext.Departments.FirstOrDefault(x => x.ID == departmentModel.Id);

            if(dbEntry == null) return;

            _mapper.Map(departmentModel, dbEntry);
            _managementContext.SaveChanges();
        }
    }
}
