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
    public class VmOnlineAssessmentStudent : ViewBase, IActionViewModel<OnlineAssessmentStudent>
    {

        /// <summary>
        /// 学生姓名 
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 登录名 
        /// </summary>
        public String LoginName { get; set; }
        /// <summary>
        /// 密码 
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 状态 
        /// </summary>
        public EState State { get; set; }
        public String StateName  { get; set; }
        public DateTime CreatedTime { get; set; }
        #region Extendsions

        public OnlineAssessmentStudent CreateNewDataModel()
        {
            var model = new OnlineAssessmentStudent();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.LoginName = LoginName;
            model.Password = Password;
            model.State = State;
            return model;
        }

        public OnlineAssessmentStudent MergeDataModel(OnlineAssessmentStudent model)
        {
            model.Name = Name;
            model.LoginName = LoginName;
            model.Password = Password;
            model.State = State;
            return model;
        }
        #endregion
    }
}
