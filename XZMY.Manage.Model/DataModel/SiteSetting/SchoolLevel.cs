using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 学校类型
    /// </summary>
    [Serializable]
    [DBTable("SchoolLevel")]
    public class SchoolLevel : EntityBase, IDataModel
    {
        #region Properties 
        /// <summary>
        /// 类型名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int32 Code { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }

        #endregion
    }
}
