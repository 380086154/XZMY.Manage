
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.OnlineAssessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("OnlineAssessmentTranscript")]
    public class OnlineAssessmentTranscript : EntityBase, IDataModel
    {
        /// <summary>
        /// 学生真实姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学生答题登录名 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 学生答题登录密码 
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 答题开始时间 
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 答题结束时间 
        /// </summary>
        public DateTime EndTime { get; set; }
        public ETranscriptState State { get; set; }
        /// <summary>
        /// 答题试卷ID
        /// </summary>
        public Guid OnlineAssessmentSecurityId { get; set; }
    }
}
