using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class GetEntityCountByMultiForeignId<T> : IGetEntityCountByMultiForeignId<T> where T : class, IDataModel
    {

        public GetEntityCountByMultiForeignId() { }
        public GetEntityCountByMultiForeignId(string tableName) { TableName = tableName; }


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


        public IList<Tuple<Expression<Func<T, object>>, Guid>> ForeignMember { get; set; }

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
        /// ����ƴ��
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return String.Join(" AND ",
                ForeignMember.Select((m, index) => String.Format("{0} = @Id" + index, PredicateUtils.GetExpressionMemberName<T>(m.Item1))));
        }

        /// <summary>
        /// ��������ִ��T-SQL���Ĳ�����
        /// </summary>
        /// <returns>һ������ִ��T-SQL���Ĳ���</returns>
        private SqlParameter[] BuildParameters()
        {
            return ForeignMember.Select((m, index) =>
                SqlServerHelper.BuildInParameter("@Id" + index, SqlDbType.UniqueIdentifier, m.Item2)
                ).ToArray();
        }
    }
}