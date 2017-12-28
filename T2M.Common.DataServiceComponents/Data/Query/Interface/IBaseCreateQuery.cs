using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface IBaseCreateQuery<T> : IQuery<T>, ISubQuery<T> where T : IDataModel
    {
        T Model { get; set; }
    }
}
