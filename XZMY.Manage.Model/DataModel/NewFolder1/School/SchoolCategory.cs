using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.School
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class SchoolCategory : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 父级Id
        /// </summary>
        //[EntAttributes.DBColumn("ParentId")] 
        //[DisplayName("父级Id")] 
        public Guid ParentId { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("类别名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 类别编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("类别编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 层级名称
        /// </summary>
        //[EntAttributes.DBColumn("PathName")] 
        //[DisplayName("层级名称")] 
        public String PathName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        //[EntAttributes.DBColumn("Level")] 
        //[DisplayName("级别")] 
        public Int32 Level { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}