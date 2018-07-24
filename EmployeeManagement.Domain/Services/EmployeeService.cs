using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

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
            var employee = _queryableDbProvider.Set<Employee>().FirstOrDefault(x => x.ID == employeeId);

            return _mapperWrapper.Map<Employee, EmployeeModel>(employee);
        }

        public List<EmployeeModel> GetAll()
        {
            var departments = _queryableDbProvider.Set<Employee>().ToList();

            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(departments);
        }

        public void Create(EmployeeModel employeeModel)
        {
            var employee = _mapperWrapper.Map<EmployeeModel, Employee>(employeeModel);

            _updateDbProvider.Add(employee);
        }

        public void Delete(int id)
        {
            var dbEntry = _queryableDbProvider.Set<Employee>().FirstOrDefault(x => x.ID == id);

            _updateDbProvider.Delete(dbEntry);
        }

        public void Save(EmployeeModel employeeModel)
        {
            var dbEntry = _queryableDbProvider.Set<Employee>().FirstOrDefault(x => x.ID == employeeModel.Id);

            _mapperWrapper.Map(employeeModel, dbEntry);

            _updateDbProvider.Update(dbEntry);
        }

        public List<EmployeeModel> GetByDepartmentId(int departmentId)
        {
            var employees = _queryableDbProvider.Set<Employee>().Where(x => x.DepartmentID == departmentId).ToList();

            return _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(employees);
        }
    }
}

