using AutoMapper;
using EmployeeManagement.Domain.Mappings;

namespace EmployeeManagement.WebUI.Mappings.MapperWrapper
{
    public class MapperWrapper : IMapperWrapper
    {
        public static bool IsInitialized;
        private static readonly object Locker = new object();

        public MapperWrapper()
        {
            lock (Locker)
            {
                if (IsInitialized) return;
                Mapper.Initialize(c =>
                {
                    DomainMapperInitializer.Initialize(c);
                    UiMapperInitializer.Initialize(c);
                });

                IsInitialized = true;
            }
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