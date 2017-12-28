
using System;
using XZMY.Manage.Model.DataModel.Agent;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Agent
{
    [Serializable]
    public class VmAgentContact : IActionViewModel<AgentContact>
    {
        #region Properties 

        /// <summary>
        /// 代理商联系人主键
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("代理商联系人主键")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 代理商Id
        /// </summary>
        //[EntAttributes.DBColumn("AgentId")] 
        //[DisplayName("代理商Id")] 
        public Guid AgentId { get; set; }
        /// <summary>
        /// 是否主要联系人
        /// </summary>
        //[EntAttributes.DBColumn("IsMain")] 
        //[DisplayName("是否主要联系人")] 
        public bool IsMain { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("姓名")] 
        public String Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("手机号")] 
        public String Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        public String Email { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        //[EntAttributes.DBColumn("QQ")] 
        //[DisplayName("QQ")] 
        public String QQ { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        //[EntAttributes.DBColumn("Tel")] 
        //[DisplayName("座机")] 
        public String Tel { get; set; }
        /// <summary>
        /// 代理商联系人描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("代理商联系人描述")] 
        public String Description { get; set; }

        #endregion

        #region Extendsions

        public AgentContact CreateNewDataModel()
        {
            var model = new AgentContact();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.AgentId = AgentId;
            model.IsMain = IsMain;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.QQ = QQ;
            model.Tel = Tel;
            model.Description = Description;
            return model;
        }

        public AgentContact MergeDataModel(AgentContact model)
        {
            model.AgentId = AgentId;
            model.IsMain = IsMain;
            model.Name = Name;
            model.Mobile = Mobile;
            model.Email = Email;
            model.QQ = QQ;
            model.Tel = Tel;
            model.Description = Description;
            return model;
        }
        #endregion
    }

}

