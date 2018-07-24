using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Controllers;
using EmployeeManagement.WebUI.Models;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Areas.API.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;
        private List<EmployeeModel> _employeeModels;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTest()
        {
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _employeeService = A.Fake<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeService, _mapperWrapper);
            Init();
        }

        [Fact]
        public void GetAll_ReturnAllEmployees_Correct()
        {
            var expectedValue = _employeeController.GetAll();

            Assert.Equal(3, expectedValue.Count);

            Assert.Equal(1, expectedValue.First().Id);
            Assert.Equal(3, expectedValue.Last().Id);
        }

        [Fact]
        public void GetByDepartmentId_ReturnEmployeesByDepartmentId_Correct()
        {
            var expectedValue = _employeeController.GetByDepartmentId(1);

            Assert.Equal(2, expectedValue.Count);

            Assert.Equal(1, expectedValue.First().Id);
            Assert.Equal(2, expectedValue.Last().Id);
        }

        [Fact]
        public void GetById_ReturnEmployeeById_Correct()
        {
            var expectedValue = _employeeController.GetById(1);

            Assert.Equal(1, expectedValue.Id);
            Assert.Equal(1, expectedValue.DepartmentId);
        }

        [Fact]
        public void Create_AddNewEmployee_Correct()
        {
            var expectedValue = _employeeController.Create(new EmployeeViewModel
            {
                Id = 50,
                DepartmentId = 50
            });

            Assert.Equal(50, expectedValue.Id);
            Assert.Equal(50, expectedValue.DepartmentId);
        }

        [Fact]
        public void Delete_RemoveEmployee_Correct()
        {
            _employeeController.Delete(10);

            A.CallTo(() => _employeeService.Delete(10)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_UpdateEmployee_Correct()
        {
            var employee = new EmployeeViewModel
            {
                DepartmentId = 2,
                Id = 1
            };

            _employeeController.Update(employee);

            Assert.Equal(2, _employeeModels.First().DepartmentId);
        }

        private void Init()
        {
            _employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel
                {
                    DepartmentId = 1,
                    Id = 1
                },
                new EmployeeModel
                {
                    DepartmentId = 1,
                    Id = 2
                },
                new EmployeeModel
                {
                    DepartmentId = 2,
                    Id = 3
                }
            };

            A.CallTo(() => _employeeService.Save(A<EmployeeModel>.Ignored))
                .Invokes((EmployeeModel employeeModel) =>
                {
                    var department = _employeeModels.FirstOrDefault(x => x.Id == employeeModel.Id);

                    if (department != null)
                    {
                        department.DepartmentId = employeeModel.DepartmentId;
                    }
                });

            A.CallTo(() => _employeeService.Create(A<EmployeeModel>.Ignored)).Invokes((EmployeeModel employeeModel) =>
                _employeeModels.Add(employeeModel));

            A.CallTo(() => _employeeService.GetAll()).Returns(_employeeModels);

            A.CallTo(() => _employeeService.GetByDepartmentId(A<int>.Ignored)).ReturnsLazily((int departmentId) =>
                _employeeModels.Where(x => x.DepartmentId == departmentId).ToList());

            A.CallTo(() => _employeeService.GetById(A<int>.Ignored))
                .ReturnsLazily((int id) => _employeeModels.FirstOrDefault(x => x.Id == id));

            A.CallTo(() =>
                    _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(A<List<EmployeeModel>>.Ignored))
                .ReturnsLazily(
                    (List<EmployeeModel> employeeModels) =>
                    {
                        var list = new List<EmployeeViewModel>();
                        employeeModels.ForEach(x => list.Add(_mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(x)));

                        return list;
                    });

            A.CallTo(() => _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(A<EmployeeModel>.Ignored))
                .ReturnsLazily((EmployeeModel employeeModel) => new EmployeeViewModel
                {
                    DepartmentId = employeeModel.DepartmentId,
                    Id = employeeModel.Id
                });

            A.CallTo(() => _mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(A<EmployeeViewModel>.Ignored))
                .ReturnsLazily((EmployeeViewModel employeeModel) => new EmployeeModel
                {
                    DepartmentId = employeeModel.DepartmentId,
                    Id = employeeModel.Id
                });
        }
    }
}
