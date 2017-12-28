using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetEntityList<T> : PaginationQuery<T> where T : class, IDataModel, IEntity<Guid>, new()
    {

        public GetEntityList() { }
        public GetEntityList(string tableName) { TableName = tableName; }

        protected override void BuildMapping()
        {
            MapAll();
        }

        public override PagedResult<T> Execute()
        {
            var sqlStatement = String.Format(QueryTemplate.QUERY_PAGINATION, PageSize, PageSize * (PageIndex - 1),
                    TableName, String.Join(",", GetMappedProperties()), GetSortTypeString());

            var countsql = String.Format(QueryTemplate.QUERY_SELECT_COUNT, TableName);
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var c =
                    (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, countsql);

                using (
                    var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text,
                        sqlStatement))
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
    }

    public class GetEntityTable<T> : MappingQuery<T>, IQuery<IList<T>> where T : class, IDataModel, IEntity<Guid>, new()
    {

        public GetEntityTable() { }
        public GetEntityTable(string tableName) { TableName = tableName; }
        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<T> Execute()
        {
            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT,
                String.Join(",", GetMappedProperties()), TableName); 
            using (var reader = SqlServerHelper.ExecuteReader(SqlServerHelper.CONNECTION_STRING, CommandType.Text,
                        sqlStatement))
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
}
