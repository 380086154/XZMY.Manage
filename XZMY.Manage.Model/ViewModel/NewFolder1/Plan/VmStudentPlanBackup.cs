using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Plan
{
    /// <summary>
    /// 学生规划表备份表
    /// </summary>
    [Serializable]
    public class VmStudentPlanBackup : ViewBase, IActionViewModel<StudentPlanBackup>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid StudentPlanId { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public String Grade { get; set; }
        /// <summary>
        /// 学校类型名称  普通学校 重点学校
        /// </summary>
        public String SchoolType { get; set; }
        /// <summary>
        /// 学校地点 国外 国内
        /// </summary>
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 预算学习费用
        /// </summary>
        public Decimal Fee { get; set; }
        /// <summary>
        /// 英语分值
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学术分值
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质分值
        /// </summary>
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 增加英语分值
        /// </summary>
        public Decimal AddEnglishScore { get; set; }
        /// <summary>
        /// 增加学术分值
        /// </summary>
        public Decimal AddLearnScore { get; set; }
        /// <summary>
        /// 增加素质分值
        /// </summary>
        public Decimal AddQualityScore { get; set; }
        /// <summary>
        /// 年级ID 排序
        /// </summary>
        public int Sort { get; set; }

        #region Extendsions

        public StudentPlanBackup CreateNewDataModel()
        {
            var model = new StudentPlanBackup();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.StudentPlanId = StudentPlanId;
            model.Grade = Grade;
            model.SchoolType = SchoolType;
            model.SchoolPlace = SchoolPlace;
            model.Fee = Fee;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.Sort = Sort;
            return model;
        }

        public StudentPlanBackup MergeDataModel(StudentPlanBackup model)
        {
            model.StudentPlanId = StudentPlanId;
            model.Grade = Grade;
            model.SchoolType = SchoolType;
            model.SchoolPlace = SchoolPlace;
            model.Fee = Fee;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.Sort = Sort;
            return model;
        }
        #endregion
    }
}
