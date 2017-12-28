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
    public class VmOnlineAssessmentQuestions : ViewBase, IActionViewModel<OnlineAssessmentQuestions>
    {

        /// <summary>
        /// 试卷ID 
        /// </summary>
        public Guid OnlineAssessmentSecurityId { get; set; }
        /// <summary>
        /// 问题标题 
        /// </summary>
        public String  Title { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 问题状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 问题禁用时间
        /// </summary>
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 问题启用时间
        /// </summary>
        public DateTime DisableTime { get; set; }
        /// <summary>
        /// 问题答题时间（秒数）
        /// </summary>
        public int UseTime { get; set; }
        /// <summary>
        /// 问题题型
        /// </summary>
        public EOnlineAssessmentQuestionType Type { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        /// 问题排序从小到大
        /// </summary>
        public int Sort { get; set; }
        #region Extendsions

        public OnlineAssessmentQuestions CreateNewDataModel()
        {
            var model = new OnlineAssessmentQuestions();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.OnlineAssessmentSecurityId = OnlineAssessmentSecurityId;
            model.Title = Title;
            model.Description = Description;
            model.State = State;
            model.EnableTime = EnableTime;
            model.DisableTime = DisableTime;
            model.UseTime = UseTime;
            model.Type = Type;
            model.Sort = Sort;
            return model;
        }

        public OnlineAssessmentQuestions MergeDataModel(OnlineAssessmentQuestions model)
        {
            model.OnlineAssessmentSecurityId = OnlineAssessmentSecurityId;
            model.Title = Title;
            model.Description = Description;
            model.State = State;
            model.EnableTime = EnableTime;
            model.DisableTime = DisableTime;
            model.UseTime = UseTime;
            model.Type = Type;
            model.Sort = Sort;
            return model;
        }
        #endregion
    }
}
