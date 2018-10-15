using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Domain.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;

        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IMapperWrapper mapperWrapper, IQueryableDbProvider queryableDbProvider, IUpdateDbProvider updateDbProvider, ILogger<DepartmentService> logger)
        {
            _mapperWrapper = mapperWrapper;
            _queryableDbProvider = queryableDbProvider;
            _updateDbProvider = updateDbProvider;
            _logger = logger;
        }

        public void Create(DepartmentModel departmentModel)
        {
            var department = _mapperWrapper.Map<DepartmentModel, Department>(departmentModel);

            _updateDbProvider.Add(department);
        }

        public void Delete(int id)
        {
            var dbEntry = _queryableDbProvider.Set<Department>().Include(x => x.Employees).FirstOrDefault(x => x.Id == id);

            _updateDbProvider.Delete(dbEntry);
        }

        public DepartmentModel GetById(int id)
        {
            var department = _queryableDbProvider.Set<Department>().Include(x => x.Employees).FirstOrDefault(x => x.Id == id);

            return _mapperWrapper.Map<Department, DepartmentModel>(department);
        }

        public List<DepartmentModel> GetAll()
        {
            _logger.LogInformation("Start method GetAll in department service");
            var departments = _queryableDbProvider.Set<Department>().Include(x => x.Employees).ToList();
            _logger.LogInformation("Map department to department model");
            var departmentModels = _mapperWrapper.Map<List<Department>, List<DepartmentModel>>(departments).ToList();
            _logger.LogInformation("Mapping is complete");
            _logger.LogInformation("Method GetAll in department service is complete");

            return departmentModels;
        }

        public void Save(DepartmentModel departmentModel)
        {
            var dbEntry = _queryableDbProvider.Set<Department>().Include(x => x.Employees).FirstOrDefault(x => x.Id == departmentModel.Id);

            _mapperWrapper.Map(departmentModel, dbEntry);
            _updateDbProvider.Update(dbEntry);
        }
    }
}
