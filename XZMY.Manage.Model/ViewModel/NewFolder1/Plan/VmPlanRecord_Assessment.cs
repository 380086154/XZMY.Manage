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
    /// 规划评估题
    /// </summary>
    [Serializable]
    public class VmPlanRecord_Assessment : ViewBase,IActionViewModel<PlanRecord_Assessment>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 获得总分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime AssessmentTime { get; set; }


        #region Extendsions

        public PlanRecord_Assessment CreateNewDataModel()
        {
            var model = new PlanRecord_Assessment();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.AssessmentTime = AssessmentTime;
            model.PlanRecordId = PlanRecordId;
            model.Score = Score;
            model.StudentId = StudentId;
            return model;
        }

        public PlanRecord_Assessment MergeDataModel(PlanRecord_Assessment model)
        {
            model.AssessmentTime = AssessmentTime;
            model.PlanRecordId = PlanRecordId;
            model.Score = Score;
            model.StudentId = StudentId;
            return model;
        }
        #endregion
    }
}
