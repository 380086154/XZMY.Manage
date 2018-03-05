using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// Provides sets of methods for Microsoft SQL Server access.
    /// </summary>
    [Serializable]
    public class DatabaseHelper
    {
        #region Fileds

        private string formatString = "PRovider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database PassWord=mp61";

        public string ConnectionString_SqlServer = string.Empty;
        //public const string connectionString_SqlServer = "Data Source=sds209635357.my3w.com;Initial Catalog=sds209635357_db; Persist Security Info=True;User ID=sds209635357; Password=E17680A936674932B358;MultipleActiveResultSets=true";
        //public const string connectionString_SqlServer = "Server=101.37.25.133;Initial Catalog=qds118399686_debug;User ID=CollegeSa;Password=Aa123456;MultipleActiveResultSets=True;";

        private string connectionString_Access = string.Empty;
        /// <summary>
        /// Access 数据库地址
        /// </summary>
        public string ConnectionString_Access
        {
            get { return connectionString_Access; }
            set { connectionString_Access = string.Format(formatString, value); }
        }

        #endregion

        public DatabaseHelper() { }

        public DatabaseHelper(string path, string connectionString)
        {
            connectionString_Access = string.Format(formatString, path);
            ConnectionString_SqlServer = connectionString;
        }

        /// <summary>
        /// 执行无返回值的sql语句
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlCommand, EProviderName providerName)
        {
            int result = 0;
            using (DbConnection connection = GetConnection(providerName))
            {
                DbCommand command = GetCommand(sqlCommand, CommandType.Text, connection);
                connection.Open();
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
                return result;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="providerName"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlCommand, EProviderName providerName, params SqlParameter[] sqlParams)
        {
            int result = 0;
            using (DbConnection connection = GetConnection(providerName))
            {
                DbCommand command = GetCommand(sqlCommand, CommandType.Text, connection, sqlParams);

                connection.Open();
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
                return result;
            }
        }

        public int ExecuteNonQuery(string sqlCommand, EProviderName providerName, params OleDbParameter[] oleDbParameter)
        {
            int result = 0;
            using (DbConnection connection = GetConnection(providerName))
            {
                DbCommand command = GetCommand(sqlCommand, CommandType.Text, connection, oleDbParameter);

                connection.Open();
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
                return result;
            }
        }

        /// <summary>
        /// 执行有返回值的sql语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public int ExecuteScalar(string cmdText, EProviderName providerName)
        {
            using (DbConnection connection = GetConnection(providerName))
            {
                DbCommand command = GetCommand(cmdText, CommandType.Text, connection);
                connection.Open();
                object val = command.ExecuteScalar();
                command.Parameters.Clear();

                return val != null
                    ? int.Parse(val.ToString())
                    : 0;
            }
        }

        /// <summary>
        /// 返回DataTable对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="tableName"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string cmdText, string tableName, EProviderName providerName)
        {
            using (DbConnection connection = GetConnection(providerName))
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(GetProviderName(providerName));
                DbCommand command = GetCommand(cmdText, CommandType.Text, connection);
                connection.Open();
                DbDataAdapter da = factory.CreateDataAdapter();
                da.SelectCommand = command;
                DataTable datatable = new DataTable(tableName);
                da.Fill(datatable);
                return datatable;
            }
        }

        //private Dictionary<string, SqlBulkCopyColumnMapping> columnMappings = new Dictionary<string, SqlBulkCopyColumnMapping>();

        /// <summary>
        /// 保存 DataTable 批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="providerName"></param>
        public void SqlBulkCopyByDataTable(DataTable dt, string tableName, EProviderName providerName)
        {
            using (SqlBulkCopy sbc = new SqlBulkCopy(GetConnection(providerName).ConnectionString))
            {
                sbc.BatchSize = 20000;
                sbc.DestinationTableName = tableName;

                //缓存计划
                //if (columnMappings.ContainsKey(tableName))
                //    sbc.ColumnMappings.Add(columnMappings[tableName]);
                //else
                //{
                //    columnMappings.Add(tableName, sbc.ColumnMappings);
                //}

                foreach (DataColumn item in dt.Columns)
                {
                    sbc.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }

                sbc.WriteToServer(dt);
            }
        }

        #region Private Method

        /// <summary>
        /// GetConnection 用于获取连接数据库的 connection 对象
        /// </summary>
        /// <returns></returns>
        private DbConnection GetConnection(EProviderName providerName)
        {
            DbConnection connection = DbProviderFactories.GetFactory(GetProviderName(providerName)).CreateConnection();

            //根据 providerName 动态选择连接字符串
            if (providerName == EProviderName.SqlClient)
                connection.ConnectionString = ConnectionString_SqlServer;
            else if (providerName == EProviderName.OleDB)
                connection.ConnectionString = connectionString_Access;

            return connection;

        }

        /// <summary>
        /// GetCommand 获取命令参数 command 对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="connection"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private DbCommand GetCommand(string commandText, CommandType commandType, DbConnection connection, IList<SqlParameter> sqlParams = null)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (sqlParams != null && sqlParams.Count > 0)
            {
                foreach (SqlParameter param in sqlParams)
                    command.Parameters.Add(param);
            }
            return command;
        }

        private DbCommand GetCommand(string commandText, CommandType commandType, DbConnection connection, IList<OleDbParameter> oleDbParameter)
        {
            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (oleDbParameter != null && oleDbParameter.Count > 0)
            {
                foreach (OleDbParameter param in oleDbParameter)
                    command.Parameters.Add(param);
            }
            return command;
        }

        private string GetProviderName(EProviderName name)
        {
            return "System.Data." + name;
        }

        #endregion
    }

    public enum EProviderName
    {
        /// <summary>
        /// 远程 SQL Server 数据库
        /// </summary>
        SqlClient = 1,
        /// <summary>
        /// 本地 Access 数据库
        /// </summary>
        OleDB = 2,
        /// <summary>
        /// 本地 SQLite 数据库
        /// </summary>
        SQLite = 3,
    }
}
