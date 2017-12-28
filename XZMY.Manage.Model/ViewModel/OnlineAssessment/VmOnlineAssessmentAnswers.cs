using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.OnlineAssessment;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.OnlineAssessment
{
    [Serializable]
    public class VmOnlineAssessmentAnswers : ViewBase, IActionViewModel<OnlineAssessmentAnswers>
    {
        /// <summary>
        /// 问题ID 
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
        /// <summary>
        /// 答案描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public String Picture { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public DateTime CreatedTime { get; set; }
        #region Extendsions

        public OnlineAssessmentAnswers CreateNewDataModel()
        {
            var model = new OnlineAssessmentAnswers();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            model.Description = Description;
            model.Picture = Picture;
            model.State = State;
            return model;
        }

        public OnlineAssessmentAnswers MergeDataModel(OnlineAssessmentAnswers model)
        {
            model.OnlineAssessmentQuestionsId = OnlineAssessmentQuestionsId;
            model.Description = Description;
            model.Picture = Picture;
            model.State = State;
            return model;
        }
        #endregion
    }
}
