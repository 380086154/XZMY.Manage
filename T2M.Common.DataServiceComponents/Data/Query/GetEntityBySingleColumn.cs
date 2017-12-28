using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetEntityBySingleColumn<T> : MappingQuery<T>, IGetEntityBySingleColumn<T> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityBySingleColumn() { }
        public GetEntityBySingleColumn(string tableName) { TableName = tableName; }
        protected override void BuildMapping()
        {
            MapAll();
        }

        public object ColumnValue { get; set; }
        public Expression<Func<T, object>> ColumnMember { get; set; }

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
            return String.Format("{0} = @value", ColumnMember.GetExpressionMemberName());
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            return new[]
            {
                SqlServerHelper.BuildInParameter("@value",GetDatabaseValue(ColumnValue) )
            };
        }
    }
}