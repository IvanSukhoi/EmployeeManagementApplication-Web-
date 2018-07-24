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
    public class DepartmentServiceTest
    {
        private readonly IMapperWrapper _mapperWrapper;
        private readonly IQueryableDbProvider _queryableDbProvider;
        private readonly IUpdateDbProvider _updateDbProvider;
        private readonly DepartmentService _departmentService;
        private List<Department> _departments;

        public DepartmentServiceTest()
        {
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _queryableDbProvider = A.Fake<IQueryableDbProvider>();
            _updateDbProvider = A.Fake<IUpdateDbProvider>();
            _departmentService = new DepartmentService(_mapperWrapper, _queryableDbProvider, _updateDbProvider);

            Init();
        }

        [Fact]
        public void GetAll_ReturnAllDepartments_Correct()
        {
            var departments = _departmentService.GetAll();

            Assert.Equal(3, departments.Count);

            Assert.Equal(1, departments.First().Id);
            Assert.Equal(3, departments.Last().Id);
        }

        [Fact]
        public void GetByid_ReturnDepartmentById_Correct()
        {
            var department = _departmentService.GetById(1);

            Assert.Equal(1, department.Id);
            Assert.Equal("DepartmentName1", department.Name);
        }

        [Fact]
        public void Delete_RemoveDepartment_Correct()
        {
            _departmentService.Delete(1);

            Assert.Null(_departmentService.GetById(1));
        }

        [Fact]
        public void Create_CreateDepartment_Correct()
        {
            var departmentModel = new DepartmentModel()
            {
                Id = 4,
                Name = "DepartmentName4"
            };

            _departmentService.Create(departmentModel);

            Assert.Equal(4, _departments.Count);
            Assert.Equal(4, _departments.Last().ID);
            Assert.Equal("DepartmentName4", _departments.Last().Name);
        }

        [Fact]
        public void Save_SaveDepartment_Correct()
        {
            var departmentName = new DepartmentModel()
            {
                Id = 1,
                Name = "DepartmentName50"
            };

            _departmentService.Save(departmentName);

            Assert.Equal("DepartmentName50", _departments.First().Name);
        }

        private void Init()
        {
            _departments = new List<Department>
            {
                new Department
                {
                    ID = 1,
                    Name = "DepartmentName1"
                },
                new Department
                {
                    ID = 2,
                    Name = "DepartmentName2"
                },
                new Department
                {
                    ID = 3,
                    Name = "DepartmentName3"
                }
            };

            A.CallTo(() => _mapperWrapper.Map<Department, DepartmentModel>(A<Department>.That.Matches(x => x != null)))
                .ReturnsLazily(
                    (Department department) => new DepartmentModel
                    {
                        Id = department.ID,
                        Name = department.Name
                    });

            A.CallTo(() => _mapperWrapper.Map<Department, DepartmentModel>(null)).Returns(null);

            A.CallTo(() =>
                    _mapperWrapper.Map<List<Department>, List<DepartmentModel>>(A<List<Department>>.Ignored))
                .ReturnsLazily(
                    (List<Department> departments) =>
                    {
                        var list = new List<DepartmentModel>();
                        departments.ForEach(x => list.Add(_mapperWrapper.Map<Department, DepartmentModel>(x)));

                        return list;
                    });

            A.CallTo(() => _queryableDbProvider.Set<Department>()).Returns(_departments.AsQueryable());

            A.CallTo(() => _mapperWrapper.Map<DepartmentModel, Department>(A<DepartmentModel>.Ignored))
                .ReturnsLazily((DepartmentModel departmentModel) => new Department
                {
                    ID = departmentModel.Id,
                    Name = departmentModel.Name
                });

            A.CallTo(() => _mapperWrapper.Map(A<DepartmentModel>.Ignored, A<Department>.Ignored)).Invokes(
                (DepartmentModel employeeModel, Department employee) =>
                {
                    employee.ID = employeeModel.Id;
                    employee.Name = employeeModel.Name;
                });

            A.CallTo(() => _updateDbProvider.Delete(A<Department>.Ignored)).Invokes((Department department) =>
            {
                _departments.Remove(department);
            });

            A.CallTo(() => _updateDbProvider.Add(A<Department>.Ignored)).Invokes((Department department) =>
            {
                _departments.Add(department);
            });

            A.CallTo(() => _updateDbProvider.Update(A<Department>.Ignored)).Invokes((Department department) =>
            {
                var entity = _departments.FirstOrDefault(x => x.ID == department.ID);
                if (entity != null) entity.Name = department.Name;
            });
        }
    }
}