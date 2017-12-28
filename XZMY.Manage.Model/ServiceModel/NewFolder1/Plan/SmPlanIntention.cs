using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.Enum;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    /// <summary>
    /// 留学规划之留学意向
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmPlanIntention 
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        [DataMember]
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        [DataMember]
        public Guid StudentId { get; set; }
        /// <summary>
        /// 留学目标国家地区ID
        /// </summary>
        //[EntAttributes.DBColumn("TargetCountryId")] 
        //[DisplayName("留学目标国家地区ID")] 
        [DataMember]
        public Guid TargetCountryId { get; set; }
        /// <summary>
        /// 留学目标国家名称
        /// </summary>
        //[EntAttributes.DBColumn("TargetCountryName")] 
        //[DisplayName("留学目标国家名称")] 
        [DataMember]
        public String TargetCountryName { get; set; }
        /// <summary>
        /// 当前学历年级ID
        /// </summary>
        //[EntAttributes.DBColumn("EducationId")] 
        //[DisplayName("当前学历年级ID")] 
        [DataMember]
        public Guid EducationId { get; set; }
        /// <summary>
        /// 当前学历年级名称
        /// </summary>
        //[EntAttributes.DBColumn("EducationName")] 
        //[DisplayName("当前学历年级名称")] 
        [DataMember]
        public String EducationName { get; set; }
        /// <summary>
        /// 出国年级ID
        /// </summary>
        //[EntAttributes.DBColumn("GoAbroadEducationId")] 
        //[DisplayName("出国年级ID")] 
        [DataMember]
        public Guid GoAbroadEducationId { get; set; }
        /// <summary>
        /// 出国年级名称
        /// </summary>
        //[EntAttributes.DBColumn("GoAbroadEducationIName")] 
        //[DisplayName("出国年级名称")] 
        [DataMember]
        public String GoAbroadEducationIName { get; set; }
        /// <summary>
        /// 学生留学花费金额 留学预算
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("学生留学花费金额")] 
        [DataMember]
        public Decimal Fee { get; set; } 
        /// <summary>
        /// 留学预算区间
        /// </summary>
        [DataMember]
        public String FeeInterval { get; set; }
        /// <summary>
        /// 学校排名 1：top1-10,2：top11-20,3：top21-50,4：top51以上
        /// </summary>
        //[EntAttributes.DBColumn("IntentionalSchoolTop")] 
        //[DisplayName("学校排名")] 
        [DataMember]
        public int IntentionalSchoolTop { get; set; }
        
        /// <summary>
        /// 意向学校名称
        /// </summary>
        //[EntAttributes.DBColumn("IntentionalSchoolName")] 
        //[DisplayName("意向学校名称")] 
        [DataMember]
        public String IntentionalSchoolName { get; set; }
        /// <summary>
        /// 年级排名
        /// </summary>
        [DataMember]
        public Guid GradeRanking { get; set; }
        /// <summary>
        /// 当前就读学校名称
        /// </summary>
        [DataMember]
        public String SchoolName { get; set; }
        /// <summary>
        /// 当前就读学校类型  SchoolLevel
        /// </summary>
        [DataMember]
        public Guid currentSchoolType { get; set; }
        /// <summary>
        /// 当前就读专业
        /// </summary>
        [DataMember]
        public String MajorName { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        [DataMember]
        public DateTime GraduationDate { get; set; }

        /// <summary>
        /// 毕业时间 只读  格式：2019-05-05
        /// </summary>
        [DataMember]
        public String GraduationDateStr
        {
            get
            {
                string strDate = "";
                strDate = GraduationDate.ToStringFormat("yyyy-MM-dd");
                return strDate;
            }
        }
        /// <summary>
        /// 学术成绩 在线平均成绩
        /// </summary>
        [DataMember]
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 学术加分项
        /// </summary>
        [DataMember]
        public String listLearn { get; set; }

        /// <summary>
        /// 英语成绩  ItemName:,ListeningScore:,VerbalScore:,ReadingScore:,WritingScore:,ItemTotalScore:|
        /// </summary>
        [DataMember]
        public String listEnglishItem { get; set; }
        /// <summary>
        /// 英语其他成绩  格式：ItemName:,ItemTotalScore:|ItemName:,ItemTotalScore:
        /// </summary>
        [DataMember]
        public String listEnglishOtherItem { get; set; }
        /// <summary>
        /// 目标学校类型 来源数据表【SchoolType】
        /// </summary>
        [DataMember]
        public String SchoolTypeId { get; set; }
        
    }
    /// <summary>
    /// 英语成绩
    /// </summary>
    [Serializable]
    [DataContract]
    public class EnglishItemScore {
        /// <summary>
        /// 分数项目名称
        /// </summary>
        [DataMember]
        public String ItemName { get; set; }
        /// <summary>
        /// 听力分数
        /// </summary>
        [DataMember]
        public String ListeningScore { get; set; }
        /// <summary>
        /// 口语分数
        /// </summary>
        [DataMember]
        public String VerbalScore { get; set; }
        /// <summary>
        /// 阅读分数
        /// </summary>
        [DataMember]
        public String ReadingScore { get; set; }
        /// <summary>
        /// 写作分数
        /// </summary>
        [DataMember]
        public String WritingScore { get; set; }
        /// <summary>
        /// 总分
        /// </summary>
        [DataMember]
        public String ItemTotalScore { get; set; }

    }
    /// <summary>
    /// 英语其他成绩
    /// </summary>
    [Serializable]
    [DataContract]
    public class EnglishOtherItemScore
    {
        /// <summary>
        /// 分数项目名称
        /// </summary>
        [DataMember]
        public String ItemName { get; set; }
        /// <summary>
        /// 总分
        /// </summary>
        [DataMember]
        public String ItemTotalScore { get; set; }

    }
    /// <summary>
    /// 素质分值
    /// </summary>
    [Serializable]
    [DataContract]
    public class QualityItemScore {
        /// <summary>
        /// 分数项目名称
        /// </summary>
        [DataMember]
        public String ItemName { get; set; }
        /// <summary>
        /// 总分
        /// </summary>
        [DataMember]
        public String ItemTotalScore { get; set; }
    }
}
