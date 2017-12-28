using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.OnlineAssessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("OnlineAssessmentAnswersGroup")]
    public class OnlineAssessmentAnswersGroup : EntityBase, IDataModel
    {
        /// <summary>
        /// 答案分组名称 
        /// </summary>
        public String GroupName { get; set; }
        /// <summary>
        /// 问题ID
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
    }
}
