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
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("ProblemPlanner")]
    public class ProblemPlanner : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 提问人Id
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("提问人Id")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 提问人名字
        /// </summary>
        //[EntAttributes.DBColumn("MemberName")] 
        //[DisplayName("提问人名字")] 
        public String MemberName { get; set; }
        /// <summary>
        /// 规划师Id
        /// </summary>
        //[EntAttributes.DBColumn("PlannerId")] 
        //[DisplayName("规划师Id")] 
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师名字
        /// </summary>
        //[EntAttributes.DBColumn("PlannerName")] 
        //[DisplayName("规划师名字")] 
        public String PlannerName { get; set; }
        /// <summary>
        /// 状态  1未处理 2已处理
        /// </summary>
        //[EntAttributes.DBColumn("State")] 
        //[DisplayName("状态  1未处理 2已处理")] 
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 提问时间
        /// </summary>
        //[EntAttributes.DBColumn("QuestionTime")] 
        //[DisplayName("提问时间")] 
        public DateTime QuestionTime { get; set; }
        /// <summary>
        /// 回答时间
        /// </summary>
        //[EntAttributes.DBColumn("AnswerTime")] 
        //[DisplayName("回答时间")] 
        public DateTime AnswerTime { get; set; }
        public String QuestionTitle { get; set; }
        /// <summary>
        /// 提问
        /// </summary>
        //[EntAttributes.DBColumn("Question")] 
        //[DisplayName("提问")] 
        public String Question { get; set; }
        /// <summary>
        /// 回答
        /// </summary>
        //[EntAttributes.DBColumn("Answer")] 
        //[DisplayName("回答")] 
        public String Answer { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}
