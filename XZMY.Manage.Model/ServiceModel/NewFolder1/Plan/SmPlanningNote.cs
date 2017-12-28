using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    [Serializable]
    [DataContract]
    public class SmPlanningNote : IActionServiceModel2C<PlanningNote>
    {
        #region Properties 
        [DataMember]
        public Guid DataId { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        //[EntAttributes.DBColumn("Grade")] 
        //[DisplayName("年级名称")] 
        [DataMember]
        public String Grade { get; set; }
        /// <summary>
        /// 学校类型  1普通学校 2重点学校  3国际学校
        /// </summary>
        //[EntAttributes.DBColumn("SchoolType")] 
        //[DisplayName("学校类型")] 
        [DataMember]
        public String SchoolType { get; set; }
        /// <summary>
        /// 学校类型  1普通学校 2重点学校  3国际学校
        /// </summary>
        //[EntAttributes.DBColumn("SchoolTypeId")] 
        //[DisplayName("学校类型")] 
        [DataMember]
        public int SchoolTypeId { get; set; }
        /// <summary>
        /// 学校地点  国外 国内
        /// </summary>
        //[EntAttributes.DBColumn("SchoolPlace")] 
        //[DisplayName("学校地点")] 
        [DataMember]
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 总预算  
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("总预算")] 
        [DataMember]
        public Decimal Fee { get; set; }
        /// <summary>
        /// 需要英语分值  
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("需要英语分值")] 
        [DataMember]
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 需要学科分值  
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("需要学科分值")] 
        [DataMember]
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 需要素质分值  
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("需要素质分值")] 
        [DataMember]
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 增加英语分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddEnglishScore")] 
        //[DisplayName("增加英语分值")] 
        [DataMember]
        public Decimal AddEnglishScore { get; set; }
        /// <summary>
        /// 增加学科分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddLearnScore")] 
        //[DisplayName("增加学科分值")] 
        [DataMember]
        public Decimal AddLearnScore { get; set; }
        /// <summary>
        /// 增加素质分值  
        /// </summary>
        //[EntAttributes.DBColumn("AddQualityScore")] 
        //[DisplayName("增加素质分值")] 
        [DataMember]
        public Decimal AddQualityScore { get; set; }
        /// <summary>
        /// 年级排序ID  
        /// </summary>
        //[EntAttributes.DBColumn("Sort")] 
        //[DisplayName("年级排序ID")] 
        [DataMember]
        public Int32 Sort { get; set; }
      
        #endregion
        public PlanningNote CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new PlanningNote();
            //model.Id = Id;
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.EnglishScore = EnglishScore;
            model.Fee = Fee;
            model.Grade = Grade;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.SchoolPlace = SchoolPlace;
            model.SchoolType = SchoolType;
            model.SchoolTypeId = SchoolTypeId;
            model.Sort = Sort;
            return model;
        }
    }
}
