using AutoMapper;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Mappings.Profiles;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.WebUI.Tests.Mappings
{
    public class UiMapperInitializerTest
    {
        [Fact]
        public void InitializeDomainMapperInitializer_Correct()
        {
            var conf = A.Fake<IMapperConfigurationExpression>();

            UiMapperInitializer.Initialize(conf);

            A.CallTo(() => conf.AddProfile<DepartmentMappingProfile>()).MustHaveHappenedOnceExactly();
            A.CallTo(() => conf.AddProfile<EmployeeMappingProfile>()).MustHaveHappenedOnceExactly();
        }
    }
}
