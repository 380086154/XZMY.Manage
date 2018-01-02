using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    [Serializable]
    [DBTable("StudentVipApply")]
    public class StudentVipApply : EntityBase, IDataModel
    {
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
    }
}
