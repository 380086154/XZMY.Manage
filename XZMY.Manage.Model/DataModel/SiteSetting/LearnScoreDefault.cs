using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 学术分值
    /// </summary>
    [Serializable]
    [DBTable("LearnScoreDefault")]
    public class LearnScoreDefault : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 学习类型ID
        /// </summary>
        public Guid SchoolLevelId { get; set; }

        /// <summary>
        /// 学习类型名称
        /// </summary>
        public String SchoolLevelName { get; set; }

        /// <summary>
        /// 年级排名ID
        /// </summary>
        public Guid GradeRankingId { get; set; }
        /// <summary>
        /// 年级排名
        /// </summary>
        public String GradeRankingName { get; set; }
        /// <summary>
        /// 学术分值
        /// </summary>
        public decimal LearnScore { get; set; }

    #endregion

}
}
