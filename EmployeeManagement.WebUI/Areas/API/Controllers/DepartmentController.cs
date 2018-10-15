using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("department")]
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapperWrapper _mapperWrapper;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, IMapperWrapper mapperWrapper, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
            _logger = logger;
        }

        [HttpGet]
        public List<DepartmentViewModel> GetAll()
        {
            _logger.LogInformation("Start method GetAllAsync in DepartmentController");
            var departmentModels = _departmentService.GetAll();
            _logger.LogInformation("Map department model to department view model");    
            var models = _mapperWrapper.Map<List<DepartmentModel>, List<DepartmentViewModel>>(departmentModels).ToList();
            _logger.LogInformation("Mappings is complete");
            _logger.LogInformation("Method GetAllAsync in DepartmentController is complete");

            return models;
        }

        [HttpGet("{id}")]
        public DepartmentViewModel GetById(int id)
        {
            return _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(_departmentService.GetById(id));
        }
    }
}
