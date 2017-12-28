using System.Data;
using System.Data.SqlClient;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubQuery
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="transaction"></param>
        void Execute(IDbTransaction transaction);
    }
}
