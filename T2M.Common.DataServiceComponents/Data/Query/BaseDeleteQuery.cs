using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class BaseDeleteQuery<T> : IBaseDeleteQuery<T> where T : IDataModel, IEntity<Guid>
    {

        protected const string QueryTemplate = @"DELETE FROM {0} WHERE DataId = @Id";


        public BaseDeleteQuery() { }
        public BaseDeleteQuery(string tableName) { TableName = tableName; }

        private String _tableName = string.Empty;
        public virtual String TableName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_tableName)) return _tableName;

                var tableattr = typeof(T).GetCustomAttribute<DBTableAttribute>();
                if (tableattr != null)
                    return tableattr.TableName;
                return typeof(T).Name;
            }
            set { _tableName = value; }
        }



        public bool Execute()
        {
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {

                    var bl = Execute(transaction);
                    transaction.Commit();
                    return bl;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Execute(IDbTransaction transaction)
        {
            var cmd1 = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text,
                String.Format(QueryTemplate, TableName), GetSqlParameters());
            return cmd1 > 0;
        }

        private SqlParameter[] GetSqlParameters()
        {
            return new[] { SqlServerHelper.BuildInParameter("Id", SqlDbType.UniqueIdentifier, Id) };
        }

        public Guid Id { get; set; }
    }

    public class BaseLogicalDeleteQuery<T> : IBaseDeleteQuery<T> where T : ILogicalDeletable, IDataModel, IEntity<Guid>
    {

        protected const string QueryTemplate = @"UPDATE {0} SET [IsDeleted]=1 WHERE DataId = @Id";

        public BaseLogicalDeleteQuery() { }
        public BaseLogicalDeleteQuery(string tableName) { TableName = tableName; }


        private String _tableName = string.Empty;
        public virtual String TableName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_tableName)) return _tableName;

                return typeof(T).Name;
            }
            set { _tableName = value; }
        }

        public bool Execute()
        {
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {

                    var bl = Execute(transaction);
                    transaction.Commit();
                    return bl;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Execute(IDbTransaction transaction)
        {
            var cmd1 = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text,
                String.Format(QueryTemplate, TableName), GetSqlParameters());
            return cmd1 > 0;
        }

        private SqlParameter[] GetSqlParameters()
        {
            return new[] { SqlServerHelper.BuildInParameter("Id", SqlDbType.UniqueIdentifier, Id) };
        }

        public Guid Id { get; set; }
    }
}