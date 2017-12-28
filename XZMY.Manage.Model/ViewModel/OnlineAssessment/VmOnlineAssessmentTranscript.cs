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
    public class VmOnlineAssessmentTranscript : ViewBase, IActionViewModel<OnlineAssessmentTranscript>
    {
        /// <summary>
        /// 学生真实姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学生答题登录名 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 学生答题登录密码 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 答题开始时间 
        /// </summary>
        public DateTime BeginTime { get; set; }
        public string BeginTimeStr
        {
            get
            {
                if (State == ETranscriptState.完成 || State == ETranscriptState.开始答题)
                    return BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                return "--";

            }
        }
        /// <summary>
        /// 答题结束时间 
        /// </summary>
        public DateTime EndTime { get; set; }
        public string EndTimeStr
        {
            get
            {
                if (State == ETranscriptState.完成)
                    return EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                return "--";

            }
        }

        /// <summary>
        /// 答题试卷ID
        /// </summary>
        public Guid OnlineAssessmentSecurityId { get; set; }
        public ETranscriptState State { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StateName
        {
            get
            {
                string str = "未开始";
                switch (State)
                {
                    case ETranscriptState.开始答题:
                        str = "开始答题";
                        break;
                    case ETranscriptState.未开始:
                        str = "未开始";
                        break;
                    case ETranscriptState.完成:
                        str = "完成";
                        break;
                    default:
                        break;
                }
                return str;
            }
        }

        /// <summary>
        /// 答题用时
        /// </summary>
        public string UsedTime
        {
            get
            {
                if (State == ETranscriptState.完成)
                {
                    TimeSpan ts = BeginTime.Subtract(EndTime).Duration();
                    string dateDiff = ts.Days == 0 ? "" : ts.Days.ToString() + "天";
                    dateDiff = (ts.Hours == 0 && dateDiff == "") ? "" : dateDiff + ts.Hours.ToString() + "小时 ";
                    dateDiff = (ts.Minutes == 0 && dateDiff == "") ? "" : dateDiff + ts.Minutes.ToString() + "分";
                    dateDiff = (ts.Seconds == 0 && dateDiff == "") ? "" : dateDiff + ts.Seconds.ToString() + "秒";
                    return dateDiff;
                }
                return "--";
            }
        }
        /// <summary>
        ///  试卷名称
        /// </summary>
        public string OnlineAssessmentSecurityName { get; set; } 

        #region Extendsions

        public OnlineAssessmentTranscript CreateNewDataModel()
        { 
            var model = new OnlineAssessmentTranscript();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.BeginTime = BeginTime;
            model.EndTime = EndTime;
            model.State = State;
            model.LoginName = LoginName;
            model.Name = Name;
            model.Password = Password;
            model.OnlineAssessmentSecurityId = OnlineAssessmentSecurityId;
            return model;
        }

        public OnlineAssessmentTranscript MergeDataModel(OnlineAssessmentTranscript model)
        {
            model.BeginTime = BeginTime;
            model.EndTime = EndTime;
            model.State = State;
            model.LoginName = LoginName;
            model.Name = Name;
            model.Password = Password;
            model.OnlineAssessmentSecurityId = OnlineAssessmentSecurityId;
            return model;
        }
        #endregion
    }
}
