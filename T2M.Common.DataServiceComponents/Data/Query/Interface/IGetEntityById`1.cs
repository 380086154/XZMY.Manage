using System;

using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetEntityById<T> where T : class ,IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T Execute();
    }
}
