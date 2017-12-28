using System;
using System.Linq.Expressions;
using T2M.Common.Utils.Models;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    public interface IBaseDeleteQuery<T,TId> : IQuery<bool>, ISubQuery<bool> where T : IDataModel,IEntity<TId>
    {
        TId Id { get; set; }
    }
    public interface IBaseDeleteQuery<T> : IBaseDeleteQuery<T, Guid> where T : IDataModel, IEntity<Guid>
    {
    }

    public interface IBaseDeleteByForeignIdQuery<T> : IBaseDeleteByForeignIdQuery<T, Guid> where T : IDataModel, IEntity<Guid>
    {

    }
    public interface IBaseDeleteByForeignIdQuery<T, TId> : IQuery<int>, ISubQuery<int> where T : IDataModel, IEntity<TId>
    {
        Expression<Func<T, object>> ForeignMember { get; set; }
        TId ForeignId { get; set; }
    }
}