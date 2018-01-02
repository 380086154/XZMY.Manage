using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("Planner")]
    public class Planner : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 规划师登录账号Id外键
        /// </summary>
        //[EntAttributes.DBColumn("UserId")] 
        //[DisplayName("规划师登录账号Id外键")] 
        public Guid UserId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("规划师姓名")] 
        public String Name { get; set; }
        /// <summary>
        /// 规划师编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("规划师编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 规划师照片 多张 用;分割
        /// </summary>
        //[EntAttributes.DBColumn("Pictures")] 
        //[DisplayName("规划师照片 多张 用;分割")] 
        public String Pictures { get; set; }
        /// <summary>
        /// 规划师资质  数据字典
        /// </summary>
        //[EntAttributes.DBColumn("QualificationsId")] 
        //[DisplayName("规划师资质  数据字典")] 
        public Guid QualificationsId { get; set; }
        /// <summary>
        /// 规划师资质  数据字典
        /// </summary>
        //[EntAttributes.DBColumn("QualificationsName")] 
        //[DisplayName("规划师资质  数据字典")] 
        public String QualificationsName { get; set; }
        /// <summary>
        /// 规划师级别 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("LevelId")] 
        //[DisplayName("规划师级别 数据字典")] 
        public Guid LevelId { get; set; }
        /// <summary>
        /// 规划师级别 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("LevelName")] 
        //[DisplayName("规划师级别 数据字典")] 
        public String LevelName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("描述")] 
        public String Description { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}