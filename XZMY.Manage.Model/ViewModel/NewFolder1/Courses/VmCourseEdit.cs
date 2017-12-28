using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Courses
{
    [Serializable]
    public class VmCourseEdit : ViewBase, IActionViewModel<Course>
    {

        public Guid DataId { get; set; }

        #region Properties 

        /// <summary>
        /// 课程类型，课程线路  数据字典  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("CourseType")] 
        //[DisplayName("课程类型，课程线路  数据字典  如 夏令营")] 
        public Guid CourseTypeId { get; set; }
        /// <summary>
        /// 课程类型，课程线路  数据字典描述  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("CourseTypeName")] 
        //[DisplayName("课程类型，课程线路  数据字典描述  如 夏令营")] 
        public String CourseTypeName { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("课程名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 项目编码  国家+年月日+编号（5位自增）
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("项目编码  国家+年月日+编号（5位自增）")] 
        public String Code { get; set; }
        /// <summary>
        /// 宣传图片  多张图片 用;分割  格式如：https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png;https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png
        /// </summary>
        //[EntAttributes.DBColumn("Pictures")] 
        //[DisplayName("宣传图片  多张图片 用;分割  格式如：https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png;https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png")] 
        public String Pictures { get; set; }
        /// <summary>
        /// 课程面向人群分类   如：初中生,高中生,大学生
        /// </summary>
        //[EntAttributes.DBColumn("SuitablePerson")] 
        //[DisplayName("课程面向人群分类   如：初中生,高中生,大学生")] 
        public String SuitablePerson { get; set; }

        public String SuitablePersonName
        {
            get
            {
                StringHelper sh = new StringHelper();
                return sh.GetSuitablePerson(SuitablePerson);
            }
        }
        /// <summary>
        /// 课程地点 描述  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("CoursePlaceName")] 
        //[DisplayName("课程地点 描述  如:中国-上海、美国-纽约")] 
        public String CoursePlaceName { get; set; }
        /// <summary>
        /// 课程地点  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("CoursePlaceLocationId")] 
        //[DisplayName("课程地点  如:中国-上海、美国-纽约")] 
        public Guid CoursePlaceLocationId { get; set; }
        /// <summary>
        /// 主办方
        /// </summary>
        //[EntAttributes.DBColumn("Sponsor")] 
        //[DisplayName("主办方")] 
        public String Sponsor { get; set; }
        /// <summary>
        /// 推荐指数  1~5
        /// </summary>
        //[EntAttributes.DBColumn("RecommendedIndex")] 
        //[DisplayName("推荐指数  1~5")] 
        public Int32 RecommendedIndex { get; set; }
        /// <summary>
        /// 推荐理由
        /// </summary>
        public String RecommendedReason { get; set; }
        /// <summary>
        /// 包含服务： 用,逗号分隔   课程所包含的服务信息标识  住宿 签证 保险
        /// </summary>
        //[EntAttributes.DBColumn("Service")] 
        //[DisplayName("包含服务： 用,逗号分隔   课程所包含的服务信息标识  住宿 签证 保险")] 
        public String Service { get; set; }

        /// <summary>
        /// 项目费用 市场价格标价
        /// </summary>
        //[EntAttributes.DBColumn("MarketPrice")] 
        //[DisplayName("项目费用 市场价格标价")] 
        public Decimal MarketPrice { get; set; }
        /// <summary>
        /// 课程价格，实际价格
        /// </summary>
        //[EntAttributes.DBColumn("ActualPrice")] 
        //[DisplayName("课程价格，实际价格")] 
        public Decimal ActualPrice { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        //[EntAttributes.DBColumn("DepositPrice")] 
        //[DisplayName("定金")] 
        public Decimal DepositPrice { get; set; }
        /// <summary>
        /// 折扣,帮助营销使用
        /// </summary>
        //[EntAttributes.DBColumn("Discount")] 
        //[DisplayName("折扣,帮助营销使用")] 
        public Decimal Discount { get; set; }
        /// <summary>
        /// 项目介绍   如:项目详细描述
        /// </summary>
        //[EntAttributes.DBColumn("CourseDescription")] 
        //[DisplayName("项目介绍   如:项目详细描述")] 
        public String CourseDescription { get; set; }
        /// <summary>
        /// 行程安排
        /// </summary>
        //[EntAttributes.DBColumn("Schedule")] 
        //[DisplayName("行程安排")] 
        public String Schedule { get; set; }
        /// <summary>
        /// 费用说明  费用说明清单描述
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("费用说明  费用说明清单描述")] 
        public String Fee { get; set; }
        /// <summary>
        /// 住宿安排描述
        /// </summary>
        //[EntAttributes.DBColumn("Stay")] 
        //[DisplayName("住宿安排描述")] 
        public String Stay { get; set; }
        /// <summary>
        /// 签证说明描述
        /// </summary>
        //[EntAttributes.DBColumn("Visa")] 
        //[DisplayName("签证说明描述")] 
        public String Visa { get; set; }
        /// <summary>
        /// 行程须知
        /// </summary>
        //[EntAttributes.DBColumn("Stroke")] 
        //[DisplayName("行程须知")] 
        public String Stroke { get; set; }
        /// <summary>
        /// 安全保障
        /// </summary>
        //[EntAttributes.DBColumn("Security")] 
        //[DisplayName("安全保障")] 
        public String Security { get; set; }

        /// <summary>
        /// 课程模板Id
        /// </summary>
        public Guid CourseTemplateId { get; set; }
        /// <summary>
        /// 难度系数
        /// </summary>
        public int DifficultyValue { get; set; }
        /// <summary>
        /// 完成系数
        /// </summary>
        public int CompletionValue { get; set; }
        /// <summary>
        /// 性价比
        /// </summary>
        public int FeeValue { get; set; }

        /// <summary>
        /// 英语得分
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学科得分
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质得分
        /// </summary>
        public Decimal QualityScore { get; set; }
        public EState State { get; set; }

        /// <summary>
        /// 分值项目名称 用, 分隔
        /// </summary>
        public String ScoreItemNames { get; set; }

        #region 新增字段(与业务无关) 2016-09-23 18：44
        /// <summary>
        /// 热点标记标签(与业务无关)
        /// </summary>
        public string HotKey { get; set; } 

        /// <summary>
        /// 学术提升指标(与业务无关)
        /// </summary>
        public int LearnUp { get; set; }
        /// <summary>
        /// 英语提升指标(与业务无关)
        /// </summary>
        public int EnglishUp { get; set; }
        /// <summary>
        /// 能力提升指标(与业务无关)
        /// </summary>
        public int AbilityUp { get; set; }
        #endregion
        #endregion


        #region Extendsions

        public Course CreateNewDataModel()
        {
            var model = new Course();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.CourseTypeId = CourseTypeId;
            model.CourseTypeName = CourseTypeName;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.CoursePlaceName = CoursePlaceName;
            model.CoursePlaceLocationId = CoursePlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.RecommendedReason = RecommendedReason;
            model.Service = Service;

            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.CourseDescription = CourseDescription;
            model.Schedule = Schedule;
            model.Fee = Fee;
            model.Stay = Stay;
            model.Visa = Visa;
            model.Stroke = Stroke;
            model.Security = Security;
            model.State = State;
            model.CourseTemplateId = CourseTemplateId;
            model.DifficultyValue = DifficultyValue;
            model.CompletionValue = CompletionValue;
            model.FeeValue = FeeValue;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.ScoreItemNames = ScoreItemNames;

            #region 新增字段(与业务无关) 2016-09-23 18：44
            model.HotKey = HotKey;
            model.LearnUp = LearnUp;
            model.EnglishUp = EnglishUp;
            model.AbilityUp = AbilityUp;
            #endregion
            return model;
        }

        public Course MergeDataModel(Course model)
        {
            model.CourseTypeId = CourseTypeId;
            model.CourseTypeName = CourseTypeName;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.CoursePlaceName = CoursePlaceName;
            model.CoursePlaceLocationId = CoursePlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.RecommendedReason = RecommendedReason;
            model.Service = Service;
       
            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.CourseDescription = CourseDescription;
            model.Schedule = Schedule;
            model.Fee = Fee;
            model.Stay = Stay;
            model.Visa = Visa;
            model.Stroke = Stroke;
            model.Security = Security;
            model.State = State;
            model.CourseTemplateId = CourseTemplateId;
            model.DifficultyValue = DifficultyValue;
            model.CompletionValue = CompletionValue;
            model.FeeValue = FeeValue;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.ScoreItemNames = ScoreItemNames;
            #region 新增字段(与业务无关) 2016-09-23 18：44
            model.HotKey = HotKey;
            model.LearnUp = LearnUp;
            model.EnglishUp = EnglishUp;
            model.AbilityUp = AbilityUp;
            #endregion
            return model;
        }
        #endregion

    }
}
