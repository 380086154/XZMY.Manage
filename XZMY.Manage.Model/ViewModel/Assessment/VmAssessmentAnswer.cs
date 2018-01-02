using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;

namespace XZMY.Manage.Model.ViewModel.Assessment
{
    /// <summary>
    /// 问题答案
    /// </summary>
    [Serializable]
    public class VmAssessmentAnswer : IActionViewModel<AssessmentAnswers>
    {
        #region Properties 

        /// <summary>
        /// 答案表主键id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("答案表主键id")] 
        public Guid DataId { get; set; }
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

        public List<VmScore> Score { get; set; }

        #endregion

        #region Extendsions

        public AssessmentAnswers CreateNewDataModel()
        {
            var model = new AssessmentAnswers();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.QuestionsId = QuestionsId;
            model.Description = Description;
            return model;
        }

        public AssessmentAnswers MergeDataModel(AssessmentAnswers model)
        {
            model.QuestionsId = QuestionsId;
            model.Description = Description;
            return model;
        }

        public List<Scores> CreateScoreDataModels()
        {
            return Score.Select(m => m.CreateNewDataModel()).ToList();
        }
        #endregion
    }
}
