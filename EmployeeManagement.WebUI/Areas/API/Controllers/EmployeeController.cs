using System.Collections.Generic;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Interfaces;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IMapperWrapper mapperWrapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public EmployeeViewModel GetById(int id)
        {
            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(id));
        }

        [HttpGet("department/{id}")]
        public List<EmployeeViewModel> GetByDepartmentId(int id)
        {
            _logger.LogInformation("Start method GetByDepartmentId in EmployeeController");
            var employeeModels = _employeeService.GetByDepartmentId(id);

            _logger.LogInformation("Mapping employeeModel to employee view model");
            var employeeViewModels = _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(employeeModels);
            _logger.LogInformation("Mapping is complete");

            _logger.LogInformation("Method GetByDepartmentId in DepartmentController is complete");

            return employeeViewModels;
        }

        [HttpGet]
        public List<EmployeeViewModel> GetAll()
        {
            _logger.LogInformation("Start method GetAll in DepartmentController");
            var employeeViewModels = _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(_employeeService.GetAll());
            _logger.LogInformation("GetAll is complete in employee controller");

            return employeeViewModels;
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