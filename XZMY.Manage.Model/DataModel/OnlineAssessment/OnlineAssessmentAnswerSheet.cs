
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
    [DBTable("OnlineAssessmentAnswerSheet")]
    public class OnlineAssessmentAnswerSheet : EntityBase, IDataModel
    {
        /// <summary>
        /// 答题成绩单ID
        /// </summary>
        public Guid OnlineAssessmentTranscriptId { get; set; }
        /// <summary>
        /// 答题问题ID
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
        /// <summary>
        /// 答题答案ID
        /// </summary>
        public Guid OnlineAssessmentAnswersId { get; set; }
        /// <summary>
        /// 分组答案ID
        /// </summary>
        public Guid OnlineAssessmentAnswersGroupId { get; set; }
        /// <summary>
        /// 答题答案描述
        /// </summary>
        public Guid AnswerDescription { get; set; }

    }
}
