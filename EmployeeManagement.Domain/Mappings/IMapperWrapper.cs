namespace EmployeeManagement.Domain.Mappings
{
    public interface IMapperWrapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
