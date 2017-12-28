using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("PlanRecord_AssessmentAnswers")]
    public class PlanRecord_AssessmentAnswers : EntityBase, IDataModel
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 评估答题ID
        /// </summary>
        public Guid AssessmentId { get; set; }
        /// <summary>
        /// 评估题答案
        /// </summary>
        public Guid AnswersId { get; set; }
        /// <summary>
        /// 素质得分
        /// </summary>
        public decimal QualityScore { get; set; }
        /// <summary>
        /// 得分明细
        /// </summary>
        public String ScoreContent { get; set; }
    }
}
