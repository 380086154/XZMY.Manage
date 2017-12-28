using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ServiceModel.Assessment;
using XZMY.Manage.Model.ServiceModel.Plan;

namespace XZMY.Manage.Model.Utils
{
    public class PlanQuery : ViewBase
    {
        /// <summary>
        /// 设置 获取  当前年级
        /// </summary>
        public EGrade Grade { get; set; }
        /// <summary>
        /// 设置 获取 当前学校类型
        /// </summary>
        public ESchoolType SchoolType { get; set; }
        public string SchoolTypeName  { get; set; }
        /// <summary>
        /// 设置 获取  出国年级
        /// </summary>
        public EGradeAbroad GradeAbroad { get; set; }
        /// <summary>
        /// 设置 获取 年预算费用
        /// </summary>
        public Int32 AnnualBudget { get; set; }
        /// <summary>
        /// 获取 总预算费用 
        /// </summary>
        public Int32 GeneralBudget
        {
            get
            {
                int Amount = 0;
                if (AnnualBudget > 0)
                {
                    if ((int)GradeAbroad > (int)Grade)
                    {
                        Amount = ((int)GradeAbroad - (int)Grade) * AnnualBudget;
                    }
                }
                return Amount;
            }
        }
        /// <summary>
        /// 设置 获取 当前英语得分
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 设置 获取 当前学科得分
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 设置 获取 当前素质 得分
        /// </summary>
        public Decimal QualityScore { get; set; }
    }
    /// <summary>
    /// 留学规划_规划 第3步 初次规划获取规划信息 规划年级和活动和课程
    /// </summary>
    public class SetPlanCreatePlanQuery
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 路线 学霸路线 = 1, 稳妥路线 = 2, 经济路线 = 3
        /// </summary>
        public int Route { get; set; }
    }
    /// <summary>
    /// 创建规划活动
    /// </summary>
    public class AddStudentPlanProgramQuery
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 规划年级ID
        /// </summary>
        public Guid StudentPlanID { get; set; }
        /// <summary>
        /// 年级规划活动模板对象ID
        /// </summary>
        public String listPlanProgramTemplateId { get; set; }
    }
    
    /// <summary>
    /// 删除规划活动
    /// </summary>
    public class DelStudentPlanProgramQuery
    {
        /// <summary>
        /// 年级规划活动ID
        /// </summary>
        public Guid StudentPlanProgramId { get; set; }
    }
    /// <summary>
    /// 留学规划_评估中心提交答案完成 第2步提交数据
    /// </summary>
    public class SetPlanAssessmentAnswerQuery
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 选着答案ID 用， 逗号分隔  
        /// </summary>
        public String AnswerIds { get; set; }
        /// <summary>
        /// 答案列表
        /// </summary>
        public List<SmAssessmentAnswer> listModel { get; set; }
    }
    /// <summary>
    /// 留学规划_评估中心提交答案未完成Pass 第2步提交数据
    /// </summary>
    public class SetPlanAssessmentAnswerPassQuery {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
    }
    /// <summary>
    /// 获取指定规划
    /// </summary>
    public class GetPlanRecordQuery {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
    }
    /// <summary>
    /// 获取指定规划年级的活动列表
    /// </summary>
    public class PlanRecordStudentGradeProgramQuery {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }
        /// <summary>
        /// 规划年级ID
        /// </summary>
        public Guid PlanStudentGradeId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public String ItemName { get; set; }
        /// <summary>
        /// 是否素质能力  1 活动 2课程
        /// </summary>
        public int IsQuality { get; set; }
        

    }
    /// <summary>
    /// 修改指定规划年级
    /// </summary>
    public class PlanRecordStudentGradeProgramEidtQuery {
        /// <summary>
        /// 规划年级ID
        /// </summary>
        public Guid PlanStudentGradeId { get; set; }
        /// <summary>
        /// 规划年级学校类型 普通学校 重点学校 国际学校
        /// </summary>
        public String SchoolType { get; set; }
        /// <summary>
        /// 规划年级分地  国内  国外
        /// </summary>
        public String SchoolPlace { get; set; }
        /// <summary>
        /// 是否重新规划活动  是 或是 否
        /// </summary>
        public String IsPlanProgram { get; set; }
    }

    /// <summary>
    /// 留学规划_规划 第3步 查询模板列表
    /// </summary>
    public class ProgramTemplateQuery {
        /// <summary>
        /// 规划年级Id
        /// </summary>
        public Guid StudentPlanId { get; set; }
        /// <summary>
        /// 年级序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 1 活动 2课程
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 历练成长名称
        /// </summary>
        public String ItemName { get; set; }
    }
}
