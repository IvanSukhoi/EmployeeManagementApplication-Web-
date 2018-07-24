using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Mappings.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.SettingsModel, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, User>();
            CreateMap<UserModel, UserModel>();
        }
    }
}
