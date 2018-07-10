using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;
using AutoMapper;
using EmployeeManagement.DataEF;

namespace EmployeeManagement.Domain.Mappings
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Department, DepartmentModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.QuantityEmployees, opt => opt.MapFrom(c => c.Employees.Count));

            CreateMap<DepartmentModel, Department>();

            CreateMap<User, UserModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ReverseMap();

            CreateMap<Settings, SettingsModel>()
                .ForMember(s => s.UserId, opt => opt.MapFrom(c => c.UserID))
                .ReverseMap();

            CreateMap<Employee, EmployeeModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.DepartmentId, opt => opt.MapFrom(c => c.DepartmentID))
                .ForMember(s => s.ManagerId, opt => opt.MapFrom(c => c.ManagerID))
                .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName))
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name));

            CreateMap<EmployeeModel, Employee>()
                .ForMember(s => s.ID, opt => opt.MapFrom(c => c.Id))
                .ForMember(s => s.DepartmentID, opt => opt.MapFrom(c => c.DepartmentId))
                .ForMember(s => s.ManagerID, opt => opt.MapFrom(c => c.ManagerId))
                .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName));

            CreateMap<Employee, Employee>();
            CreateMap<Department, Department>();
            CreateMap<Settings, Settings>();
        }
    }
}
