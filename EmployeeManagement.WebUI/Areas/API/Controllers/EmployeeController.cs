using System.Collections.Generic;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeController(IEmployeeService employeeService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
        }

        [HttpGet]
        [Route("/employee/{id}")]
        public EmployeeViewModel GetById(int id)
        {
            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(id));
        }

        [HttpGet]
        [Route("/department/{id}/employees")]
        public List<EmployeeViewModel> GetByDepartmentId(int id)
        {
            return _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(_employeeService.GetByDepartmentId(id));
        }

        [HttpGet]
        [Route("")]
        [Route("/employee")]
        public List<EmployeeViewModel> GetAll()
        {
            return _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(_employeeService.GetAll());
        }

        [HttpPost]
        [Route("/employee")]
        public EmployeeViewModel Create([FromBody]EmployeeViewModel employeeModel)
        {
            _employeeService.Create(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(employeeModel));

            return _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(employeeModel.Id));
        }

        [HttpPut]
        [Route("/employee")]
        public void Update([FromBody]EmployeeViewModel employeeModel)
        {
            _employeeService.Save(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(employeeModel));
        }

        [Route("/employee/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _employeeService.Delete(id);
        }
    }
}