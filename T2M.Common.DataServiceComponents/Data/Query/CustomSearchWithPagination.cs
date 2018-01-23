using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class CustomSearchWithPagination<T> : PaginationQuery<T>, ICustomSearchWithPagination<T> where T : class, IDataModel, IEntity<Guid>, new()
    {

        public CustomSearchWithPagination() { }
        public CustomSearchWithPagination(string tableName) { TableName = tableName; }

        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<CustomCondition<T>> CustomConditions { get; set; }

        public override PagedResult<T> Execute()
        {
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            var sqlStatement = String.Format(QueryTemplate.QUERY_PAGINATION_WITH_CLAUSE, PageSize, PageSize * (PageIndex - 1),
                TableName, String.Join(",", GetMappedProperties()), GetSortTypeString(), condition);

            var countsql = String.Format(QueryTemplate.QUERY_SELECT_COUNT_WITH_CLAUSE, TableName, condition);


            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var c = (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, countsql, sqlParameter);

                using (var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text,
                        sqlStatement, sqlParameter))
                {
                    var res = InitPagedResult(c);

                    while (reader.Read())
                    {
                        res.Results.Add(reader.ToModel<T>());
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
            if (CustomConditions == null)
                return " 1=1 ";

            return String.Join(" AND ",
                CustomConditions.Select((m, index) =>
                {
                    if (m is CustomConditionBase<T>)
                        return String.Format("{0} {1}", ConvertUtils.As<CustomConditionBase<T>>(m).Member.GetExpressionMemberName(), GetOperation(m.Operation, index));
                    if (m is CustomConditionPlus<T>)
                    {
                        var members = ConvertUtils.As<CustomConditionPlus<T>>(m).Member;
                        var cond = String.Join(" OR ", members.Select(n => String.Format("{0} {1}", PredicateUtils.GetExpressionMemberName<T>(n), GetOperation(m.Operation, index))));
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
            if (m == SqlOperation.In) return "IN (@value" + index + ")";
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
