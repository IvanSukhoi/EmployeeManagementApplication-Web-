using AutoMapper;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Models;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class EmployeeMappingTest
    {
        private readonly MapperWrapper _mapperWrapper;
        private EmployeeModel _employeeModel;
        private EmployeeViewModel _employeeViewModel;

        public EmployeeMappingTest(MapperSetUp mapperSetUp)
        {
            _mapperWrapper = mapperSetUp.GetMapperWrapper();
            Init();
        }

        [Fact]
        public void Automapper_MappAllProperties_Correct()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_ConvertFromEmployeeModel_EmployeeViewModel_Correct()
        {
            var employeeViewModel = _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeModel);

            AssertPropertyValue(_employeeModel, employeeViewModel);
        }

        [Fact]
        public void AutoMapper_ConvertFromEmployeeViewModel_EmployeeModel_Correct()
        {
            var employeeModel = _mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(_employeeViewModel);

            AssertPropertyValue(employeeModel, _employeeViewModel);
        }

        private void AssertPropertyValue(EmployeeModel employeeModel, EmployeeViewModel employeeViewModel)
        {
            Assert.Equal(employeeModel.Id, employeeViewModel.Id);
            Assert.Equal(employeeModel.FirstName, employeeViewModel.FirstName);
            Assert.Equal(employeeModel.MiddleName, employeeViewModel.MiddleName);
            Assert.Equal(employeeModel.LastName, employeeViewModel.LastName);
            Assert.Equal(employeeModel.DepartmentId, employeeViewModel.DepartmentId);
            Assert.Equal(employeeModel.ManagerId, employeeViewModel.ManagerId);
            Assert.Equal(employeeModel.Position, employeeViewModel.Position);
            Assert.Equal(employeeModel.Profession, employeeViewModel.Profession);
            Assert.Equal(employeeModel.Sex, employeeViewModel.Sex);
        }

        private void Init()
        {
            _employeeModel = new EmployeeModel
            {
                DepartmentId = 2,
                DepartmentName = "DepartmentName2",
                FirstName = "FirstName2",
                MiddleName = "MiddleName2",
                LastName = "LastName2",
                ManagerId = 2,
                Position = Position.Middle,
                Profession = Profession.Developer,
                Sex = Sex.Male,
            };

            _employeeViewModel = new EmployeeViewModel
            {
                DepartmentId = 3,
                DepartmentName = "DepartmentName3",
                FirstName = "FirstName3",
                MiddleName = "MiddleName3",
                LastName = "LastName3",
                Id = 3,
                Position = Position.Middle,
                ManagerId = 3,
                Profession = Profession.Developer,
                Sex = Sex.Male
            };
        }
    }
}
