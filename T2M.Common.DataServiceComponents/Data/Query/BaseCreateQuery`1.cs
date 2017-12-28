using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class BaseCreateQuery<T> : MappingQuery<T>, IBaseCreateQuery<T> where T : class,IDataModel, new()
    {

        public BaseCreateQuery() { }
        public BaseCreateQuery(string tableName) { TableName = tableName; }

        protected override void BuildMapping()
        {
            MapAll();
        }

        /// <summary>
        /// 业务实体
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public T Execute()
        {
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    Execute(transaction);
                    transaction.Commit();
                    return Model;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public T Execute(IDbTransaction transaction)
        {
            var properties = GetMappedProperties();
            var paramsValue = GetParamterPlaceholders();
            var sqlParameter = GetModelParameters(Model);

            var sqlStatement = String.Format(QueryTemplate.QUERY_INSERT,
                                   TableName, String.Join(",", properties), String.Join(",", paramsValue));

            var res = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text, sqlStatement, sqlParameter);
            if (res == 0) 
                throw new DataException(String.Format("Insert {0} failed", EntityType.FullName));

            return Model;
        }
    }
}
