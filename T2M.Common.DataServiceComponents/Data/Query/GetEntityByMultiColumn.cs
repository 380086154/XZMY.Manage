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
    public class GetEntityByMultiColumn<T> : MappingQuery<T>, IGetEntityByMultiColumn<T> where T : class, IDataModel, IEntity<Guid>, new()
    {
        public GetEntityByMultiColumn() { }
        public GetEntityByMultiColumn(string tableName) { TableName = tableName; }
        protected override void BuildMapping()
        {
            MapAll();
        }

        public IList<Tuple<Expression<Func<T, object>>, Guid>> ColumnMember { get; set; }

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
        /// ����ƴ��
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return String.Join(" AND ",
                ColumnMember.Select((m, index) => String.Format("{0} = @value" + index, m.Item1.GetExpressionMemberName())));
        }

        /// <summary>
        /// ��������ִ��T-SQL���Ĳ�����
        /// </summary>
        /// <returns>һ������ִ��T-SQL���Ĳ���</returns>
        private SqlParameter[] BuildParameters()
        {
            return ColumnMember.Select((m, index) =>
                SqlServerHelper.BuildInParameter("@value" + index, GetDatabaseValue(m.Item2))
                ).ToArray();
        }
    }
}