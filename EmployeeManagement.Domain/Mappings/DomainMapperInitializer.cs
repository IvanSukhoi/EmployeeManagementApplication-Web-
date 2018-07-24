using AutoMapper;
using EmployeeManagement.Domain.Mappings.Profiles;

namespace EmployeeManagement.Domain.Mappings
{
    public static class DomainMapperInitializer
    {
        public static void Initialize(IMapperConfigurationExpression config)
        {
            config.AddProfile<DepartmentMappingProfile>();
            config.AddProfile<EmployeeMappingProfile>();
            config.AddProfile<SettingsMappingProfile>();
            config.AddProfile<UserMappingProfile>();
        }
    }
}
