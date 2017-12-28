using System;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Agent
{
    /// <summary>
    /// 代理商与联系人视图
    /// </summary>
    public class V_AgentUserAgentContact : EntityBase, IDataModel
    {
        public String UserLoginName { get; set; }
        public String UserCode { get; set; }
        public String UserState { get; set; }
        public String AgentAddress { get; set; }
        public String AgentLocationPathName { get; set; }
        public String AgentDescription { get; set; }
        public String AgentBankAccount { get; set; }
        public String AgentBankNumber { get; set; }
        public String AgentBankFullName { get; set; }
        public String AgentBankName { get; set; }
        public String AgentCategoryId { get; set; }
        public String AgentNatureId { get; set; }
        public String AgentLevelId { get; set; }
        public String AgentLegalMobile { get; set; }
        public String AgentLegalPerson { get; set; }
        public String AgentCompanyName { get; set; }
        public String UserId { get; set; }
        public String AgentLocationId { get; set; }
        public String AgentCommission { get; set; }
        public String AgentContactCreatorName { get; set; }
        public String AgentContactCreatorId { get; set; }
        public String AgentContactCreatedTime { get; set; }
        public String AgentContactDescription { get; set; }
        public String AgentContactTel { get; set; }
        public String AgentContactEmail { get; set; }
        public String AgentContactQQ { get; set; }
        public String AgentContactMobile { get; set; }
        public String AgentContactName { get; set; }
        public String AgentContactId { get; set; }
    }
}