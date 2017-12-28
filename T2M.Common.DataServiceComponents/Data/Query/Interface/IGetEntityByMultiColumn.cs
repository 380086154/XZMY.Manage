using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface IGetEntityByMultiColumn<T> where T : class ,IEntity<Guid>, new()
    {

        IList<Tuple<Expression<Func<T, object>>, Guid>> ColumnMember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }


}