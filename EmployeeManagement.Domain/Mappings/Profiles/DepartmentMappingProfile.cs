using AutoMapper;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Mappings.Profiles
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.QuantityEmployees, opt => opt.MapFrom(c => c.Employees.Count));

            CreateMap<DepartmentModel, Department>()
                .ForMember(s => s.Employees, opt => opt.Ignore());

            CreateMap<Department, Department>();
            CreateMap<DepartmentModel, DepartmentModel>();
        }
    }
}
