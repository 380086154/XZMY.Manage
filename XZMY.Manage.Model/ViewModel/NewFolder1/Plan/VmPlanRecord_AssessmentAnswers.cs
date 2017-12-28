using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Plan
{
    /// <summary>
    /// 规划评估题答案
    /// </summary>
    [Serializable]
    public class VmPlanRecord_AssessmentAnswers : ViewBase, IActionViewModel<PlanRecord_AssessmentAnswers>
    {
        public Guid DataId { get; set; }
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


        #region Extendsions

        public PlanRecord_AssessmentAnswers CreateNewDataModel()
        {
            var model = new PlanRecord_AssessmentAnswers();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.AnswersId = AnswersId;
            model.AssessmentId = AssessmentId;
            model.PlanRecordId = PlanRecordId;
            model.QualityScore = QualityScore;
            model.ScoreContent = ScoreContent;
            return model;
        }

        public PlanRecord_AssessmentAnswers MergeDataModel(PlanRecord_AssessmentAnswers model)
        {
            model.AnswersId = AnswersId;
            model.AssessmentId = AssessmentId;
            model.PlanRecordId = PlanRecordId;
            model.QualityScore = QualityScore;
            model.ScoreContent = ScoreContent;
            return model;
        }
        #endregion
    }
}
