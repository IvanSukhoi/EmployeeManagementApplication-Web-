using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    [CollectionDefinition("MapperCollection")]
    public class CollectionClass : ICollectionFixture<MapperSetUp>
    {}
}
