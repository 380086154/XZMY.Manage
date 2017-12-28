using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface IGetEntityBySingleColumn<T> where T : class ,IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        object ColumnValue { get; set; }

        Expression<Func<T, object>> ColumnMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }
}