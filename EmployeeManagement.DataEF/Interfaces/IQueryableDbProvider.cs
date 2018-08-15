using System.Linq;

namespace EmployeeManagement.DataEF.Interfaces
{
    public interface IQueryableDbProvider
    {
        IQueryable<T> Set<T>() where T : class;
    }
}
