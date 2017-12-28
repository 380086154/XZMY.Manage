using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    /// <summary>
    /// 留学预算
    /// </summary>
    public class VmStudyBudget : ViewBase, IActionViewModel<StudyBudget>
    {
        /// <summary>
        /// 留学预算名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public int Budget { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public String StateName  { get; set; }
        public StudyBudget CreateNewDataModel()
        {
            var model = new StudyBudget();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Budget = Budget;
            model.State = State;
            return model;
        }

        public StudyBudget MergeDataModel(StudyBudget model)
        {
            model.Name = Name;
            model.Budget = Budget;
            model.State = State;
            return model;
        }
    }
}
