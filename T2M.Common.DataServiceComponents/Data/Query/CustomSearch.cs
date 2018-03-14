using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Query
{
    public class CustomSearch<T> : MappingQuery<T>, ICustomSearch<T> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public CustomSearch() { }
        public CustomSearch(string tableName) { TableName = tableName; }
        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<CustomCondition<T>> CustomConditions { get; set; }

        public IList<T> Execute()
        {
            var properties = GetMappedProperties();
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT_WITH_CLAUSE,
                String.Join(",", properties), TableName, condition);

            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                using (var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text,
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
                CustomConditions.Select((m, index) =>
                {
                    if (m is CustomConditionBase<T>)
                        return String.Format("{0} {1}", m.As<CustomConditionBase<T>>().Member.GetExpressionMemberName(), GetOperation(m.Operation, index));
                    if (m is CustomConditionPlus<T>)
                    {
                        var members = m.As<CustomConditionPlus<T>>().Member;
                        var cond = String.Join(" OR ", members.Select(n => String.Format("{0} {1}", n.GetExpressionMemberName(), GetOperation(m.Operation, index))));
                        return String.Format("({0})", cond);
                    }
                    return null;
                }));
        }

        private string GetOperation(SqlOperation m, int index)
        {
            if (m == SqlOperation.Equals) return "=@value" + index;
            if (m == SqlOperation.Greater) return ">@value" + index;
            if (m == SqlOperation.GreaterOrEquals) return ">=@value" + index;
            if (m == SqlOperation.Lesser) return "<@value" + index;
            if (m == SqlOperation.LesserOrEquals) return "<=@value" + index;
            if (m == SqlOperation.NotEquals) return "!=@value" + index;
            if (m == SqlOperation.Like) return "LIKE @value" + index;
            if (m == SqlOperation.StartWith) return "LIKE @value" + index;
            if (m == SqlOperation.EndWith) return "LIKE @value" + index;
            throw new NotSupportedException("Invalid SqlOperation Parameter");
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            return CustomConditions.Select((m, index) =>
                SqlServerHelper.BuildInParameter("@value" + index, GetParameterValue(m.Value, m.Operation))
                ).ToArray();
        }

        private object GetParameterValue(object value, SqlOperation m)
        {
            value = GetDatabaseValue(value);
            if (m == SqlOperation.Like) return "%" + value + "%";
            if (m == SqlOperation.StartWith) return "%" + value;
            if (m == SqlOperation.EndWith) return value + "%";
            return value;
        }
    }
}
