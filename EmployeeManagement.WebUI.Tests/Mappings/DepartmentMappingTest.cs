using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Models;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class DepartmentMappingTest
    {
        private readonly MapperWrapper _mapperWrapper;
        private DepartmentModel _departmentModel;
        private DepartmentViewModel _departmentViewModel;

        public DepartmentMappingTest(MapperSetUp mapperSetUp)
        {
            _mapperWrapper = mapperSetUp.GetMapperWrapper();

            Init();
        }

        [Fact]
        public void AutoMapper_ConvertFromDepartmentModel_DepartmentViewModel_Correct()
        {
            var departmentViewModel = _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(_departmentModel);

            AssertPropertyValue(_departmentModel, departmentViewModel);
        }

        [Fact]
        public void AutoMapper_ConvertFromDepartmentViewModel_DepartmentModel_Correct()
        {
            var departmentModel = _mapperWrapper.Map<DepartmentViewModel, DepartmentModel>(_departmentViewModel);

            AssertPropertyValue(departmentModel, _departmentViewModel);
        }

        private void AssertPropertyValue(DepartmentModel departmentModel, DepartmentViewModel departmentViewModel)
        {
            Assert.Equal(departmentModel.Id, departmentViewModel.Id);
            Assert.Equal(departmentModel.Name, departmentViewModel.Name);
        }

        private void Init()
        {
            _departmentModel = new DepartmentModel
            {
                Id = 1,
                Name = "DepartmentName1"
            };

            _departmentViewModel = new DepartmentViewModel
            {
                Id = 2,
                Name = "DepartmentName2"
            };
        }
    }
}
