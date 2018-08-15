using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;

        public EmployeeService(IMapperWrapper mapperWrapper, IUpdateDbProvider updateDbProvider, IQueryableDbProvider queryableDbProvider)
        {
            _mapperWrapper = mapperWrapper;
            _updateDbProvider = updateDbProvider;
            _queryableDbProvider = queryableDbProvider;
        }

        public EmployeeModel GetById(int employeeId)
        {
            var employee = _queryableDbProvider.Set<Employee>().Include(x => x.Department).FirstOrDefault(x => x.Id == employeeId);

            return _mapperWrapper.Map<Employee, EmployeeModel>(employee);
        }

        public List<EmployeeModel> GetAll()
        {
            var employees = _queryableDbProvider.Set<Employee>().Include(x => x.Department).ToList();

            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(employees);
        }

        public void Create(EmployeeModel employeeModel)
        {
            var employee = _mapperWrapper.Map<EmployeeModel, Employee>(employeeModel);

            _updateDbProvider.Add(employee);
        }

        public void Delete(int id)
        {
            var dbEntry = _queryableDbProvider.Set<Employee>().Include(x => x.Department).FirstOrDefault(x => x.Id == id);

            _updateDbProvider.Delete(dbEntry);
        }

        public void Save(EmployeeModel employeeModel)
        {
            var dbEntry = _queryableDbProvider.Set<Employee>().Include(x => x.Department).FirstOrDefault(x => x.Id == employeeModel.Id);

            _mapperWrapper.Map(employeeModel, dbEntry);

            _updateDbProvider.Update(dbEntry);
        }

        public List<EmployeeModel> GetByDepartmentId(int departmentId)
        {
            var employees = _queryableDbProvider.Set<Employee>().Include(x => x.Department).Where(x => x.DepartmentId == departmentId).ToList();

            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(employees);
        }
    }
}

