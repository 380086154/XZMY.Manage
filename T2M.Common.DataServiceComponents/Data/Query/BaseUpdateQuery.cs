using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.DataServiceComponents.Data.Impl.Query
{
    public class BaseUpdateQuery<T> : MappingQuery<T>, IBaseUpdateQuery<T> where T : class,IDataModel, new()
    {

        public BaseUpdateQuery() { }
        public BaseUpdateQuery(string tableName) { TableName = tableName; }

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
        public bool Execute()
        {
            using (var conn = new SqlConnection(SqlServerHelper.CONNECTION_STRING))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {

                    var bl = Execute(transaction);
                    transaction.Commit();
                    return bl;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Execute(IDbTransaction transaction)
        {
            var condition = GetCondition();
            var template = GetUpdateStringTemplate();
            var sqlParameter = GetModelParameters(Model);

            var sqlStatement = String.Format(QueryTemplate.QUERY_UPDATE_WITH_CLAUSE,
                TableName, String.Join(",", template), condition);

            Console.WriteLine(sqlStatement);

            var res = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text, sqlStatement, sqlParameter);

            return res > 0;
        }


        private String GetCondition()
        {
            return "[DataId] = @DataId";
        }
    }
}