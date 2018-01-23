
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Data.Query.Customer;
using XZMY.Manage.Model.DataModel.Customer;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace XZMY.Manage.Data.Impl.Query.Customer
{
    public class SearchXfxx : PaginationQuery<Xfxx>, ISearchXfxx
    {
        protected override void BuildMapping()
        {
            MapAll();
        }
        public override PagedResult<UserAccount> Execute()
        {
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            //var sqlStatement = string.Format(string.IsNullOrWhiteSpace(condition)
            //    ? string.Format(QueryTemplate.QUERY_PAGINATION, PageSize, PageSize * (PageIndex - 1),
            //        TableName, string.Join(",", GetMappedProperties()), GetSortTypeString())
            //    : string.Format(QueryTemplate.QUERY_PAGINATION_WITH_CLAUSE, PageSize, PageSize * (PageIndex - 1),
            //        TableName, string.Join(",", GetMappedProperties()), condition, GetSortTypeString()));

            var sqlStatement = string.Format(string.IsNullOrWhiteSpace(condition)
                    ? QueryTemplate.QUERY_PAGINATION
                    : QueryTemplate.QUERY_PAGINATION_WITH_CLAUSE
                    , PageSize, PageSize * (PageIndex - 1), TableName, string.Join(",", GetMappedProperties()), GetSortTypeString(), condition);

            var countsql = string.Format(string.IsNullOrWhiteSpace(condition)
                ? QueryTemplate.QUERY_SELECT_COUNT
                : QueryTemplate.QUERY_SELECT_COUNT_WITH_CLAUSE, TableName, condition);

            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var c = (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, countsql, sqlParameter);

                using (var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text, sqlStatement, sqlParameter))
                {
                    var res = InitPagedResult(c);

                    while (reader.Read())
                    {
                        res.Results.Add(reader.ToModel<UserAccount>());
                    }
                    return res;
                }
            }
        }

        public string Keyword { get; set; }

        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(Keyword)) list.Add("[Name] LIKE '%@Keyword%'");
            return String.Join(" AND ", list);
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            var list = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(Keyword))
                list.Add(SqlServerHelper.BuildInParameter("@Keyword", SqlDbType.NVarChar, Keyword));
            return list.ToArray();
        }
    }
}
