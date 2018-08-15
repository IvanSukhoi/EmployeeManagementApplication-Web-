using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Areas.API.Filters;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("department")]
    [LoggingFilter]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapperWrapper _mapperWrapper;

        public DepartmentController(IDepartmentService departmentService, IMapperWrapper mapperWrapper)
        {
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
        }

        [HttpGet]
        public List<DepartmentViewModel> GetAll()
        {
            var departmentModels = _departmentService.GetAll().Select(x =>  _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(x)).ToList();

            return departmentModels;
        }

        [HttpGet("{id}")]
        public DepartmentViewModel GetById(int id)
        {
            return _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(_departmentService.GetById(id));
        }
    }
}
