using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    /// <summary> 
    /// 学生规划记录
    /// </summary> 
    [Serializable]
    [DBTable("PlanRecord")]
    public class PlanRecord : EntityBase, IDataModel
    {
        #region Properties 
        /// <summary>
        /// 学生ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生ID")] 
        public Guid StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        //[EntAttributes.DBColumn("StudentName")] 
        //[DisplayName("学生姓名")] 
        public String StudentName { get; set; }
        /// <summary>
        /// 规划生成时间
        /// </summary>
        //[EntAttributes.DBColumn("PlanTime")] 
        //[DisplayName("规划生成时间")] 
        public DateTime PlanTime { get; set; }
        /// <summary>
        /// 规划时学生英语成绩
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("规划时学生英语成绩")] 
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 规划时学生学科成绩
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("规划时学生学科成绩")] 
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 学术加分项 有多条用逗号分隔
        /// </summary>
        public String listLearn { get; set; }
        /// <summary>
        /// 规划时学生素质成绩
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("规划时学生素质成绩")] 
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 学生留学花费金额
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("学生留学花费金额")] 
        public Decimal Fee { get; set; }
        /// <summary>
        /// 留学目标国家地区ID
        /// </summary>
        //[EntAttributes.DBColumn("TargetCountryId")] 
        //[DisplayName("留学目标国家地区ID")] 
        public Guid TargetCountryId { get; set; }
        /// <summary>
        /// 留学目标国家名称
        /// </summary>
        //[EntAttributes.DBColumn("TargetCountryName")] 
        //[DisplayName("留学目标国家名称")] 
        public String TargetCountryName { get; set; }
        /// <summary>
        /// 当前学历年级ID
        /// </summary>
        //[EntAttributes.DBColumn("EducationId")] 
        //[DisplayName("当前学历年级ID")] 
        public Guid EducationId { get; set; }
        /// <summary>
        /// 当前学历年级名称
        /// </summary>
        //[EntAttributes.DBColumn("EducationName")] 
        //[DisplayName("当前学历年级名称")] 
        public String EducationName { get; set; }
        /// <summary>
        /// 出国年级ID
        /// </summary>
        //[EntAttributes.DBColumn("GoAbroadEducationId")] 
        //[DisplayName("出国年级ID")] 
        public Guid GoAbroadEducationId { get; set; }
        /// <summary>
        /// 出国年级名称
        /// </summary>
        //[EntAttributes.DBColumn("GoAbroadEducationIName")] 
        //[DisplayName("出国年级名称")] 
        public String GoAbroadEducationIName { get; set; }
        /// <summary>
        /// 学校排名 1：top1-10,2：top11-20,3：top21-50,4：top51以上
        /// </summary>
        //[EntAttributes.DBColumn("IntentionalSchoolTop")] 
        //[DisplayName("学校排名")] 
        public int IntentionalSchoolTop { get; set; }
        /// <summary>
        /// 意向学校名称
        /// </summary>
        //[EntAttributes.DBColumn("IntentionalSchoolName")] 
        //[DisplayName("意向学校名称")] 
        public String IntentionalSchoolName { get; set; }
        /// <summary>
        /// 意向专业
        /// </summary>
        //[EntAttributes.DBColumn("major")] 
        //[DisplayName("意向专业")] 
        public String major { get; set; }
        /// <summary>
        /// 移民计划  1有 2无 3尚未决定
        /// </summary>
        //[EntAttributes.DBColumn("ImmigrationProgram")] 
        //[DisplayName("移民计划")] 
        public int ImmigrationProgram { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// 分值JSON  EnglishItemRows英语，EnglishOtherItemRows其他英语
        /// </summary>
        public String JsonScore { get; set; }
        /// <summary>
        /// 素质分值 JSon
        /// </summary>
        public String JsonQualityScore { get; set; }
        /// <summary>
        /// 留学路线 1学霸路线  2稳妥路线 3经济路线
        /// </summary>
        public ERoute Route { get; set; }
        /// <summary>
        /// 是否有效 1有效 2过期
        /// </summary>
            //[EntAttributes.DBColumn("IsValid")] 
            //[DisplayName("是否有效")] 
        public int IsValid { get; set; }
        /// <summary>
        /// 是否完成规划，1完成 2未完成
        /// </summary>
        //[EntAttributes.DBColumn("IsComplete")] 
        //[DisplayName("是否完成规划")] 
        public int IsComplete { get; set; }
        /// <summary>
        /// 是留学预算区间   比如   10000-50000
        /// </summary>

        public String FeeInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String listEnglishItem { get; set; }
        public String listEnglishOtherItem { get; set; }
        /// <summary>
        /// 目标学校类型
        /// </summary>
        public Guid SchoolTypeId { get; set; }
        /// <summary>
        /// 当前就读学校类型 SchoolLevel 学校等级
        /// </summary>
        public Guid currentSchoolType { get; set; }
        /// <summary>
        /// 年级排名  GradeRanking 表ID
        /// </summary>
        public Guid GradeRanking { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}
