using AutoMapper;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Mappings.Profiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(s => s.DepartmentId, opt => opt.MapFrom(c => c.DepartmentId))
                .ForMember(s => s.ManagerId, opt => opt.MapFrom(c => c.ManagerId))
                .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name));

            CreateMap<EmployeeModel, Employee>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(s => s.DepartmentId, opt => opt.MapFrom(c => c.DepartmentId))
                .ForMember(s => s.ManagerId, opt => opt.MapFrom(c => c.ManagerId))
                .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                .ForMember(s => s.Department, opt => opt.Ignore());

            CreateMap<Employee, Employee>();
            CreateMap<EmployeeModel, EmployeeModel>();
        }
    }
}
