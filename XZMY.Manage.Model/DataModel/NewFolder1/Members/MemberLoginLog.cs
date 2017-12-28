using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Members
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class MemberLoginLog : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 登录人Id
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("登录人Id")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        //[EntAttributes.DBColumn("LoginTime")] 
        //[DisplayName("登录时间")] 
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        //[EntAttributes.DBColumn("LoginName")] 
        //[DisplayName("登录名称")] 
        public String LoginName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        //[EntAttributes.DBColumn("IP")] 
        //[DisplayName("IP地址")] 
        public String IP { get; set; }
        /// <summary>
        /// 浏览器名称
        /// </summary>
        //[EntAttributes.DBColumn("Browser")] 
        //[DisplayName("浏览器名称")] 
        public String Browser { get; set; }
        /// <summary>
        /// 登录地点
        /// </summary>
        //[EntAttributes.DBColumn("Location")] 
        //[DisplayName("登录地点")] 
        public String Location { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}