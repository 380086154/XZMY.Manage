using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Project
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("ProjectTemplate")]
    public class ProjectTemplate : EntityBase, IDataModel
    {
        #region Properties 

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
        /// <summary>
        /// 活动地点 描述  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTemplatePlaceName")] 
        //[DisplayName("活动地点 描述  如:中国-上海、美国-纽约")] 
        public String ProjectTemplatePlaceName { get; set; }
        /// <summary>
        /// 活动地点  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTemplatePlaceLocationId")] 
        //[DisplayName("活动地点  如:中国-上海、美国-纽约")] 
        public Guid ProjectTemplatePlaceLocationId { get; set; }
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
        //[EntAttributes.DBColumn("ProjectTemplateDescription")] 
        //[DisplayName("项目介绍   如:项目详细描述")] 
        public String ProjectTemplateDescription { get; set; }
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
        /// <summary>
        /// 分值项目名称 用, 分隔
        /// </summary>
        public String ScoreItemNames { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 适合年级 数字类型  如7小学一年级
        /// </summary>
        public String FitGrade { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}