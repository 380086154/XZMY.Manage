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
    [DBTable("OnlineAssessmentAnswers")]
    public class OnlineAssessmentAnswers : EntityBase, IDataModel
    {
        /// <summary>
        /// 问题ID 
        /// </summary>
        public Guid OnlineAssessmentQuestionsId { get; set; }
        /// <summary>
        /// 答案描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public String Picture { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
       



    }
}
