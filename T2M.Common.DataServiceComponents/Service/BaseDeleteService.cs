using System;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    public class BaseDeleteService<T> : IInvokeService<bool>, IInvokeTransactionService<bool> where T : class, IDataModel, IEntity<Guid>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public BaseDeleteService(Guid id, string tablename = null)
        {
            Id = id;
            _tableName = tablename;
        }

        protected BaseDeleteService()
        {
        }

        private String _tableName;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

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
            var query = new BaseDeleteQuery<T>(_tableName);
            query.Id = Id;
            return query.Execute(transaction);
        }
    }
}