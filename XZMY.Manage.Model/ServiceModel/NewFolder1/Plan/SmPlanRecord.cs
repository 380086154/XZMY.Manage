using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Plan;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Order;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.Members;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Model.ViewModel.Project;
using XZMY.Manage.Model.ViewModel.School;

namespace XZMY.Manage.Model.ServiceModel.Plan
{
    /// <summary>
    /// 学生规划
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmPlanRecord : IActionViewModel<PlanRecord>
    {
        #region Properties 
  
        public Guid DataId { get; set; }
        #region 学生基本信息
        /// <summary>
        /// 学生ID
        /// </summary>
        //[EntAttributes.DBColumn("StudentId")] 
        //[DisplayName("学生ID")] 
        [DataMember]
        public Guid StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        //[EntAttributes.DBColumn("StudentName")] 
        //[DisplayName("学生姓名")] 
        [DataMember]
        public String StudentName { get; set; }
        /// <summary>
        /// 学生对象
        /// </summary>
        public VmStudent modelStudent { get; set; }
        #endregion
        #region 规划师信息
        public VmPlannerEdit modelPlanner { get; set; }
        #endregion
        #region 留学意向
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
        /// 就读学校毕业时间
        /// </summary>
        [DataMember]
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// 分值JSON  EnglishItemRows英语，EnglishOtherItemRows其他英语
        /// </summary>
        [DataMember]
        public String JsonScore { get; set; }
        /// <summary>
        /// 素质分值 JSon
        /// </summary>
        [DataMember]
        public String JsonQualityScore { get; set; }
        /// <summary>
        /// 意向专业
        /// </summary>
        //[EntAttributes.DBColumn("major")] 
        //[DisplayName("意向专业")] 
        [DataMember]
        public String major { get; set; }
        /// <summary>
        /// 移民计划  1有 2无 3尚未决定
        /// </summary>
        //[EntAttributes.DBColumn("ImmigrationProgram")] 
        //[DisplayName("移民计划")] 
        [DataMember]
        public int ImmigrationProgram { get; set; }
        /// <summary>
        /// 移民计划  1有 2无 3尚未决定
        /// </summary>
        //[EntAttributes.DBColumn("ImmigrationProgram")] 
        //[DisplayName("移民计划")] 
        public String ImmigrationProgramName
        {
            get
            {
                string strValue = "尚未决定";
                switch (ImmigrationProgram)
                {
                    case 1:
                        strValue = "有";
                        break;
                    case 2:
                        strValue = "无";
                        break;
                    case 3:
                        strValue = "尚未决定";
                        break;
                }
                return strValue;
            }
        }

        /// <summary>
        /// 学生留学花费金额 留学预算
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("学生留学花费金额")] 
        [DataMember]
        public Decimal Fee { get; set; }
        #endregion
        #region 学生现状信息
        /// <summary>
        /// 爱好
        /// </summary>
        [DataMember]
        public string Hobby { get; set; }
        /// <summary>
        /// 特长
        /// </summary>
        [DataMember]
        public string Skill { get; set; }
        /// <summary>
        /// 当前就读学校名称
        /// </summary>
        [DataMember]
        public string School { get; set; }
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
        /// 年级排名  GradeRanking 表
        /// </summary>
        [DataMember]
        public Guid GradeRanking { get; set; }
        /// <summary>
        /// 规划师分析，给学生的分析
        /// </summary>
        [DataMember]
        public string Planning { get; set; }
        #endregion
        #region 学校信息  推荐学校介绍
        /// <summary>
        /// 推荐学校列表
        /// </summary>
        [DataMember]
        List<VmSchool> listSchool { get; set; }
        #endregion
        #region 评估结果
        /// <summary>
        /// 规划时学生英语成绩
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("规划时学生英语成绩")] 
        [DataMember]
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 英语指标成绩
        /// </summary>
        [DataMember]
        public List<AchievementIndex> listEnglishIndex { get; set; }
        /// <summary>
        /// 规划时学生学科成绩
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("规划时学生学科成绩")] 
        [DataMember]
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 学术指标成绩
        /// </summary>
        [DataMember]
        public List<AchievementIndex> listLearnIndex { get; set; }
        /// <summary>
        /// 规划时学生素质成绩
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("规划时学生素质成绩")] 
        [DataMember]
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 素质指标成绩
        /// </summary>
        [DataMember]
        public List<AchievementIndex> listQualityIndex { get; set; }
        #endregion
        #region 规划
        /// <summary>
        /// 规划年级节点
        /// </summary>
        public List<SmStudentPlan> listStudentPlan { get; set; }
        #endregion 
        /// <summary>
        /// 温馨提示
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 规划生成时间
        /// </summary>
        //[EntAttributes.DBColumn("PlanTime")] 
        //[DisplayName("规划生成时间")] 
        [DataMember]
        public DateTime PlanTime { get; set; }
        /// <summary>
        /// 学术加分项 有多条用逗号分隔
        /// </summary>
        [DataMember]
        public String  listLearn{ get; set; }

        /// <summary>
        /// 留学路线 1学霸路线  2稳妥路线 3经济路线
        /// </summary>
        [DataMember]
        public ERoute Route { get; set; }
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
        /// 当前就读学校类型 SchoolLevel 学校等级
        /// </summary>
        [DataMember]
        public Guid currentSchoolType { get; set; }
        /// <summary>
        /// 是否有效 1有效 2过期
        /// </summary>
        //[EntAttributes.DBColumn("IsValid")] 
        //[DisplayName("是否有效")] 
        [DataMember]
        public int IsValid { get; set; }
        [DataMember]
        public String IsValidName {
            get {
                string strValue = "无效";
                switch (IsValid) {
                    case 1:
                        strValue = "有效";
                        break;
                    case 2:
                        strValue = "无效";
                        break;
                }
                return strValue;
            }
        }
        /// <summary>
        /// 是否完成规划，1完成 2未完成
        /// </summary>
        //[EntAttributes.DBColumn("IsComplete")] 
        //[DisplayName("是否完成规划")] 
        [DataMember]
        public int IsComplete { get; set; }
        [DataMember]
        public String IsCompleteName {
            get {
                string strValue = "未确认";
                switch (IsComplete)
                {
                    case 1:
                        strValue = "确认";
                        break;
                    case 2:
                        strValue = "未确认";
                        break;
                }
                return strValue;
            }
        }

        /// <summary>
        /// 设置 获取 课程订单信息
        /// </summary>
        [DataMember]
        public List<SmCourseOrderCreate> listOrderCourse { get; set; }
        /// <summary>
        /// 设置 获取 活动订单信息
        /// </summary>
        [DataMember]
        public List<SmProjectOrderCreate> listOrderProject { get; set; }

        /// <summary>
        /// 设置 获取数据库内的全部国家信息
        /// </summary>
        [DataMember]
        public List<VmLocation> listCountry { get; set; }
        /// <summary>
        /// 设置 获取年级列表
        /// </summary>
        [DataMember]
        public List<SmPlanningNote> listSmPlanningNote { get; set; }
        //public ActionResult GetJsonlistSmPlanningNote {
        //    get {
        //        return JsonConvert.SerializeObject(listSmPlanningNote);
        //    }
        //}
        /// <summary>
        /// 设置 获取 全部活动模板
        /// </summary>
        [DataMember]
        public List<VmProjectTemplate> listProjectTemplate { get; set; }
        /// <summary>
        /// 设置 获取  全部课程模板
        /// </summary>
        [DataMember]
        public List<VmCourseTemplate> listCourseTemplate { get; set; }




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
        /// 目标学校类型ID
        /// </summary>
        public Guid SchoolTypeId { get; set; }
        [DataMember]
        public  DateTime CreatedTime { get; set; }
        #endregion


        public PlanRecord CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new PlanRecord();
            //model.Id = Id;
            model.StudentId = StudentId;
            model.StudentName = StudentName;
            model.PlanTime = PlanTime;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.Fee = Fee;
            model.TargetCountryId = TargetCountryId;
            model.TargetCountryName = TargetCountryName;
            model.EducationId = EducationId;
            model.EducationName = EducationName;
            model.GoAbroadEducationId = GoAbroadEducationId;
            model.GoAbroadEducationIName = GoAbroadEducationIName;
            model.IntentionalSchoolTop = IntentionalSchoolTop;
            model.IntentionalSchoolName = IntentionalSchoolName;
            model.major = major;
            model.ImmigrationProgram = ImmigrationProgram;
            model.GraduationDate = GraduationDate;
            model.JsonScore = JsonScore;
            model.JsonQualityScore = JsonQualityScore;
            model.Route = Route;
            model.IsValid = IsValid;
            model.IsComplete = IsComplete;
            model.FeeInterval = FeeInterval;
            model.listEnglishItem = listEnglishItem;
            model.listEnglishOtherItem = listEnglishOtherItem;
            model.SchoolTypeId = SchoolTypeId;
            model.listLearn = listLearn;
            model.currentSchoolType = currentSchoolType;
            model.GradeRanking = GradeRanking;
            return model;
        }
        public PlanRecord MergeDataModel(PlanRecord model)
        {
            if (model == null)
                model = new PlanRecord();
            model.StudentId = StudentId;
            model.StudentName = StudentName;
            model.PlanTime = PlanTime;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.Fee = Fee;
            model.TargetCountryId = TargetCountryId;
            model.TargetCountryName = TargetCountryName;
            model.EducationId = EducationId;
            model.EducationName = EducationName;
            model.GoAbroadEducationId = GoAbroadEducationId;
            model.GoAbroadEducationIName = GoAbroadEducationIName;
            model.IntentionalSchoolTop = IntentionalSchoolTop;
            model.IntentionalSchoolName = IntentionalSchoolName;
            model.major = major;
            model.ImmigrationProgram = ImmigrationProgram;
            model.GraduationDate = GraduationDate;
            model.JsonScore = JsonScore;
            model.JsonQualityScore = JsonQualityScore;
            model.Route = Route;
            model.IsValid = IsValid;
            model.IsComplete = IsComplete;
            model.FeeInterval = FeeInterval;
            model.listEnglishItem = listEnglishItem;
            model.listEnglishOtherItem = listEnglishOtherItem;
            model.SchoolTypeId = SchoolTypeId;
            model.listLearn = listLearn;
            model.currentSchoolType = currentSchoolType;
            model.GradeRanking = GradeRanking;
            return model;
        }


    }
    public class AchievementIndex {
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
