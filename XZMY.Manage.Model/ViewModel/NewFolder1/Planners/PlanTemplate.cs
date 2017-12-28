using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    public class PlanTemplate
    {

        #region 学生基本信息
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生年龄
        /// </summary>
        public string StudentAge { get; set; }
        /// <summary>
        /// 学生性别
        /// </summary>
        public string StudentGender { get; set; }
        /// <summary>
        /// 规划创建时间
        /// </summary>
        public string PlanCreatedTime { get; set; }

        #endregion

        #region 规划师信息
        /// <summary>
        /// 规划师照片
        /// </summary>
        public Stream PlannerImage { get; set; }
        /// <summary>
        /// 规划师名称
        /// </summary>
        public string PlannerName { get; set; }
        /// <summary>
        /// 规划师资质
        /// </summary>
        public IList<string> PlannerQuality { get; set; }
        /// <summary>
        /// 规划师等级
        /// </summary>
        public IList<string> PlannerHonor { get; set; }
        /// <summary>
        /// 规划师介绍
        /// </summary>
        public string PlannerDescription { get; set; }
        #endregion



        #region 学生留学意向信息
        /// <summary>
        /// 意向国家名称
        /// </summary>
        public string TargetCountry { get; set; }
        /// <summary>
        /// 意向学校类型
        /// </summary>
        public string TargetSchoolType { get; set; }
        /// <summary>
        /// 留学预算费用
        /// </summary>
        public decimal Budget { get; set; }
        #endregion



        #region 学生信息
        /// <summary>
        /// 学生爱好
        /// </summary>
        public string Hobby { get; set; }
        /// <summary>
        /// 学生技能
        /// </summary>
        public string Skill { get; set; }
        /// <summary>
        /// 当前就读学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 当前就读学校年级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 规划师给学生的介绍
        /// </summary>
        public string Planning { get; set; }


        #endregion

        #region 学校信息
        /// <summary>
        /// 推荐学校列表
        /// </summary>
        public IList<PlanningSchool> Schools { get; set; }

        ///// <summary>
        ///// 没得数据的
       // /// </summary>
        //public SchoolComparetion SchoolComparetion { get; set; }

        #endregion

        #region 评估结果
        /// <summary>
        /// 规划时学生英语成绩
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("规划时学生英语成绩")] 
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 英语指标成绩
        /// </summary>
        public List<AchievementIndex> listEnglishIndex { get; set; }
        /// <summary>
        /// 规划时学生学科成绩
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("规划时学生学科成绩")] 
        public Decimal LearnScore { get; set; } 
        /// <summary>
        /// 学术指标成绩
        /// </summary>
        public List<AchievementIndex> listLearnIndex { get; set; }
        /// <summary>
        /// 规划时学生素质成绩
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("规划时学生素质成绩")] 
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 素质指标成绩
        /// </summary>
        public List<AchievementIndex> listQualityIndex { get; set; }
        #endregion

        #region 规划

        public PlanningPath PlanningPath { get; set; }

        /// <summary>
        /// 规划师温馨提示
        /// </summary>
        public string PlannerRecommand { get; set; }
        /// <summary>
        /// 辅导员建议（VIP）
        /// </summary>
        public string PlannerRecommand2 { get; set; }
        /// <summary>
        /// 辅导员分析点评（VIP）
        /// </summary>
        public string PlannerRecommand3 { get; set; }
        #endregion
    }





    public class PlanningPath
    {
        public IList<PathPoint> Points { get; set; }
        public string Remark { get; set; }

    }
    /// <summary>
    /// 学校年级
    /// </summary>
    public class PathPoint
    {
        /// <summary>
        /// 学校年级名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动或课程名称列表
        /// </summary>
        public List<string> Projects { get; set; }
        /// <summary>
        /// 排序 从小到大
        /// </summary>
        public int Sort { get; set; }
    }
    /// <summary>
    /// 规划推荐学校
    /// </summary>
    public class PlanningSchool
    {
        /// <summary>
        /// 学校名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学校LOGO图片
        /// </summary>
        public Stream Logo { get; set; }
        /// <summary>
        /// 学校图片
        /// </summary>
        public IList<Stream> Images { get; set; }
        /// <summary>
        /// 学校所在地区
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 学校排名
        /// </summary>
        public string Ranking { get; set; }
        /// <summary>
        /// 学校成立时间
        /// </summary>
        public string CreatedTime { get; set; }
        /// <summary>
        /// 学校介绍
        /// </summary>
        public string Description { get; set; }
    }


    //public class SchoolComparetion
    //{
    //    public IList<SchoolScore> Schools { get; set; }
    //    public IList<string> ParameterNames { get; set; }
    //}

    //public class SchoolScore
    //{
    //    public string Name { get; set; }
    //    public IList<string> Scores { get; set; }
    //}


    public class AchievementIndex
    {
        /// <summary>
        /// 指标名称 科目
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public Boolean IsPass { get; set; }
        /// <summary>
        /// 考试成绩
        /// </summary>
        public Decimal Score { get; set; }
    }
}
