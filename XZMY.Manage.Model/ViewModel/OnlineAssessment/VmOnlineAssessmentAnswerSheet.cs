using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.OnlineAssessment;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.OnlineAssessment
{
    [Serializable]
    public class VmOnlineAssessmentAnswerSheet : ViewBase, IActionViewModel<OnlineAssessmentAnswerSheet>
    {
        /// <summary>
        /// 答题成绩单ID
        /// </summary>
        public Guid OnlineAssessmentTranscriptId { get; set; }
        /// <summary>
        /// 答题问题ID
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
        /// <summary>
        /// 答题答案ID
        /// </summary>
        public Guid OnlineAssessmentAnswersId { get; set; }
        /// <summary>
        /// 分组答案ID
        /// </summary>
        public Guid OnlineAssessmentAnswersGroupId { get; set; }
    /// <summary>
    /// 答题答案描述
    /// </summary>
    public Guid AnswerDescription { get; set; }
        #region Extendsions

        public OnlineAssessmentAnswerSheet CreateNewDataModel()
        {
            var model = new OnlineAssessmentAnswerSheet();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.OnlineAssessmentTranscriptId = OnlineAssessmentTranscriptId;
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            model.OnlineAssessmentAnswersId = OnlineAssessmentAnswersId;
            model.OnlineAssessmentAnswersGroupId = OnlineAssessmentAnswersGroupId;
            model.AnswerDescription = AnswerDescription;
            return model;
        }

        public OnlineAssessmentAnswerSheet MergeDataModel(OnlineAssessmentAnswerSheet model)
        {
            model.OnlineAssessmentTranscriptId = OnlineAssessmentTranscriptId;
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            model.OnlineAssessmentAnswersId = OnlineAssessmentAnswersId;
            model.OnlineAssessmentAnswersGroupId = OnlineAssessmentAnswersGroupId;
            model.AnswerDescription = AnswerDescription;
            return model;
        }
        #endregion
    }
}
