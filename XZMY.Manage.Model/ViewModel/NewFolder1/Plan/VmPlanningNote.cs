using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;

namespace XZMY.Manage.Model.ViewModel.Plan
{
    /// <summary>
    /// 年级节点
    /// </summary>
    [Serializable]
    public class VmPlanningNote : IActionViewModel<PlanningNote>
    {
        #region Properties 
        public Guid DataId { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        //[EntAttributes.DBColumn("Grade")] 
        //[DisplayName("年级名称")] 
        public String Grade { get; set; }
        /// <summary>
        /// 学校类型  1普通学校 2重点学校  3国际学校
        /// </summary>
        //[EntAttributes.DBColumn("SchoolType")] 
        //[DisplayName("学校类型")] 
        public String SchoolType { get; set; }
        /// <summary>
        /// 学校类型  1普通学校 2重点学校  3国际学校
        /// </summary>
        //[EntAttributes.DBColumn("SchoolTypeId")] 
        //[DisplayName("学校类型")] 
        public int SchoolTypeId { get; set; }
        /// <summary>
        /// 学校地点  国外 国内
        /// </summary>
        //[EntAttributes.DBColumn("SchoolPlace")] 
        //[DisplayName("学校地点")] 
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 总预算  
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("总预算")] 
        public Decimal Fee { get; set; }
        /// <summary>
        /// 需要英语分值  
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("需要英语分值")] 
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 需要学科分值  
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("需要学科分值")] 
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 需要素质分值  
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("需要素质分值")] 
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 增加英语分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddEnglishScore")] 
        //[DisplayName("增加英语分值")] 
        public Decimal AddEnglishScore { get; set; }
        /// <summary>
        /// 增加学科分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddLearnScore")] 
        //[DisplayName("增加学科分值")] 
        public Decimal AddLearnScore { get; set; }
        /// <summary>
        /// 增加素质分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddQualityScore")] 
        //[DisplayName("增加素质分值")] 
        public Decimal AddQualityScore { get; set; }
        /// <summary>
        /// 年级排序ID  
        /// </summary>
        //[EntAttributes.DBColumn("Sort")] 
        //[DisplayName("年级排序ID")] 
        public Int32 Sort { get; set; }
        /// <summary>
        /// 升学率
        /// </summary>
        public Int32 EnrollmentRate { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        #endregion

        #region Extendsions

        public PlanningNote CreateNewDataModel()
        {
            var model = new PlanningNote();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Grade = Grade;
            model.SchoolType = SchoolType;
            model.SchoolTypeId = SchoolTypeId;
            model.SchoolPlace = SchoolPlace;
            model.Fee = Fee;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.Sort = Sort;
            model.EnrollmentRate = EnrollmentRate;
            model.Description = Description;
            return model;
        }

        public PlanningNote MergeDataModel(PlanningNote model)
        {
            model.Grade = Grade;
            model.SchoolType = SchoolType;
            model.SchoolTypeId = SchoolTypeId;
            model.SchoolPlace = SchoolPlace;
            model.Fee = Fee;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.Sort = Sort;
            model.EnrollmentRate = EnrollmentRate;
            model.Description = Description;
            return model;
        }
        #endregion
    }
}
