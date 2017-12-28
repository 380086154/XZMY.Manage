using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetEntityCountByForeignId<T> : IGetEntityCountByForeignId<T> where T : class, IDataModel
    {


        public GetEntityCountByForeignId() { }
        public GetEntityCountByForeignId(string tableName) { TableName = tableName; }



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


        public Guid ForeignId { get; set; }
        public Expression<Func<T, object>> ForeignMember { get; set; }

        public int Execute()
        {
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT_COUNT_WITH_CLAUSE,
                TableName, condition);


            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                
                var c =
                    (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, sqlStatement, sqlParameter);
                return c;
            }
        }


        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return String.Format("{0} = @Id", ForeignMember.GetExpressionMemberName());
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            return new[]
            {
                SqlServerHelper.BuildInParameter("@Id",SqlDbType.UniqueIdentifier, ForeignId)
            };
        }
    }
}