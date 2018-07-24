using AutoMapper;
using EmployeeManagement.Domain;
using EmployeeManagement.Domain.Mappings;

namespace EmployeeManagement.WebUI.Mappings
{
    public class MapperWrapper : IMapperWrapper
    {
        public MapperWrapper()
        {
           Mapper.Initialize(c =>
           {
               DomainMapperInitializer.Initialize(c);
               UiMapperInitializer.Initialize(c);
           });
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }
    }
}