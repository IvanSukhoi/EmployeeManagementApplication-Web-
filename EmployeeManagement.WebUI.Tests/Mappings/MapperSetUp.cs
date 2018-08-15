using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Mappings.MapperWrapper;

namespace EmployeeManagement.WebUI.Tests.Mappings
{
    public class MapperSetUp
    {
        private static MapperWrapper _mapperWrapper;

        public MapperWrapper GetMapperWrapper()
        {
            return _mapperWrapper ?? (_mapperWrapper = new MapperWrapper());
        }
    }
}
