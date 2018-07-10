using System;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Mappings;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ManagementContext _managementContext;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public EmployeeModel GetById(int employeeId)
        {
            var employee = _managementContext.Employees.FirstOrDefault(x => x.ID == employeeId);

            return _mapperWrapper.Map<Employee, EmployeeModel>(employee);
        }

        public List<EmployeeModel> GetAll()
        {
            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(_managementContext.Employees.Include("Department").ToList());
        }

        public void Create(EmployeeModel employeeModel)
        {
            var employee = _mapperWrapper.Map<EmployeeModel, Employee>(employeeModel);

            _managementContext.Employees.Add(employee);
            _managementContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _managementContext.Employees.FirstOrDefault(x => x.ID == id);
            if (employee != null)
            {
                _managementContext.Employees.Remove(employee);
                _managementContext.SaveChanges();
            }
            else
            {
                throw new ObjectDisposedException("Object with such Id was not found");
            }
        }

        public List<EmployeeModel> GetByManagerId(int managerId)
        {
            throw new NotImplementedException();
        }

        public void Save(EmployeeModel employeeModel)
        {
            var dbEntry = _managementContext.Employees.FirstOrDefault(x => x.ID == employeeModel.Id);

            if (dbEntry == null) return;

             _mapperWrapper.Map(employeeModel, dbEntry);

            _managementContext.Entry(dbEntry).Reference(x => x.Department).Load();
            _managementContext.SaveChanges();
        }


        public List<EmployeeModel> GetByDepartmentId(int departmentId)
        {
            var employees = _managementContext.Employees.Include("Department").Where(x => x.DepartmentID == departmentId).ToList();

            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(employees);
        }
    }
}
