using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Mappings.Profiles
{
    public class DepartmentMappingProfile: Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<DepartmentModel, DepartmentViewModel>()
                .ReverseMap();
        }
    }
}
