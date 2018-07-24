namespace EmployeeManagement.DataEF.Interfaces
{
    public interface IUpdateDbProvider
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}
