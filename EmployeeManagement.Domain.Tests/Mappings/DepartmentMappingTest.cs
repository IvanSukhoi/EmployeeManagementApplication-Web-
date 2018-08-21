using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings.MapperWrapper;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class DepartmentMappingTest 
    {
        private readonly MapperWrapper _mapperWrapper;
        private DepartmentModel _departmentModel;
        private Department _department;

        public DepartmentMappingTest(MapperSetUp mapperSetUp)
        {
            _mapperWrapper = mapperSetUp.GetMapperWrapper();
            Init();
        }

        [Fact]
        public void AutoMapper_ConvertFromDepartment_DepartmentModel_Correct()
        {
            var departmentModel = _mapperWrapper.Map<Department, DepartmentModel>(_department);

            AssertPropertyValue(_department, departmentModel);
        }

        [Fact]
        public void AutoMapper_ConvertFromDepartmentModel_Department_Correct()
        {
            var department = _mapperWrapper.Map<DepartmentModel, Department>(_departmentModel);

            AssertPropertyValue(department, _departmentModel);
        }

        [Fact]
        public void AutoMapper_CopyFromDepartmentModel_Department_Correct()
        {
            var department = new Department();

            _mapperWrapper.Map(_departmentModel, department);

            AssertPropertyValue(department, _departmentModel);
        }

        [Fact]
        public void AutoMapper_CopyFromDepartment_DepartmentModel_Correct()
        {
            var departmentModel = new DepartmentModel();

            _mapperWrapper.Map(_department, departmentModel);

            AssertPropertyValue(_department, departmentModel);
        }

        private void AssertPropertyValue(Department department, DepartmentModel departmentModel)
        {
            Assert.Equal(department.Id, departmentModel.Id);
            Assert.Equal(department.Name, departmentModel.Name);
        }

        private void Init()
        {
            _department = new Department
            {
                Id = 1,
                Name = "DepartmentName1"
            };

            _departmentModel = new DepartmentModel
            {
                Id = 2,
                Name = "DepartmentName2"
            };
        }
    }
}
