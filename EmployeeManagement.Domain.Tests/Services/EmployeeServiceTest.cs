using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.Services;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Services
{
    public class EmployeeServiceTest
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;
        private readonly EmployeeService _employeeService;
        private List<Employee> _employees;

        public EmployeeServiceTest()
        {
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _queryableDbProvider = A.Fake<IQueryableDbProvider>();
            _updateDbProvider = A.Fake<IUpdateDbProvider>();
            _employeeService = new EmployeeService(_mapperWrapper, _updateDbProvider, _queryableDbProvider);

            Init();
        }

        [Fact]
        public void GetAll_ReturnsAllEmployees_Correct()
        {
            var employees = _employeeService.GetAll();

            Assert.Equal(3, employees.Count);

            Assert.Equal(1, employees.First().Id);
            Assert.Equal(3, employees.Last().Id);
        }

        [Fact]
        public void GetById_ReturnEmployeeById_Correct()
        {
            var employee = _employeeService.GetById(1);

            Assert.Equal(1, employee.Id);
            Assert.Equal(1, employee.DepartmentId);
        }

        [Fact]
        public void GetByDepartmentId_RetunrEmployeesByDepartmentId_Correct()
        {
            var employees = _employeeService.GetByDepartmentId(2);

            Assert.Equal(2, employees.Count);

            Assert.Equal(2, employees.First().Id);
            Assert.Equal(3, employees.Last().Id);
        }

        [Fact]
        public void Delete_RemoveEmploye_Correct()
        {
            _employeeService.Delete(1);

            Assert.Null(_employeeService.GetById(1));
        }

        [Fact]
        public void Create_CreateEmployee_Correct()
        {
            var employeeModel = new EmployeeModel
            {
                Id = 4,
                DepartmentId = 4
            };

            _employeeService.Create(employeeModel);

            Assert.Equal(4, _employees.Count);
            Assert.Equal(4, _employees.Last().Id);
            Assert.Equal(4, _employees.Last().DepartmentId);
        }

        [Fact]
        public void Save_SaveEmployee_Correcr()
        {
            var employeeModel = new EmployeeModel
            {
                Id = 1,
                DepartmentId = 2
            };

            _employeeService.Save(employeeModel);

            Assert.Equal(2, _employees.First().DepartmentId);
        }

        private void Init()
        {
            _employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 2
                }
            };

            A.CallTo(() => _mapperWrapper.Map<Employee, EmployeeModel>(A<Employee>.That.Matches(x => x != null))).ReturnsLazily(
                (Employee employee) => new EmployeeModel
                {
                    Id = employee.Id,
                    DepartmentId = employee.DepartmentId
                });

            A.CallTo(() => _mapperWrapper.Map<Employee, EmployeeModel>(null)).Returns(null);

            A.CallTo(() =>
                    _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(A<List<Employee>>.Ignored))
                .ReturnsLazily(
                    (List<Employee> employees) =>
                    {
                        var list = new List<EmployeeModel>();
                        employees.ForEach(x => list.Add(_mapperWrapper.Map<Employee, EmployeeModel>(x)));

                        return list;
                    });

            A.CallTo(() => _mapperWrapper.Map<EmployeeModel, Employee>(A<EmployeeModel>.Ignored))
                .ReturnsLazily((EmployeeModel employeeModel) => new Employee
                {
                    Id = employeeModel.Id,
                    DepartmentId = employeeModel.Id
                });

            A.CallTo(() => _mapperWrapper.Map(A<EmployeeModel>.Ignored, A<Employee>.Ignored)).Invokes(
                (EmployeeModel employeeModel, Employee employee) =>
                {
                    employee.Id = employeeModel.Id;
                    employee.DepartmentId = employeeModel.DepartmentId;
                });

            A.CallTo(() => _queryableDbProvider.Set<Employee>()).Returns(_employees.AsQueryable());

            A.CallTo(() => _updateDbProvider.Delete(A<Employee>.Ignored)).Invokes((Employee employee) =>
            {
                _employees.Remove(employee);
            });

            A.CallTo(() => _updateDbProvider.Add(A<Employee>.Ignored)).Invokes((Employee employee) =>
            {
                _employees.Add(employee);
            });

            A.CallTo(() => _updateDbProvider.Update(A<Employee>.Ignored)).Invokes((Employee employee) =>
            {
                var entity = _employees.FirstOrDefault(x => x.Id == employee.Id);
                if (entity != null) entity.DepartmentId = employee.DepartmentId;
            });
        }
    }
}
