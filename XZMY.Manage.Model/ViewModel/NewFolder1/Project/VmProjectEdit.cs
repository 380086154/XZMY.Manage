using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Project
{
    [Serializable]
    public class VmProjectEdit : ViewBase, IActionViewModel<Model.DataModel.Project.Project>
    {

        public Guid DataId { get; set; }

        #region Properties 
        /// <summary>
        /// 活动模板Id
        /// </summary>
        public Guid ProjectTemplateId { get; set; }
        /// <summary>
        /// 活动类型，活动线路  数据字典  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTypeId")] 
        //[DisplayName("活动类型，活动线路  数据字典  如 夏令营")] 
        public Guid ProjectTypeId { get; set; }
        /// <summary>
        /// 活动类型，活动线路  数据字典  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTypeName")] 
        //[DisplayName("活动类型，活动线路  数据字典  如 夏令营")] 
        public String ProjectTypeName { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("活动名称")] 
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
        /// 活动面向人群分类   如：初中生,高中生,大学生
        /// </summary>
        //[EntAttributes.DBColumn("SuitablePerson")] 
        //[DisplayName("活动面向人群分类   如：初中生,高中生,大学生")] 
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
        /// 活动地点 描述  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("ProjectPlaceName")] 
        //[DisplayName("活动地点 描述  如:中国-上海、美国-纽约")] 
        public String ProjectPlaceName { get; set; }
        /// <summary>
        /// 活动地点  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("ProjectPlaceLocationId")] 
        //[DisplayName("活动地点  如:中国-上海、美国-纽约")] 
        public Guid ProjectPlaceLocationId { get; set; }
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
        //[EntAttributes.DBColumn("RecommendedReason")] 
        //[DisplayName("推荐指数  1~5")] 
        public String RecommendedReason { get; set; }
        /// <summary>
        /// 包含服务： 用,逗号分隔   活动所包含的服务信息标识  住宿 签证 保险
        /// </summary>
        //[EntAttributes.DBColumn("Service")] 
        //[DisplayName("包含服务： 用,逗号分隔   活动所包含的服务信息标识  住宿 签证 保险")] 
        public String Service { get; set; }
      
        /// <summary>
        /// 项目费用 市场价格标价
        /// </summary>
        //[EntAttributes.DBColumn("MarketPrice")] 
        //[DisplayName("项目费用 市场价格标价")] 
        public Decimal MarketPrice { get; set; }
        /// <summary>
        /// 活动价格，实际价格
        /// </summary>
        //[EntAttributes.DBColumn("ActualPrice")] 
        //[DisplayName("活动价格，实际价格")] 
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
        //[EntAttributes.DBColumn("ProjectDescription")] 
        //[DisplayName("项目介绍   如:项目详细描述")] 
        public String ProjectDescription { get; set; }
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
        /// 难度系数 值越大越难   对应学霸路线
        /// </summary>
        public int DifficultyValue { get; set; }
        /// <summary>
        /// 完成系数  值越高完成度就越高  对应稳妥路线
        /// </summary>
        public int CompletionValue { get; set; }
        /// <summary>
        /// 性价比  值越高性价比 越高 对应
        /// </summary>
        public int FeeValue { get; set; }
        /// <summary>
        /// 英语得分
        /// </summary>
        public decimal EnglishScore { get; set; }
        /// <summary>
        /// 学科得分
        /// </summary>
        public decimal LearnScore { get; set; }
        /// <summary>
        /// 素质得分
        /// </summary>
        public decimal QualityScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
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

        public Model.DataModel.Project.Project CreateNewDataModel()
        {
            var model = new Model.DataModel.Project.Project();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectTypeId = ProjectTypeId;
            model.ProjectTypeName = ProjectTypeName;
            model.ProjectTemplateId = ProjectTemplateId;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.ProjectPlaceName = ProjectPlaceName;
            model.ProjectPlaceLocationId = ProjectPlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.RecommendedReason = RecommendedReason;
            model.Service = Service;
            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.ProjectDescription = ProjectDescription;
            model.Schedule = Schedule;
            model.Fee = Fee;
            model.Stay = Stay;
            model.Visa = Visa;
            model.Stroke = Stroke;
            model.Security = Security;
            model.DifficultyValue = DifficultyValue;
            model.CompletionValue = CompletionValue;
            model.FeeValue = FeeValue;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.State = State == EState.其它 ? EState.启用 : State;
            model.ScoreItemNames = ScoreItemNames;

            #region 新增字段(与业务无关) 2016-09-23 18：44
            model.HotKey = HotKey;
            model.LearnUp = LearnUp;
            model.EnglishUp = EnglishUp;
            model.AbilityUp = AbilityUp;
            #endregion
            return model;
        }

        public Model.DataModel.Project.Project MergeDataModel(Model.DataModel.Project.Project model)
        {
            model.ProjectTypeId = ProjectTypeId;
            model.ProjectTypeName = ProjectTypeName;
            model.ProjectTemplateId = ProjectTemplateId;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.ProjectPlaceName = ProjectPlaceName;
            model.ProjectPlaceLocationId = ProjectPlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.RecommendedReason = RecommendedReason;
            model.Service = Service;
            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.ProjectDescription = ProjectDescription;
            model.Schedule = Schedule;
            model.Fee = Fee;
            model.Stay = Stay;
            model.Visa = Visa;
            model.Stroke = Stroke;
            model.Security = Security;
            model.DifficultyValue = DifficultyValue;
            model.CompletionValue = CompletionValue;
            model.FeeValue = FeeValue;
            model.EnglishScore = EnglishScore;
            model.LearnScore = LearnScore;
            model.QualityScore = QualityScore;
            model.State = State == EState.其它 ? EState.启用 : State;
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
