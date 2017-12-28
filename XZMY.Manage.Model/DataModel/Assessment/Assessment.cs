using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Assessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("Assessment")]
    public class Assessment : EntityBase, IDataModel
    {
        #region Properties 
        /// <summary>
        /// 试卷名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 试卷编码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal TotalScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        #endregion
    }
}
