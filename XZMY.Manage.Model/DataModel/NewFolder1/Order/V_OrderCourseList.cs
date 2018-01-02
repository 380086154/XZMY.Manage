using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Order
{
    [Serializable]
    [DBTable("V_OrderCourseList")]
    public class V_OrderCourseList : EntityBase, IDataModel
    {
        public String OrderNo { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CancelTime { get; set; }

        public EOrderProcessState OrderCourseProcessState { get; set; }
        /// <summary>
        /// 流程状态名称
        /// </summary>
        public string OrderCourseProcessStateName  { get; set; }

        public String CourseName { get; set; }
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String Email { get; set; }
        public String Education { get; set; }
        public Guid PlannerId { get; set; }
        public String PlannerName { get; set; }
        /// <summary>
        /// 家长ID
        /// </summary>
        public Guid ParentsId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
    }
    //public enum OrderCourseState
    //{
    //    已报名 = 1,
    //    活动中 = 2,
    //    已结束 = 3,
    //    已取消 = 4
    //}
}
