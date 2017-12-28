using System;
using System.Data;
using XZMY.Manage.Service.Auth.Data.SqlServer.Query;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using T2M.Common.Utils.ADONET.SQLServer;

namespace XZMY.Manage.Service.Auth.Data.SqlServer.Impl
{
    public class UpdateActionBase : MappingQuery<Sys_Action>, IUpdateActionBase
    {
        public UpdateActionBase(Sys_Action model)
        {
            Model = model;
        }

        protected override void BuildMapping()
        {
            Map(m => m.DataId);
            Map(m => m.Name);
            Map(m => m.Sort);
            Map(m => m.State);
            //Map(m => m.ModifierId);
            //Map(m => m.ModifierName);
            //Map(m => m.ModifiedTime);
        }

        public Sys_Action Model { get; set; }

        public int Execute(IDbTransaction transaction)
        {
            var condition = GetCondition();
            var template = GetUpdateStringTemplate();
            var sqlParameter = GetModelParameters(Model);

            var sqlStatement = string.Format(QueryTemplate.QUERY_UPDATE_WITH_CLAUSE,
                TableName, string.Join(",", template), condition);
            
            var res = SqlServerHelper.ExecuteNonQuery(transaction, CommandType.Text, sqlStatement, sqlParameter);

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetCondition()
        {
            return "[DataId] = @DataId";
        }
    }
}
