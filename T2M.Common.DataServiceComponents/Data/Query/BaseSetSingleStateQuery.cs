using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Query
{
    public class BaseSetSingleStateQuery<T, TP> : IBaseSetSingleStateQuery<T, TP> where T : IDataModel
    {
        protected const string QueryTemplate = @"UPDATE {0} SET {1}=@Value WHERE DataId = @Id";


        public BaseSetSingleStateQuery() { }
        public BaseSetSingleStateQuery(string tableName) { TableName = tableName; }

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
                String.Format(QueryTemplate, TableName, Member.GetExpressionMemberName()), GetSqlParameters());
            return cmd1 > 0;
        }

        private SqlParameter[] GetSqlParameters()
        {
            return new[] { SqlServerHelper.BuildInParameter("Id", SqlDbType.UniqueIdentifier, ModelId),
                SqlServerHelper.BuildInParameter("Value",GetDatabaseValue( Value)) };
        }

        protected object GetDatabaseValue(Object val)
        {
            if (val == null)
                return null;

            if (val.GetType().IsEnum)
                return (Int32)val;

            /*
            if (value.GetType().IsClass)
            {
            }
            */

            return val;
        }

    }
    public class BaseSetSingleStateByForeignIdQuery<T, TP> : IBaseSetSingleStateByForeignIdQuery<T, TP> where T : IDataModel
    {
        protected const string QueryTemplate = @"UPDATE {0} SET {1}=@Value WHERE {2}=@FId";


        public BaseSetSingleStateByForeignIdQuery() { }
        public BaseSetSingleStateByForeignIdQuery(string tableName) { TableName = tableName; }

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

        public TP Value
        {

            get; set;
        }

        public Expression<Func<T, object>> Member
        {

            get; set;
        }

        public Guid ForeignId
        {
            get; set;
        }

        public Expression<Func<T, object>> ForeignMember
        {
            get; set;
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
                String.Format(QueryTemplate, TableName, Member.GetExpressionMemberName(), ForeignMember.GetExpressionMemberName()), GetSqlParameters());
            return cmd1 > 0;
        }

        private SqlParameter[] GetSqlParameters()
        {
            return new[] { SqlServerHelper.BuildInParameter("FId", SqlDbType.UniqueIdentifier, ForeignId),
                SqlServerHelper.BuildInParameter("Value", GetDatabaseValue(Value)) };
        }
        protected object GetDatabaseValue(Object val)
        {
            if (val == null)
                return null;

            if (val.GetType().IsEnum)
                return (Int32)val;

            /*
            if (value.GetType().IsClass)
            {
            }
            */

            return val;
        }

    }
}
