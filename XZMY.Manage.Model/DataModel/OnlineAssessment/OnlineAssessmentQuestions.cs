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
    [DBTable("OnlineAssessmentQuestions")]
    public class OnlineAssessmentQuestions : EntityBase, IDataModel
    {

        /// <summary>
        /// 试卷ID 
        /// </summary>
        public Guid OnlineAssessmentSecurityId { get; set; }
        /// <summary>
        /// 问题标题 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 问题状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 问题禁用时间
        /// </summary>
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 问题启用时间
        /// </summary>
        public DateTime DisableTime { get; set; }
        /// <summary>
        /// 问题答题时间（秒数）
        /// </summary>
        public int UseTime { get; set; }
        /// <summary>
        /// 问题题型
        /// </summary>
        public EOnlineAssessmentQuestionType Type { get; set; }
        /// <summary>
        /// 问题排序从小到大
        /// </summary>
        public int Sort { get; set; }
    }
}
