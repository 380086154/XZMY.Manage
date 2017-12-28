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
    public class VmOnlineAssessmentSecurity : ViewBase, IActionViewModel<OnlineAssessmentSecurity>
    {
        /// <summary>
        /// 试卷名称 
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 试卷编码
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public String StateName  { get; set; }
        /// <summary>
        /// 用时（秒）
        /// </summary>
        public int UseTime { get; set; }


        #region Extendsions

        public OnlineAssessmentSecurity CreateNewDataModel()
        {
            var model = new OnlineAssessmentSecurity();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Code = Code;
            model.TotalScore = TotalScore;
            model.State = State;
            model.UseTime = UseTime;
            return model;
        }

        public OnlineAssessmentSecurity MergeDataModel(OnlineAssessmentSecurity model)
        {
            model.Name = Name;
            model.Code = Code;
            model.TotalScore = TotalScore;
            model.State = State;
            model.UseTime = UseTime;
            return model;
        }
        #endregion
    }
}
