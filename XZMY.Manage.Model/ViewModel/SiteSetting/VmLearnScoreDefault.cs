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
    public class VmLearnScoreDefault : ViewBase, IActionViewModel<LearnScoreDefault>
    {
        public Guid DataId { get; set; }
        #region Properties 

        /// <summary>
        /// 学习类型ID
        /// </summary>
        public Guid SchoolLevelId { get; set; }

        /// <summary>
        /// 学习类型名称
        /// </summary>
        public String SchoolLevelName { get; set; }

        /// <summary>
        /// 年级排名ID
        /// </summary>
        public Guid GradeRankingId { get; set; }
        /// <summary>
        /// 年级排名
        /// </summary>
        public String GradeRankingName { get; set; }
        /// <summary>
        /// 学术分值
        /// </summary>
        public decimal LearnScore { get; set; }

        #endregion


        #region Extendsions

        public LearnScoreDefault CreateNewDataModel()
        {
            var model = new LearnScoreDefault();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.SchoolLevelId = SchoolLevelId;
            model.SchoolLevelName = SchoolLevelName;
            model.GradeRankingId = GradeRankingId;
            model.GradeRankingName = GradeRankingName;
            model.LearnScore = LearnScore;
            return model;
        }

        public LearnScoreDefault MergeDataModel(LearnScoreDefault model)
        {
            model.SchoolLevelId = SchoolLevelId;
            model.SchoolLevelName = SchoolLevelName;
            model.GradeRankingId = GradeRankingId;
            model.GradeRankingName = GradeRankingName;
            model.LearnScore = LearnScore;
            return model;
        }
        #endregion
    }
}
