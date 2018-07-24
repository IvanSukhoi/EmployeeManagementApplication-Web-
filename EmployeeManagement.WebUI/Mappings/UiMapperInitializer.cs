using AutoMapper;
using EmployeeManagement.WebUI.Mappings.Profiles;

namespace EmployeeManagement.WebUI.Mappings
{
    public static class UiMapperInitializer
    {
        public static void Initialize(IMapperConfigurationExpression config)
        {
            config.AddProfile<DepartmentMappingProfile>();
            config.AddProfile<EmployeeMappingProfile>();
        }
    }
}
