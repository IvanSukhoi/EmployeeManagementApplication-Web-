using AutoMapper;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Mappings.Profiles;
using FakeItEasy;
using Xunit;

namespace EmployeeManagement.Domain.Tests.Mappings
{
    public class DomainMapperInitializerTest
    {
        [Fact]
        public void InitializeDomainMapperInitializer_Correct()
        {
            var conf = A.Fake<IMapperConfigurationExpression>();

            DomainMapperInitializer.Initialize(conf);

            A.CallTo(() => conf.AddProfile<DepartmentMappingProfile>()).MustHaveHappenedOnceExactly();
            A.CallTo(() => conf.AddProfile<EmployeeMappingProfile>()).MustHaveHappenedOnceExactly();
            A.CallTo(() => conf.AddProfile<SettingsMappingProfile>()).MustHaveHappenedOnceExactly();
            A.CallTo(() => conf.AddProfile<UserMappingProfile>()).MustHaveHappenedOnceExactly();
        }
    }
}
