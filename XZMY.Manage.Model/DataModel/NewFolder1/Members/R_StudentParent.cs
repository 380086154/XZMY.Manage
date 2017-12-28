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
    [DBTable("R_StudentParents")]
    public class R_StudentParent : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 学生表主键ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生表主键ID")] 
        public Guid StudentId { get; set; }
        /// <summary>
        /// 家长表主键ID
        /// </summary>
        //[EntAttributes.DBColumn("ParentsId")] 
        //[DisplayName("家长表主键ID")] 
        public Guid ParentsId { get; set; }
        /// <summary>
        /// 关系  1 父子 2父女  3母子  4母女
        /// </summary>
        //[EntAttributes.DBColumn("Relationship")] 
        //[DisplayName("关系  1 父子 2父女  3母子  4母女")] 
        public Int32 Relationship { get; set; }
        /// <summary>
        /// 是否主要关系
        /// </summary>
        //[EntAttributes.DBColumn("IsMain")] 
        //[DisplayName("是否主要关系")] 
        public Int32 IsMain { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}