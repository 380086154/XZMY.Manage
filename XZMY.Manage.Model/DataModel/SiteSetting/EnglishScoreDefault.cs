using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 英语分值
    /// </summary>
    [Serializable]
    [DBTable("EnglishScoreDefault")]
    public class EnglishScoreDefault : EntityBase, IDataModel
    {
        #region Properties 

        public Guid PlanningNoteId { get; set; }

        public String GradeName { get; set; }

        public int Sort { get; set; }

        public Guid GradeRankingId { get; set; }

        public String GradeRankingName { get; set; }

        public decimal EnglishScore { get; set; }
        

    #endregion
}
}
