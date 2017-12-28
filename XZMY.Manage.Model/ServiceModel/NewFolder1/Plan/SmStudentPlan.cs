using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.ViewModel;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    [Serializable]
    [DataContract]
    public class SmStudentPlan : IActionViewModel<StudentPlan>
    {
        #region Properties 
        [DataMember]
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        [DataMember]
        public Guid StudentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Guid PlanningNoteId { get; set; }
        [DataMember]
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 当前年级
        /// </summary>
        [DataMember]
        public String Grade { get; set; }
        /// <summary>
        /// 当前年级的简写
        /// </summary>
        [DataMember]
        public String GradeShorthand {
            get {
                string strvalue = "";
                switch (Grade)
                {
                    case "小学一年级":
                        strvalue = "小一";
                        break;
                    case "小学二年级":
                        strvalue = "小二";
                        break;
                    case "小学三年级":
                        strvalue = "小三";
                        break;
                    case "小学四年级":
                        strvalue = "小四";
                        break;
                    case "小学五年级":
                        strvalue = "小五";
                        break;
                    case "小学六年级":
                        strvalue = "小六";
                        break;
                    case "初中一年级":
                        strvalue = "初一";
                        break;
                    case "初中二年级":
                        strvalue = "初二";
                        break;
                    case "初中三年级":
                        strvalue = "初三";
                        break;
                    case "高中一年级":
                        strvalue = "高一";
                        break;
                    case "高中二年级":
                        strvalue = "高二";
                        break;
                    case "高中三年级":
                        strvalue = "高三";
                        break;
                    case "预科":
                        strvalue = "预科";
                        break;
                }

                return strvalue;
            }
        }
        [DataMember]
        public String SchoolType { get; set; }
        [DataMember]
        public String SchoolPlace { get; set; }
        [DataMember]
        public Decimal Fee { get; set; }
        [DataMember]
        public Decimal EnglishScore { get; set; }
        [DataMember]
        public Decimal LearnScore { get; set; }
        [DataMember]
        public Decimal QualityScore { get; set; }
        [DataMember]
        public Decimal AddEnglishScore { get; set; }
        [DataMember]
        public Decimal AddLearnScore { get; set; }
        [DataMember]
        public Decimal AddQualityScore { get; set; }
        [DataMember]
        public int Sort { get; set; }
        /// <summary>
        /// 面向人群  小学 初中 高中
        /// </summary>
        [DataMember]
        public String SuitablePersonName
        {
            get
            {
                string strValue = "";
                if (Sort >= 7 && Sort <= 12)
                {
                    strValue = "小学";
                } else if (Sort >= 13 && Sort <= 15)
                {
                    strValue = "初中";
                }
                else if (Sort >= 16 && Sort <= 18)
                {
                    strValue = "高中";
                }
                return strValue;
            }
        }
        /// <summary>
        /// 面向人群 初中生 3, 高中生 4, 大学生 5
        /// </summary>
        public String SuitablePersonSort
        {
            get
            {
                string strValue = "小学";
                if (Sort >= 7 && Sort <= 12)
                {
                    strValue = "2";
                }
                else if (Sort >= 13 && Sort <= 15)
                {
                    strValue = "3";
                }
                else if (Sort >= 16 && Sort <= 18)
                {
                    strValue = "4";
                }
                else if (Sort >= 19 && Sort <= 22)
                {
                    strValue = "5";
                }
                return strValue;
            }
        }
            
            
        /// <summary>
        /// 年级中规划参加的活动或是课程
        /// </summary>
        public List<SmStudentPlanProgram> listStudentPlanProgram { get; set; }
        #endregion


        public StudentPlan CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new StudentPlan();
            //model.Id = Id;
            model.AddEnglishScore=AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.EnglishScore = EnglishScore;
            model.Fee = Fee;
            model.Grade = Grade;
            model.LearnScore = LearnScore;
            model.PlanningNoteId = PlanningNoteId;
            model.PlanRecordId = PlanRecordId;
            model.QualityScore = QualityScore;
            model.SchoolPlace = SchoolPlace;
            model.SchoolType = SchoolType;
            model.Sort = Sort;
            model.StudentId = StudentId;
            
            return model;
        }
        public StudentPlan MergeDataModel(StudentPlan model)
        {
            model.AddEnglishScore = AddEnglishScore;
            model.AddLearnScore = AddLearnScore;
            model.AddQualityScore = AddQualityScore;
            model.EnglishScore = EnglishScore;
            model.Fee = Fee;
            model.Grade = Grade;
            model.LearnScore = LearnScore;
            model.PlanningNoteId = PlanningNoteId;
            model.PlanRecordId = PlanRecordId;
            model.QualityScore = QualityScore;
            model.SchoolPlace = SchoolPlace;
            model.SchoolType = SchoolType;
            model.Sort = Sort;
            model.StudentId = StudentId;
            return model;
        }
    }
}
