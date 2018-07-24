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
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;

        public DepartmentService(IMapperWrapper mapperWrapper, IQueryableDbProvider queryableDbProvider, IUpdateDbProvider updateDbProvider)
        {
            _mapperWrapper = mapperWrapper;
            _queryableDbProvider = queryableDbProvider;
            _updateDbProvider = updateDbProvider;
        }

        public void Create(DepartmentModel departmentModel)
        {
            var department = _mapperWrapper.Map<DepartmentModel, Department>(departmentModel);

            _updateDbProvider.Add(department);
        }

        public void Delete(int id)
        {
            var dbEntry = _queryableDbProvider.Set<Department>().FirstOrDefault(x => x.ID == id);

            _updateDbProvider.Delete(dbEntry);
        }

        public DepartmentModel GetById(int id)
        {
            var department = _queryableDbProvider.Set<Department>().Include("Employees").FirstOrDefault(x => x.ID == id);

            return _mapperWrapper.Map<Department, DepartmentModel>(department);
        }

        public List<DepartmentModel> GetAll()
        {
            var departments = _queryableDbProvider.Set<Department>().Include("Employees").ToList();

            return _mapperWrapper.Map<List<Department>, List<DepartmentModel>>(departments).ToList();
        }

        public void Save(DepartmentModel departmentModel)
        {
            var dbEntry = _queryableDbProvider.Set<Department>().FirstOrDefault(x => x.ID == departmentModel.Id);

            _mapperWrapper.Map(departmentModel, dbEntry);
            _updateDbProvider.Update(dbEntry);
        }
    }
}
