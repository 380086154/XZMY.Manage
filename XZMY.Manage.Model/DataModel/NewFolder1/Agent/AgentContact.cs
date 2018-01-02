using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Agent
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class AgentContact : EntityBase, IDataModel
    {
        #region Properties 

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

        #region Collection

        #endregion
    }
}