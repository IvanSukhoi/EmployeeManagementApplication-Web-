using EmployeeManagement.WebUI.Mappings;

namespace EmployeeManagement.Domain.Tests.Mappings
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
