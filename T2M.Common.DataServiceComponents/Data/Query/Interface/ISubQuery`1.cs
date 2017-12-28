using System.Data;
using System.Data.SqlClient;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubQuery<out T>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="transaction"></param>
        T Execute(IDbTransaction transaction);
    }
}