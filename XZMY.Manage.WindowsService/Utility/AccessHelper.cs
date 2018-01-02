using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// conn 的摘要说明。
    /// </summary>
    public class AccessHelper
    {
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        private string connectionString;

        /// <summary>
        /// 存储数据库连接（保护类，只有由它派生的类才能访问）
        /// </summary>
        protected OleDbConnection Connection;

        /// <summary>
        /// 构造函数：数据库的默认连接
        /// </summary>
        public AccessHelper(string path)
        {
            var connStr = string.Format("PRovider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database PassWord=mp61", path);

            connectionString = connStr;
            Connection = new OleDbConnection(connectionString);
        }

        /// <summary>
        /// 获得连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        /// <summary>
        /// 执行SQL语句没有返回结果，如：执行删除、更新、插入等操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>操作成功标志</returns>
        public bool ExecuteNonQuery(string cmdText)
        {
            bool resultState = false;

            Connection.Open();
            OleDbTransaction myTrans = Connection.BeginTransaction();
            OleDbCommand command = new OleDbCommand(cmdText, Connection, myTrans);

            try
            {
                command.ExecuteNonQuery();
                myTrans.Commit();
                resultState = true;
            }
            catch
            {
                myTrans.Rollback();
                resultState = false;
            }
            finally
            {
                Connection.Close();
            }
            return resultState;
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataReader中
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>dataReader</returns>
        private OleDbDataReader GetDataReader(string cmdText)
        {
            Connection.Open();
            OleDbCommand command = new OleDbCommand(cmdText, Connection);
            OleDbDataReader dataReader = command.ExecuteReader();
            Connection.Close();

            return dataReader;
        }

        /// <summary>
        /// 执行SQL语句返回结果到DataSet中
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>DataSet</returns>
        public DataTable GetDataSet(string cmdText)
        {
            Connection.Open();
            DataSet dataSet = new DataSet();
            try
            {
                OleDbDataAdapter OleDbDA = new OleDbDataAdapter(cmdText, Connection);
                OleDbDA.Fill(dataSet, "objDataSet");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return dataSet.Tables[0];
        }

        /// <summary>
        /// 执行一查询语句，同时返回查询结果数目
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>sqlResultCount</returns>
        public int ExecuteScalar(string cmdText)
        {
            object val = 0;

            try
            {
                Connection.Open();
                OleDbCommand command = new OleDbCommand(cmdText, Connection);
                val = command.ExecuteScalar();

                return val != null
                      ? int.Parse(val.ToString())
                      : 0;
            }
            catch (Exception ex)
            {
                var sd = ex.Message;
                val = 0;
            }
            finally
            {
                Connection.Close();
            }
            return int.Parse(val.ToString());
        }
    }
}
