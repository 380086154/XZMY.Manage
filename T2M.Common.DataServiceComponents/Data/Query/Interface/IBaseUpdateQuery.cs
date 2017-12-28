using System;
using System.Linq.Expressions;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseUpdateQuery<T> : IQuery<bool>, ISubQuery<bool> where T : IDataModel
    {
        T Model { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    public interface IBaseSetSingleStateQuery<T,TP> : IQuery<bool>, ISubQuery<bool> where T : IDataModel
    {
        Guid ModelId { get; set; }
        TP Value { get; set; }
        Expression<Func<T, object>> Member { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    public interface IBaseSetSingleStateByForeignIdQuery<T, TP> : IQuery<bool>, ISubQuery<bool> where T : IDataModel
    {
        Guid ForeignId { get; set; }
        TP Value { get; set; }
        Expression<Func<T, object>> Member { get; set; }
        Expression<Func<T, object>> ForeignMember { get; set; }

    }
}