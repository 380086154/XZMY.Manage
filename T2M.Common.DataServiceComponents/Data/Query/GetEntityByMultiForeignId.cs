using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetEntityByMultiForeignId<T> : MappingQuery<T>, IGetEntityByMultiForeignId<T> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByMultiForeignId() { }
        public GetEntityByMultiForeignId(string tableName) { TableName = tableName; }
        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<Tuple<Expression<Func<T, object>>, Guid>> ForeignMember { get; set; }

        public IList<T> Execute()
        {
            var properties = GetMappedProperties();
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT_WITH_CLAUSE,
                String.Join(",", properties), TableName, condition);


            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {

                using (
                    var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text,
                        sqlStatement, sqlParameter))
                {
                    var res = new List<T>();

                    while (reader.Read())
                    {
                        res.Add(reader.ToModel<T>());
                    }
                    return res;
                }
            }
        }


        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return String.Join(" AND ",
                ForeignMember.Select((m, index) => String.Format("{0} = @Id" + index, m.Item1.GetExpressionMemberName())));
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            return ForeignMember.Select((m, index) =>
                SqlServerHelper.BuildInParameter("@Id" + index, SqlDbType.UniqueIdentifier, m.Item2)
                ).ToArray();
        }
    }
}