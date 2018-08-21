using AutoMapper;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings.MapperWrapper;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    [Collection("MapperCollection")]
    public class EmployeeMappingTest
    {
        private readonly MapperWrapper _mapperWrapper;
        private EmployeeModel _employeeModel;
        private Employee _employee;

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
        public void AutoMapper_ConvertFromEmployee_EmmployeeModel_Correct()
        {
            var employeeModel = _mapperWrapper.Map<Employee, EmployeeModel>(_employee);

            AssertPropertyValue(_employee, employeeModel);
        }

        [Fact]
        public void AutoMapper_ConvertFromEmployeeModel_Employee_Correct()
        {
            var employee = _mapperWrapper.Map<EmployeeModel, Employee>(_employeeModel);

            AssertPropertyValue(employee, _employeeModel);
        }

        [Fact]
        public void AutoMapper_CopyFromEmployee_EmployeeModel_Correct()
        {
            var employeeModel = new EmployeeModel();

            _mapperWrapper.Map(_employee, employeeModel);

            AssertPropertyValue(_employee, employeeModel);
        }

        [Fact]
        public void AutoMapper_CopyFromEmployeeModel_Employee_Correct()
        {
            var employee = new Employee();

            _mapperWrapper.Map(_employeeModel, employee);

            AssertPropertyValue(employee, _employeeModel);
        }

        private void AssertPropertyValue(Employee employee, EmployeeModel employeeModel)
        {
            Assert.Equal(employee.Id, employeeModel.Id);
            Assert.Equal(employee.FirstName, employeeModel.FirstName);
            Assert.Equal(employee.MiddleName, employeeModel.MiddleName);
            Assert.Equal(employee.LastName, employeeModel.LastName);
            Assert.Equal(employee.DepartmentId, employeeModel.DepartmentId);
            Assert.Equal(employee.ManagerId, employeeModel.ManagerId);
            Assert.Equal(employee.Position, employeeModel.Position);
            Assert.Equal(employee.Profession, employeeModel.Profession);
            Assert.Equal(employee.Sex, employeeModel.Sex);
        }

        private void Init()
        {
            _employee = new Employee
            {
                Id = 4,
                FirstName = "FirstName4",
                MiddleName = "MiddleName4",
                LastName = "LastName4",
                DepartmentId = 4,
                ManagerId= 4,
                Position = Position.Intern,
                Profession = Profession.Developer,
                Sex = Sex.Female,
                Department = new Department
                {
                    Id = 4,
                    Name = "DepartmentName3"
                }
            };

            _employeeModel = new EmployeeModel
            {
                DepartmentId = 2,
                DepartmentName = "DepartmentName2",
                FirstName = "FirstName2",
                MiddleName = "MiddleName2",
                LastName = "LastName2",
                ManagerId = 2,
                Position = Position.Middle,
                Profession = Profession.Bookkeeper,
                Sex = Sex.Male,
            };
        }
    }
}
