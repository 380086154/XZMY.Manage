using System;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Utils
{
    /// <summary>
    /// 事务相关
    /// </summary>
    public class SqlTransactionWrapper : IDisposable
    {
        private IDbTransaction _transaction;
        private SqlConnection _connection;
        private readonly object _gate = new object();
        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(ToString());

                lock (_gate)
                    if (_transaction == null)
                    {
                        _connection = new SqlConnection(SqlServerHelper.CONNECTION_STRING);
                        _connection.Open();
                        _transaction = _connection.BeginTransaction();
                    }
                return _transaction;
            }
        }

        /// <summary>
        /// 是否存在错误
        /// </summary>
        public Boolean HasError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public String ErrorMessage { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            lock (_gate)
            {
                if (_disposed) return;
                if (_transaction == null) return;

                if (HasError)
                    _transaction.Rollback();
                else
                    _transaction.Commit();

                _connection.Dispose();
                _disposed = true;
            }
        }
    }
}
