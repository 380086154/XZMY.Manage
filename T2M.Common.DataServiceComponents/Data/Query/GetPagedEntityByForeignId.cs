using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetPagedEntityByForeignId<T> : PaginationQuery<T>, IGetPagedEntityByForeignId<T> where T : class, IDataModel, IEntity<Guid>, new()
    {

        public GetPagedEntityByForeignId() { }
        public GetPagedEntityByForeignId(string tableName) { TableName = tableName; }

        protected override void BuildMapping()
        {
            MapAll();
        }

        public Guid ForeignId { get; set; }
        public Expression<Func<T, object>> ForeignMember { get; set; }

        public override PagedResult<T> Execute()
        {
            var condition = GetCondition();
            var sqlParameter = BuildParameters();
            var sqlStatement = String.Format(QueryTemplate.QUERY_PAGINATION_WITH_CLAUSE, PageSize, PageSize * (PageIndex - 1),
                TableName, String.Join(",", GetMappedProperties()), GetSortTypeString(), condition);

            var countsql = String.Format(QueryTemplate.QUERY_SELECT_COUNT_WITH_CLAUSE, TableName, condition);


            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                var c =
                    (int)SqlServerHelper.ExecuteScalar(conn, CommandType.Text, countsql, sqlParameter);

                using (
                    var reader = SqlServerHelper.ExecuteReader(conn, CommandType.Text,
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
        /// ����ƴ��
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return String.Format("{0} = @Id", (ForeignMember.GetExpressionMemberName()));
        }

        /// <summary>
        /// ��������ִ��T-SQL���Ĳ�����
        /// </summary>
        /// <returns>һ������ִ��T-SQL���Ĳ���</returns>
        private SqlParameter[] BuildParameters()
        {
            return new[]
            {
                SqlServerHelper.BuildInParameter("@Id",SqlDbType.UniqueIdentifier, ForeignId)
            };
        }
    }
}