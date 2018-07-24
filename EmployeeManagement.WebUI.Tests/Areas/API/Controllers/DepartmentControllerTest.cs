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
    public class DepartmentControllerTest
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapperWrapper _mapperWrapper;
        private List<DepartmentModel> _departmentModels;
        private readonly DepartmentController _departmentController;

        public DepartmentControllerTest()
        {
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _departmentService = A.Fake<IDepartmentService>();
            _departmentController = new DepartmentController(_departmentService, _mapperWrapper);
            Init();
        }

        [Fact]
        public void GetAll_ReturnsAllEmployeeModel_Correct()
        {
            var departmentViewModels = _departmentController.GetAll();

            Assert.Equal(2, departmentViewModels.Count);
            Assert.Equal(1, departmentViewModels.First().Id);
            Assert.Equal(2, departmentViewModels.Last().Id);
        }

        [Fact]
        public void GetById_ReturnEmployeeViewModelById_Correct()
        {
            var expectedValue = _departmentController.GetById(2);

            Assert.Equal(expectedValue.Id, _departmentModels.Last().Id);
            Assert.Equal(expectedValue.Name, _departmentModels.Last().Name);
        }

        private void Init()
        {
            _departmentModels = new List<DepartmentModel>
            {
                new DepartmentModel
                {
                    Id = 1,
                    Name = "DepartmentName1"
                },
                new DepartmentModel
                {
                    Id = 2,
                    Name = "DepartmentName2"
                }
            };

            A.CallTo(() => _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(A<DepartmentModel>.Ignored))
                .ReturnsLazily((DepartmentModel departmentModel) => new DepartmentViewModel
                {
                    Id = departmentModel.Id,
                    Name = departmentModel.Name
                });

            A.CallTo(() => _departmentService.GetAll()).Returns(_departmentModels);

            A.CallTo(() => _departmentService.GetById(A<int>.Ignored))
                .ReturnsLazily((int id) => _departmentModels.FirstOrDefault(x => x.Id == id));
        }
    }
}
