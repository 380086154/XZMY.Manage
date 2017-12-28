using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
namespace T2M.Common.Utils.ADONET.SQLServer
{
    /// <summary>
    /// Provides sets of methods for Microsoft SQL Server access.
    /// </summary>
    public class SqlServerHelper
    {
        #region Consts

        public static readonly String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #endregion

        #region Public methods

        /// <summary>
        /// Executes a Transact-SQL statement by using specific database connection and return number of affected rows.
        /// </summary>
        /// <param name="connString">A string that used to open a SQL Server database.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected</returns>
        public static Int32 ExecuteNonQuery(String connString, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            using (var conn = new SqlConnection(connString))
            {
                return ExecuteNonQuery(conn, cmdType, cmdText, sqlParams);
            }
        }

        /// <summary>
        /// Executes a Transact-SQL statement by using specific database connection and return number of affected rows.
        /// </summary>
        /// <param name="conn">A <see cref="SqlConection"/> that represents the connection to a instance of SQL Server.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected</returns>
        public static Int32 ExecuteNonQuery(SqlConnection conn, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            var cmd = new SqlCommand();
            BuildCommand(conn, cmd, cmdType, cmdText, sqlParams);
            var rows = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return rows;
        }

        /// <summary>
        /// Executes a Transact-SQL statement by using specific database connection and return number of affected rows.
        /// </summary>
        /// <param name="trans">The <see cref="SqlTransaction"/> in which the <see cref="SqlCommand"/> Executes.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected</returns>
        public static int ExecuteNonQuery(IDbTransaction trans, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            var cmd = new SqlCommand();
            BuildCommand(trans.Connection, cmd, trans, cmdType, cmdText, sqlParams);
            var rows = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return rows;
        }

        /// <summary>
        /// Execute a Transact-SQL statement by using specific database connection and return a result set. this result set is reading forward-only.
        /// </summary>
        /// <param name="connString">A string that used to open a SQL Server database.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>A reading forward-only result set.</returns>
        public static SqlDataReader ExecuteReader(String connString, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            var conn = new SqlConnection(connString);

            return ExecuteReader(conn, cmdType, cmdText, true, sqlParams);
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            return ExecuteReader(conn, cmdType, cmdText, false, sqlParams);
        }
        /// <summary>
        /// Execute a Transact-SQL statement by using specific database connection and return a result set. this result set is reading forward-only.
        /// </summary>
        /// <param name="conn">A <see cref="SqlConection"/> that represents th connection to a instance of SQL Server.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>A reading forward-only result set.</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, String cmdText, Boolean closeConnectionByReader,
            params SqlParameter[] sqlParams)
        {
            var cmd = new SqlCommand();

            try
            {
                BuildCommand(conn, cmd, cmdType, cmdText, sqlParams);

                //When the command is executed. the associated connection object is closed when the associated DataReader object is closed.
                var reader = cmd.ExecuteReader(closeConnectionByReader ? CommandBehavior.CloseConnection : CommandBehavior.Default);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                AddLog(ex, conn);//记录日志
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a Transact-SQL statement and return first column of the first row in the result set returned by the query. Additional columns and rows are ignored.
        /// </summary>
        /// <param name="connString">A string that used to open a SQL Server database.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        public static Object ExecuteScalar(String connString, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            using (var conn = new SqlConnection(connString))
            {
                return ExecuteScalar(conn, cmdType, cmdText, sqlParams);
            }
        }

        /// <summary>
        /// Execute a Transact-SQL statement and return first column of the first row in the result set returned by the query. Additional columns and rows are ignored.
        /// </summary>
        /// <param name="conn">A <see cref="SqlConection"/> that represents the connection to a instance of SQL Server.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        public static Object ExecuteScalar(SqlConnection conn, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            try
            {
                var cmd = new SqlCommand();

                BuildCommand(conn, cmd, cmdType, cmdText, sqlParams);
                var val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();

                return val;
            }
            catch (Exception ex)
            {
                AddLog(ex, conn);
                throw;
            }
        }

        /// <summary>
        /// Execute a Transact-SQL statement and return first column of the first row in the result set returned by the query. Additional columns and rows are ignored.
        /// </summary>
        /// <param name="trans">The <see cref="SqlTransaction"/> in which the <see cref="SqlCommand"/> Executes.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        public static Object ExecuteScalar(SqlTransaction trans, CommandType cmdType, String cmdText,
            params SqlParameter[] sqlParams)
        {
            try
            {
                var cmd = new SqlCommand();

                BuildCommand(trans.Connection, cmd, trans, cmdType, cmdText, sqlParams);
                var val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();

                return val;
            }
            catch (Exception ex)
            {
                AddLog(ex, trans);
                throw;
            }
        }

        /// <summary>
        /// Build a input parameter for a <see cref="SqlCommand"/>.
        /// </summary>
        /// <param name="paramterName">The name of the parameter.</param>
        /// <param name="dbType">Specifies SQL Server specific data type.</param>
        /// <param name="val">The value of the parameter.</param>
        /// <returns>a input parameter for a <see cref="SqlCommand"/></returns>
        public static SqlParameter BuildInLikeParameter(String paramterName, SqlDbType dbType, Object val)
        {
            return BuildInParameter(paramterName, dbType, "%" + val + "%");
        }

        public static SqlParameter BuildInParameter(string paramterName, object val)
        {
            var dtype = SqlDbType.NText;

            if(val == null)
                return BuildInParameter(paramterName, dtype, val);

            var type = val.GetType();
            if (type == typeof(Guid))
                dtype = SqlDbType.UniqueIdentifier;
            if (type == typeof(Int32))
                dtype = SqlDbType.Int;
            if (type == typeof(Int16))
                dtype = SqlDbType.SmallInt;
            if (type == typeof(Int64))
                dtype = SqlDbType.BigInt;
            if (type == typeof(string))
                dtype = SqlDbType.NVarChar;
            if (type == typeof(DateTime))
                dtype = SqlDbType.DateTime;
            if (type == typeof(decimal))
                dtype = SqlDbType.Decimal;
            if (type == typeof(float))
                dtype = SqlDbType.Float;
            if (type == typeof(double))
                dtype = SqlDbType.Decimal;
            if (type == typeof(bool))
                dtype = SqlDbType.Bit;

            return BuildInParameter(paramterName, dtype, val);
        }
        /// <summary>
        /// Build a input parameter for a <see cref="SqlCommand"/>.
        /// </summary>
        /// <param name="paramterName">The name of the parameter.</param>
        /// <param name="dbType">Specifies SQL Server specific data type.</param>
        /// <param name="val">The value of the parameter.</param>
        /// <returns>a input parameter for a <see cref="SqlCommand"/></returns>
        public static SqlParameter BuildInParameter(String paramterName, SqlDbType dbType, Object val)
        {
            if (val == null)
                //throw new ArgumentNullException("val", "The null sql parameter is not allowed");
                val = String.Empty;

            return new SqlParameter(paramterName, val)
            {
                SqlDbType = dbType,
                Direction = ParameterDirection.Input
            };
        }

        /// <summary>
        /// Build a output/return parameter for a <see cref="SqlCommand"/>.
        /// </summary>
        /// <param name="paramterName">The name of the parameter.</param>
        /// <param name="dbType">Specifies SQL Server specific data type.</param>
        /// <param name="val">The value of the parameter.</param>
        /// <param name="isReturnVal">the type of parameter. true, return value, false, output.</param>
        /// <returns>a input parameter for a <see cref="SqlCommand"/></returns>
        public static SqlParameter BuildOutParameter(String paramterName, SqlDbType dbType, Object val, Boolean isReturnVal)
        {
            if (val == null)
                //throw new ArgumentNullException("val", "The null sql parameter is not allowed");
                val = String.Empty;

            var param = new SqlParameter(paramterName, val)
            {
                SqlDbType = dbType,

                Direction = isReturnVal
                    ? ParameterDirection.ReturnValue
                    : ParameterDirection.Output
            };

            return param;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Build a <see cref="SqlCommand"/> object for query execution.
        /// </summary>
        /// <param name="conn">A <see cref="SqlConection"/> that represents the connection to a instance of SQL Server.</param>
        /// <param name="cmd">A Transact-SQL statement or stored procedure to execute against a SQL Server database.</param>
        /// <param name="cmdType">A value indicates how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        private static void BuildCommand(SqlConnection conn, SqlCommand cmd, CommandType cmdType, String cmdText, IList<SqlParameter> sqlParams)
        {
            BuildCommand(conn, cmd, null, cmdType, cmdText, sqlParams);
        }

        /// <summary>
        /// Build a <see cref="SqlCommand"/> object for query execution.
        /// </summary>
        /// <param name="conn">A <see cref="SqlConection"/> that represents the connection to a instance of SQL Server.</param>
        /// <param name="cmd">A Transact-SQL statement or stored procedure to execute against a SQL Server database.</param>
        /// <param name="trans">The <see cref="SqlTransaction"/> in which the <see cref="SqlCommand"/> Executes.</param>
        /// <param name="cmdType">A value indicate how the <see cref="CommandText"/> property is to be interpreted.</param>
        /// <param name="cmdText">The text of the query or name of stored procedure.</param>
        /// <param name="sqlParams">The parameters of T-SQL statement or stored procedure.</param>
        private static void BuildCommand(IDbConnection conn, SqlCommand cmd, IDbTransaction trans, CommandType cmdType, String cmdText, IList<SqlParameter> sqlParams)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn as SqlConnection;
                cmd.CommandText = cmdText;

                if (trans != null)
                    cmd.Transaction = trans as SqlTransaction;

                cmd.CommandType = cmdType;

                if (sqlParams != null && sqlParams.Count > 0)
                {
                    foreach (SqlParameter param in sqlParams)
                        cmd.Parameters.Add(param);
                }
            }
            catch (Exception ex)
            {
                AddLog(ex, conn);//记录日志
                throw;
            }
        }

        private static void AddLog(Exception ex, string connection)
        {
            var logdirpath = "/log_data/";
            if (!Directory.Exists(logdirpath)) Directory.CreateDirectory(logdirpath);
            var json = JsonConvert.SerializeObject(ex);
            File.AppendAllText(logdirpath + DateTime.Today.ToString("yyyyMMdd") + ".txt", json);
            File.AppendAllText(logdirpath + DateTime.Today.ToString("yyyyMMdd") + ".txt", Environment.NewLine);
        }

        private static void AddLog(Exception ex, SqlConnection conn)
        {
            AddLog(ex, conn.ConnectionString);
        }

        private static void AddLog(Exception ex, IDbConnection conn)
        {
            AddLog(ex, conn.ConnectionString);
        }

        private static void AddLog(Exception ex, SqlTransaction conn)
        {
            AddLog(ex, conn.Connection.ConnectionString);
        }

        #endregion

    }
}
