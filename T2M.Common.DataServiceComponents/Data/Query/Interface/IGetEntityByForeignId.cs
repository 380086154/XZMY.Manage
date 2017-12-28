using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface IGetEntityByForeignId<T> where T : class ,IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ForeignId { get; set; }

        Expression<Func<T, object>> ForeignMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }
    public interface IGetEntityByForeignIdList<T> where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        IList<Guid> ForeignId { get; set; }

        Expression<Func<T, object>> ForeignMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }
    public interface IGetEntityByMultiForeignId<T> where T : class ,IEntity<Guid>, new()
    {

        IList<Tuple<Expression<Func<T, object>>, Guid>> ForeignMember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Execute();
    }

    public interface IGetEntityCountByForeignId<T> where T : class 
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ForeignId { get; set; }

        Expression<Func<T, object>> ForeignMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Execute();
    }
    public interface IGetEntityCountByMultiForeignId<T> where T : class 
    {

        IList<Tuple<Expression<Func<T, object>>, Guid>> ForeignMember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Execute();
    }


    public interface IGetPagedEntityByForeignId<T> where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ForeignId { get; set; }

        Expression<Func<T, object>> ForeignMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PagedResult<T> Execute();
    }
}