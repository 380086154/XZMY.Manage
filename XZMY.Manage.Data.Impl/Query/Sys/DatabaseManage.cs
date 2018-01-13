using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using XZMY.Manage.Data.Query.Sys;

namespace XZMY.Manage.Data.Impl.Query.Sys
{
    /// <summary>
    /// 数据库管理
    /// </summary>
    public class DatabaseManage : IDatabaseManage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Execute()
        {
            var result = 0;
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var condition = GetCondition();

                var sqlParameter = BuildParameters();
                var sqlStatement = string.Empty;
                foreach (var item in TablenameList)
                {
                    sqlStatement += string.Format(QueryTemplate.QUERY_DELETE_WITH_CLAUSE, item, condition) + ";";
                }

                if (!string.IsNullOrWhiteSpace(sqlStatement))
                {
                    result += SqlServerHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStatement, sqlParameter);
                }
            }
            return result;
        }

        /// <summary>
        /// 分店 Id
        /// </summary>
        public Guid BranchDataId { get; set; }

        /// <summary>
        /// 表名集合
        /// </summary>
        public List<string> TablenameList { get; set; }

        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private string GetCondition()
        {
            var list = new List<string>();
            list.Add("[BranchDataId] = @BranchDataId");
            return string.Join(" AND ", list);
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            var list = new List<SqlParameter>();
            list.Add(SqlServerHelper.BuildInParameter("@BranchDataId", SqlDbType.UniqueIdentifier, BranchDataId));
            return list.ToArray();
        }
    }
}
