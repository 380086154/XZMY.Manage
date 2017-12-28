using System;
using System.Collections.Generic;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("PlanningNote")]
    public class PlanningNote : EntityBase, IDataModel
    {
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

        public List<Model.DataModel.Courses.CourseTemplate> ListCourseTemplate { get; set; }
        public List<Model.DataModel.Project.ProjectTemplate> ListProjectTemplate { get; set; }
        #region Collection

        #endregion
    }
}
