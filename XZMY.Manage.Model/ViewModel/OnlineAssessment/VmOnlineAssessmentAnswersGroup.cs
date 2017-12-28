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
    public class VmOnlineAssessmentAnswersGroup : ViewBase, IActionViewModel<OnlineAssessmentAnswersGroup>
    {
        /// <summary>
        /// 问题ID
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
        /// <summary>
        /// 答案分组名称 
        /// </summary>
        public String GroupName { get; set; }

        public DateTime CreatedTime { get; set; }

        #region Extendsions

        public OnlineAssessmentAnswersGroup CreateNewDataModel()
        {
            var model = new OnlineAssessmentAnswersGroup();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.GroupName = GroupName;
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            return model;
        }

        public OnlineAssessmentAnswersGroup MergeDataModel(OnlineAssessmentAnswersGroup model)
        {
            model.GroupName = GroupName;
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            return model;
        }
        #endregion
    }
}
