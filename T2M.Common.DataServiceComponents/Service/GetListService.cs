using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    /// <summary>
    /// 获取集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetEntityListService<T> : IInvokeService<PagedResult<T>> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityListService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数。
        /// </summary>
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Expression<Func<T, object>>[] SortMember { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortType SortType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual PagedResult<T> Invoke()
        {
            var query = new GetEntityList<T>(_tableName);
            query.PageIndex = PageIndex;
            query.PageSize = PageSize;

            query.SortMember = SortMember;
            query.SortType = SortType;

            return query.Execute();
        }
    }


    public class GetEntityTableService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityTableService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;
        public IList<T> Invoke()
        {
            var query = new GetEntityTable<T>(_tableName);
            return query.Execute();
        }
    }

    public class GetEntityByForeignIdService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByForeignIdService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;

        public Guid ForeignId { get; set; }
        public Expression<Func<T, object>> ForeignMember { get; set; }

        public IList<T> Invoke()
        {
            var query = new GetEntityByForeignId<T>(_tableName);
            query.ForeignId = ForeignId;
            query.ForeignMember = ForeignMember;
            return query.Execute();
        }
    }


    public class GetEntityByForeignIdListService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByForeignIdListService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;

        public IList<Guid> ForeignId { get; set; }
        public Expression<Func<T, object>> ForeignMember { get; set; }

        public IList<T> Invoke()
        {
            var query = new GetEntityByForeignIdList<T>(_tableName);
            query.ForeignId = ForeignId;
            query.ForeignMember = ForeignMember;
            return query.Execute();
        }
    }
    public class GetEntityCountByForeignIdService<T> : IInvokeService<int>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityCountByForeignIdService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;

        public Guid ForeignId { get; set; }
        public Expression<Func<T, object>> ForeignMember { get; set; }

        public int Invoke()
        {
            var query = new GetEntityCountByForeignId<T>(_tableName);
            query.ForeignId = ForeignId;
            query.ForeignMember = ForeignMember;
            return query.Execute();
        }
    }


    public class GetEntityByMultiForeignIdService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByMultiForeignIdService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;

        public IList<Tuple<Expression<Func<T, object>>, Guid>> ForeignMember { get; set; }

        public IList<T> Invoke()
        {
            var query = new GetEntityByMultiForeignId<T>(_tableName);
            query.ForeignMember = ForeignMember;
            return query.Execute();
        }
    }
    public class GetEntityBySingleColumnService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityBySingleColumnService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;


        public object ColumnValue { get; set; }
        public Expression<Func<T, object>> ColumnMember { get; set; }

        public IList<T> Invoke()
        {
            var query = new GetEntityBySingleColumn<T>(_tableName);
            query.ColumnValue = ColumnValue;
            query.ColumnMember = ColumnMember;
            return query.Execute();
        }
    }
    public class GetEntityByMultiColumnService<T> : IInvokeService<IList<T>>
        where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByMultiColumnService(string tablename = null)
        {
            _tableName = tablename;
        }
        private string _tableName;


        public IList<Tuple<Expression<Func<T, object>>, Guid>> ColumnMember { get; set; }

        public IList<T> Invoke()
        {
            var query = new GetEntityByMultiColumn<T>(_tableName);
            query.ColumnMember = ColumnMember;
            return query.Execute();
        }
    }

}