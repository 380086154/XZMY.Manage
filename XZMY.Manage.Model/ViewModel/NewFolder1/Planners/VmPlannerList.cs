using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmPlannerList : ViewBase, IActionViewModel<V_PlannerList>
    {
        public String Name { get; set; }
        public String Code { get; set; }
        public String LevelName { get; set; }
        public String QualificationsName { get; set; }
        public String LoginName { get; set; }
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }


        public V_PlannerList CreateNewDataModel()
        {
            var model = new V_PlannerList();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.Name = Name;
            model.Code = Code;
            model.LevelName = LevelName;
            model.QualificationsName = QualificationsName;
            model.LoginName = LoginName;
            model.State = State;
            return model;
        }

        public V_PlannerList MergeDataModel(V_PlannerList model)
        {
            model.Name = Name;
            model.Code = Code;
            model.LevelName = LevelName;
            model.QualificationsName = QualificationsName;
            model.LoginName = LoginName;
            model.State = State;
            return model;
        }
    }
}
