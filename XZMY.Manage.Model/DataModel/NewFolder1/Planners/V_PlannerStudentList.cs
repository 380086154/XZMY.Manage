using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    [Serializable]
    [DBTable("V_PlannerStudentList")]
    public class V_PlannerStudentList : EntityBase, IDataModel
    {
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
        /// 规划师编号
        /// </summary>
        //[EntAttributes.DBColumn("PlannerCode")] 
        //[DisplayName("规划师编号")] 
        public String PlannerCode { get; set; }
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
        /// 学生手机号
        /// </summary>
        //[EntAttributes.DBColumn("StudentMobile")] 
        //[DisplayName("学生手机号")] 
        public String StudentMobile { get; set; }
        /// <summary>
        /// 学生当前年级
        /// </summary>
        //[EntAttributes.DBColumn("StudentGrade")] 
        //[DisplayName("学生当前年级")] 
        public String StudentGrade { get; set; }
        /// <summary>
        /// 学生家长ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentParentsId")] 
        //[DisplayName("学生家长ID")] 
        public Guid StudentParentsId { get; set; }
        /// <summary>
        /// 学生提交课程订单数量
        /// </summary>
        //[EntAttributes.DBColumn("StudentOrderCourseCount")] 
        //[DisplayName("学生提交课程订单数量")] 
        public Int32 StudentOrderCourseCount { get; set; }
        /// <summary>
        /// 学生提交活动订单数量
        /// </summary>
        //[EntAttributes.DBColumn("StudentOrderProjectCount")] 
        //[DisplayName("学生提交活动订单数量")] 
        public Int32 StudentOrderProjectCount { get; set; }
    }
}
