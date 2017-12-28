using System;

using System.Collections.Generic;

using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetEntityByIdList<T> where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        IList<Guid> IdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }
}
