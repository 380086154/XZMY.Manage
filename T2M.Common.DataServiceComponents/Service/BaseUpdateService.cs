using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Service
{
    public class BaseUpdateService<T> : IInvokeService<bool>, IInvokeTransactionService<bool> where T : class, IDataModel, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public BaseUpdateService(T model, string tablename = null)
        {
            Model = model;
            _tableName = tablename;
        }
        private String _tableName;

        /// <summary>
        /// 
        /// </summary>
        public virtual T Model { get; set; }

        /// <summary>
        /// Ö´ÐÐ
        /// </summary>
        /// <returns></returns>
        public virtual bool Invoke()
        {
            using (var wrapper = new SqlTransactionWrapper())
            {
                try
                {
                    return Invoke(wrapper.Transaction);
                }
                catch (Exception)
                {
                    wrapper.HasError = true;
                    throw;
                }
            }
        }

        public virtual bool Invoke(IDbTransaction transaction)
        {
            var query = new BaseUpdateQuery<T>(_tableName);
            query.Model = Model;
            return query.Execute(transaction);
        }
    }

    public class BaseSetSingleStateService<T,TP> : IInvokeService<bool>, IInvokeTransactionService<bool> where T : class, IDataModel, new()
    {
        public BaseSetSingleStateService(string tablename = null)
        {
            _tableName = tablename;
        }

        private String _tableName;

        public Guid ModelId
        {
            get; set;
        }

        public TP Value
        {

            get; set;
        }

        public Expression<Func<T, object>> Member
        {
            get; set;
        }
        public virtual bool Invoke()
        {
            using (var wrapper = new SqlTransactionWrapper())
            {
                try
                {
                    return Invoke(wrapper.Transaction);
                }
                catch (Exception)
                {
                    wrapper.HasError = true;
                    throw;
                }
            }
        }

        public virtual bool Invoke(IDbTransaction transaction)
        {
            var query = new BaseSetSingleStateQuery<T, TP>()
            {
                ModelId = ModelId,
                Member = Member,
                Value = Value,
                TableName = _tableName
            };
            return query.Execute(transaction);
        }
    }


    public class BaseSetSingleStateByForeignIdService<T, TP> : IInvokeService<bool>, IInvokeTransactionService<bool> where T : class, IDataModel, new()
    {
        public BaseSetSingleStateByForeignIdService(string tablename = null)
        {
            _tableName = tablename;
        }

        private String _tableName;

        public Expression<Func<T, object>> ForeignMember { get; set; }
        public Guid ForeignId { get; set; }

        public TP Value
        {

            get; set;
        }

        public Expression<Func<T, object>> Member
        {
            get; set;
        }
        public virtual bool Invoke()
        {
            using (var wrapper = new SqlTransactionWrapper())
            {
                try
                {
                    return Invoke(wrapper.Transaction);
                }
                catch (Exception)
                {
                    wrapper.HasError = true;
                    throw;
                }
            }
        }

        public virtual bool Invoke(IDbTransaction transaction)
        {
            var query = new BaseSetSingleStateByForeignIdQuery<T, TP>()
            {
                ForeignId = ForeignId,
                ForeignMember = ForeignMember,
                Member = Member,
                Value = Value,
                TableName = _tableName
            };
            return query.Execute(transaction);
        }
    }
}