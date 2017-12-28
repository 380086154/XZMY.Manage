using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    /// <summary>
    /// 学生成绩的总分数
    /// </summary>
    public class VmStudentTotalScore : ViewBase, IActionViewModel<StudentTotalScore>
    {
        /// <summary>
        /// 英语总分
        /// </summary>
        public int EnglishScore { get; set; }
        /// <summary>
        /// 学术总分
        /// </summary>
        public int LearnScore { get; set; }
        /// <summary>
        /// 素质总分
        /// </summary>
        public int QualityScore { get; set; }

        public StudentTotalScore CreateNewDataModel()
        {
            var model = new StudentTotalScore();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            return model;
        }

        public StudentTotalScore MergeDataModel(StudentTotalScore model)
        {
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            return model;
        }
    }
}
