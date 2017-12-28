using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using T2M.Common.Utils.ADONET.SQLServer;
using XZMY.Manage.Data.Query.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;

namespace XZMY.Manage.Data.Impl.Query.Assessment
{
    public class SearchSourceIdList : MappingQuery<Scores>, ISearchSourceIdList
    {
        protected override void BuildMapping()
        {
            MapAll();
        }
        public List<Scores> Invoke()
        {
            var condition = GetCondition();

            var sqlStatement = String.Format(
                string.IsNullOrWhiteSpace(condition)
                ? QueryTemplate.QUERY_SELECT
                : QueryTemplate.QUERY_SELECT_WITH_CLAUSE,
                String.Join(",", GetMappedProperties()), TableName, condition);

            var countsql = string.Format(string.IsNullOrWhiteSpace(condition)
                ? QueryTemplate.QUERY_SELECT_COUNT
                : QueryTemplate.QUERY_SELECT_COUNT_WITH_CLAUSE, TableName, condition);

            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var c = (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, countsql);

                var res = new List<Scores>();
                using (var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text, sqlStatement))
                {
                    while (reader.Read())
                        res.Add(reader.ToModel<Scores>());
                    return res;
                }
            }
        }

        public List<Guid> IdList { get; set; }

        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return IdList.Count > 0 
                //? $"[SourceId] IN ({string.Join(",", IdList.Select(x => $"'{x}'"))})" 
                ? "" 
                : string.Empty;
        }
    }
}
