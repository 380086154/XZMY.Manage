using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel.Members;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmStudentVipApply : IActionViewModel<StudentVipApply>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生ID")] 
        public Guid StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        //[EntAttributes.DBColumn("StudentName")] 
        //[DisplayName("学生姓名")]
        public String StudentName { get; set; }
        /// <summary>
        /// 规划师ID
        /// </summary>
        //[EntAttributes.DBColumn("PlannerId")] 
        //[DisplayName("规划师ID")]
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        //[EntAttributes.DBColumn("PlannerName")] 
        //[DisplayName("规划师姓名")]
        public String PlannerName { get; set; }
        /// <summary>
        /// 申请VIP时间
        /// </summary>
        //[EntAttributes.DBColumn("ApplyTime")] 
        //[DisplayName("申请VIP时间")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        //[EntAttributes.DBColumn("State")] 
        //[DisplayName("状态")]
        public EState State { get; set; }
        /// <summary>
        /// 获取状态名称
        /// </summary>
        public String StateName {
            get {
                string strValue = "申请VIP完成";
                switch (State)
                {
                    case XZMY.Manage.Model.Enum.EState.启用:
                        strValue = "待处理";
                        break;
                    case XZMY.Manage.Model.Enum.EState.禁用:
                        strValue = "已经处理";
                        break;
                }
                return strValue;
            }
        }
        /// <summary>
        /// 设置 获取 学生信息
        /// </summary>
        public VmStudent Student { get; set; } 

        /// <summary>
        /// 设置 获取 规划师信息
        /// </summary>
        public VmPlanner Planner { get; set; }
        #region Extendsions

        public StudentVipApply CreateNewDataModel()
        {
            var model = new StudentVipApply();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.StudentId= StudentId;
            model.StudentName= StudentName;
            model.PlannerId= PlannerId;
            model.PlannerName= PlannerName;
            model.ApplyTime = ApplyTime;
            model.State= State;
            return model;
        }

        public StudentVipApply MergeDataModel(StudentVipApply model)
        {
            model.StudentId = StudentId;
            model.StudentName = StudentName;
            model.PlannerId = PlannerId;
            model.PlannerName = PlannerName;
            model.ApplyTime = ApplyTime;
            model.State = State;
            return model;
        }
        #endregion
    }
}
