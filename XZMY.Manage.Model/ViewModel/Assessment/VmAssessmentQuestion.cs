using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.Common.Utils.Helper;

namespace XZMY.Manage.Model.ViewModel.Assessment
{
    [Serializable]
    public class VmAssessmentQuestion :ViewBase, IActionViewModel<AssessmentQuestions>
    {
        #region Properties 

        /// <summary>
        /// 问题id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("问题id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("标题")] 
        public String Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("内容")] 
        public String Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        //[EntAttributes.DBColumn("State")] 
        //[DisplayName("状态")] 
        public EState State { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        //[EntAttributes.DBColumn("EnableTime")] 
        //[DisplayName("启用时间")] 
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 废弃时间
        /// </summary>
        //[EntAttributes.DBColumn("DisableTime")] 
        //[DisplayName("废弃时间")] 
        public DateTime DisableTime { get; set; }

        /// <summary>
        /// 答案数
        /// </summary>
        public Int32 AnswerCount { get; set; }
        /// <summary>
        /// 所属试卷ID
        /// </summary>
        public Guid AssessmentId { get; set; }

        #endregion

        #region Extendsions

        public AssessmentQuestions CreateNewDataModel()
        {
            var model = new AssessmentQuestions();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Title = Title;
            model.Description = Description;
            model.State = EState.启用;
            model.EnableTime = EnableTime == DateTimePlus.GetMinDateTime() ? DateTime.Now: EnableTime;
            model.DisableTime = DisableTime;
            model.AssessmentId = AssessmentId;
            return model;
        }

        public AssessmentQuestions MergeDataModel(AssessmentQuestions model)
        {
            model.Title = Title;
            model.Description = Description;
            model.State = State == 0 ? EState.启用 : State;
            model.EnableTime = EnableTime;
            model.DisableTime = DisableTime;
            model.AssessmentId = AssessmentId;
            return model;
        }
        #endregion
    }
}
