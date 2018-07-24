using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Mappings.Profiles
{
    public class EmployeeMappingProfile: Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeViewModel>()
                .ReverseMap();

            CreateMap<EmployeeViewModel, EmployeeViewModel>();
        }
    }
}
