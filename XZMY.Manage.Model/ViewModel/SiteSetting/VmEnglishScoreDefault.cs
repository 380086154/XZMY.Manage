using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    [Serializable]
    public class VmEnglishScoreDefault : ViewBase, IActionViewModel<EnglishScoreDefault>
    {
        public Guid DataId { get; set; }
        #region Properties 
        public Guid PlanningNoteId { get; set; }

        public String GradeName { get; set; }

        public int Sort { get; set; }

        public Guid GradeRankingId { get; set; }

        public String GradeRankingName { get; set; }

        public decimal EnglishScore { get; set; }
        #endregion


        #region Extendsions

        public EnglishScoreDefault CreateNewDataModel()
        {
            var model = new EnglishScoreDefault();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.PlanningNoteId = PlanningNoteId;
            model.GradeName = GradeName;
            model.GradeRankingId = GradeRankingId;
            model.GradeRankingName = GradeRankingName;
            model.EnglishScore = EnglishScore;
            model.Sort = Sort;
            return model;
        }

        public EnglishScoreDefault MergeDataModel(EnglishScoreDefault model)
        {
            model.PlanningNoteId = PlanningNoteId;
            model.GradeName = GradeName;
            model.GradeRankingId = GradeRankingId;
            model.GradeRankingName = GradeRankingName;
            model.EnglishScore = EnglishScore;
            model.Sort = Sort;
            return model;
        }
        #endregion
    }
}
