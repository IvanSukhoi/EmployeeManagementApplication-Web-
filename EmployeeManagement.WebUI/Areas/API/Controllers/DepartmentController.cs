using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Areas.API.Controllers
{
    public class DepartmentController : ApiController
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

        [HttpGet]
        public DepartmentViewModel GetById(int id)
        {
            return _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(_departmentService.GetById(id));
        }
    }
}
