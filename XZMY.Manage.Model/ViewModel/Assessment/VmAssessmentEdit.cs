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
    /// 编辑问题及答案
    /// </summary>
    [Serializable]
    public class VmAssessmentEdit : IActionViewModel2M<AssessmentQuestions>
    {
        #region Properties 

        /// <summary>
        /// 答案表主键id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("答案表主键id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 答案描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("答案描述")] 
        public String Description { get; set; }

        #endregion

        #region Extendsions

        public AssessmentQuestions MergeDataModel(AssessmentQuestions model)
        {
            model.Description = Description;
            return model;
        }

        #endregion
    }
}
