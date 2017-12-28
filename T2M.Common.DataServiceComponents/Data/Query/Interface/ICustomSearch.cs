using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface ICustomSearch<T> : IQuery<IList<T>>
    {
        IList<CustomCondition<T>> CustomConditions { get; set; }
    }
    public interface ICustomSearchWithPagination<T> : IPaginationQuery<T>
    {
        IList<CustomCondition<T>> CustomConditions { get; set; }
    }
}
