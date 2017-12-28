using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Members
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("V_StudentMember")]
    public class V_StudentMember_List : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        /// 规划师ID
        /// </summary>
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        public String PlannerName { get; set; }
        public Guid ParentsId { get; set; }
        public string Mobile { get; set; }
        public string Grade { get; set; }
        public string LocationPathName { get; set; }
        public decimal AverageScore { get; set; }
        public DateTime RegistedTime { get; set; }
    }
}
