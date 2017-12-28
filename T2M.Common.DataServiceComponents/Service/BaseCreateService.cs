using System;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Service
{
    public class BaseCreateService<T> : IInvokeService<T>,IInvokeTransactionService<T> where T : class, IDataModel, new()
    {
        protected BaseCreateService()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public BaseCreateService(T model,string tablename = null)
        {
            Model = model;
            _tableName = tablename;
        }

        private String _tableName;
        /// <summary>
        /// 
        /// </summary>
        public virtual T Model { get; set; }

        public virtual T Invoke(IDbTransaction transaction)
        {
            var query = new BaseCreateQuery<T>(_tableName);
            query.Model = Model;
            return query.Execute(transaction);
        }

        public virtual T Invoke()
        {
            using (var wrapper = new SqlTransactionWrapper())
            {
                try
                {
                    return Invoke(wrapper.Transaction);
                }
                catch (Exception ex)
                {
                    wrapper.HasError = true;
                    throw;
                }
            }
        }
    }
}