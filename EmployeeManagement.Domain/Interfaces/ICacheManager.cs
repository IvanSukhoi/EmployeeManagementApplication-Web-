namespace EmployeeManagement.Domain.Interfaces
{
    public interface ICacheManager
    {
        void Set(object key, object value);
        void Remove(object key);
        bool TryGetValue(object key, object obj);
        object Get(object key);
    }
}
