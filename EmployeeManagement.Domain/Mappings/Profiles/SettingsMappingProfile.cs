using AutoMapper;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Mappings
{
    public class SettingsMappingProfile : Profile
    {
        public SettingsMappingProfile()
        {
            CreateMap<Settings, SettingsModel>()
                .ForMember(s => s.UserId, opt => opt.MapFrom(c => c.UserID))
                .ForMember(s => s.Theme, opt => opt.MapFrom(c => c.Topic))
                .ReverseMap();

            CreateMap<Settings, Settings>();
            CreateMap<SettingsModel, SettingsModel>();
        }
    }
}
