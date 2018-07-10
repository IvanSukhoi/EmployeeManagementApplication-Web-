using System.Collections.Generic;
using System.Web.Http;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeController(IEmployeeService employeeService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
        }

        [HttpGet]
        public EmployeeViewModel GetById(int id)
        {
            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(id));
        }

        [HttpGet]
        public List<EmployeeViewModel> GetByDepartmentId(int id)
        {
            return _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(_employeeService.GetByDepartmentId(id));
        }

        [HttpGet]
        public List<EmployeeViewModel> GetAll()
        {
            return _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(_employeeService.GetAll());
        }

        [HttpPost]
        public EmployeeViewModel Create(EmployeeViewModel employeeModel)
        {
            _employeeService.Create(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(employeeModel));

            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(employeeModel.Id));
        }

        [HttpPut]
        public void Update(EmployeeViewModel employeeModel)
        {
            _employeeService.Save(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel > (employeeModel));
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _employeeService.Delete(id);
        }
    }
}