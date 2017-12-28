using System;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    /// <summary> 
    /// 规划访客记录
    /// </summary> 
    [Serializable]
    [DBTable("PlanVisitor")]
    public class PlanVisitor : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public String Mobile { get; set; }
        /// <summary>
        /// 联系人邮件
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// 意向国家名字
        /// </summary>
        public String Country { get; set; }
        /// <summary>
        /// 出国时间
        /// </summary>
        public DateTime AbroadDate { get; set; }
        /// <summary>
        /// 当前年级
        /// </summary>
        public String Grade { get; set; }

    }
}
