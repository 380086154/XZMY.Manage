using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Assessment
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("AssessmentAnswers")]
    public class AssessmentAnswers : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 问题Id
        /// </summary>
        //[EntAttributes.DBColumn("QuestionsId")] 
        //[DisplayName("问题Id")] 
        public Guid QuestionsId { get; set; }
        /// <summary>
        /// 答案描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("答案描述")] 
        public String Description { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}