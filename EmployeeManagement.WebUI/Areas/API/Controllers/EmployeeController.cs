using System.Collections.Generic;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Filters;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("employee")]
    [LoggingFilter]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeController(IEmployeeService employeeService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
        }

        [HttpGet("{id}")]
        public EmployeeViewModel GetById(int id)
        {
            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(id));
        }

        [HttpGet("department/{id}")]
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
        public EmployeeViewModel Create([FromBody]EmployeeViewModel employeeModel)
        {
            _employeeService.Create(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(employeeModel));

            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(employeeModel.Id));
        }

        [HttpPut]
        public void Update([FromBody]EmployeeViewModel employeeModel)
        {
            _employeeService.Save(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(employeeModel));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeService.Delete(id);
        }
    }
}