using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Remotion.Linq.Clauses;

namespace EmployeeManagement.DataEF.Interfaces
{
    public interface IQueryableDbProvider
    {
        IQueryable<T> Set<T>() where T : class;
    }
}
