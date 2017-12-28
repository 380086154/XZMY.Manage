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
    [DBTable("Member")]
    public class Member : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 代理商ID 
        /// </summary>
        public Guid AgentId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        //[EntAttributes.DBColumn("LoginName")] 
        //[DisplayName("登录名")] 
        public String LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        //[EntAttributes.DBColumn("Password")] 
        //[DisplayName("登录密码")] 
        public String Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        //[EntAttributes.DBColumn("RealName")] 
        //[DisplayName("真实姓名")] 
        public String RealName { get; set; }
        /// <summary>
        /// 分类 ：1 学生  2 家长
        /// </summary>
        //[EntAttributes.DBColumn("Type")] 
        //[DisplayName("分类 ：1 学生  2 家长")] 
        public Int32 Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        public EGender Gender { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string GenderName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        public String Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("手机号")] 
        public String Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }        
        /// <summary>
        /// 注册时间
        /// </summary>
        //[EntAttributes.DBColumn("RegisteredTime")] 
        //[DisplayName("注册时间")] 
        public DateTime RegisteredTime { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}
