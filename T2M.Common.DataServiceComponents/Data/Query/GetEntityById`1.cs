using System;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    /// <summary>
    /// 根据Id查询业务实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetEntityById<T> : MappingQuery<T>, IGetEntityById<T>, IQuery<T> where T : class ,IDataModel, IEntity<Guid>, new()
    {

        public GetEntityById() { }
        public GetEntityById(string tableName) { TableName = tableName; }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void BuildMapping()
        {
            MapAll();
        }

        public T Execute()
        {
            var properties = GetMappedProperties();
            var condition = GetCondition();
            var sqlParameter = BuildParameters();

            var sqlStatement = String.Format(QueryTemplate.QUERY_SELECT_WITH_CLAUSE,
                    String.Join(",", properties), TableName, condition);

            using (var reader = SqlServerHelper.ExecuteReader(SqlServerHelper.CONNECTION_STRING, CommandType.Text, sqlStatement, sqlParameter))
            {
                if (!reader.Read())
                    return null;

                return reader.ToModel<T>();
            }
        }

        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <returns></returns>
        private String GetCondition()
        {
            return "[DataId] = @Id";
        }

        /// <summary>
        /// 构建用于执行T-SQL语句的参数。
        /// </summary>
        /// <returns>一组用于执行T-SQL语句的参数</returns>
        private SqlParameter[] BuildParameters()
        {
            return new[]
            {
                SqlServerHelper.BuildInParameter("@Id",SqlDbType.UniqueIdentifier, Id)
            };
        }
    }
}
