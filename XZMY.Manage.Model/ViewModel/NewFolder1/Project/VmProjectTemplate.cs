﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Configuration;
using XZMY.Manage.Model.DataModel.Project;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Project
{
    [Serializable]
    [DataContract]
    public class VmProjectTemplate : ViewBase, IActionViewModel<ProjectTemplate>
    {
        #region Properties 

        /// <summary>
        /// 活动主键Id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("活动主键Id")] 
        [DataMember]
        public Guid DataId { get; set; }
        /// <summary>
        /// 活动类型，活动线路  数据字典  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTypeId")] 
        //[DisplayName("活动类型，活动线路  数据字典  如 夏令营")] 
        [DataMember]
        public Guid ProjectTypeId { get; set; }
        /// <summary>
        /// 活动类型，活动线路  数据字典  如 夏令营
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTypeName")] 
        //[DisplayName("活动类型，活动线路  数据字典  如 夏令营")] 
        [DataMember]
        public String ProjectTypeName { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("活动名称")] 
        [DataMember]
        public String Name { get; set; }
        /// <summary>
        /// 项目编码  国家+年月日+编号（5位自增）
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("项目编码  国家+年月日+编号（5位自增）")] 
        [DataMember]
        public String Code { get; set; }
        /// <summary>
        /// 宣传图片  多张图片 用;分割  格式如：https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png;https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png
        /// </summary>
        //[EntAttributes.DBColumn("Pictures")] 
        //[DisplayName("宣传图片  多张图片 用;分割  格式如：https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png;https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo/bd_logo1_31bdc765.png")] 
        [DataMember]
        public String Pictures { get; set; }
        /// <summary>
        /// 获取唯一一张绝对路径的图片
        /// </summary>
        public String PrctureOnly
        {
            get
            {
                string strImage = "";
                var sImages = Pictures.Split(",");
                if (sImages.Length > 0)
                {
                    if (sImages[0].IndexOf("http://") > -1)
                    {
                        strImage = string.Format("{0}{1}", WebConfigurationManager.AppSettings["SiteUrl"], sImages[0]);
                    }
                    else
                    {
                        strImage = sImages[0];
                    }

                }
                return strImage;
            }
        }
        /// <summary>
        /// 活动面向人群分类   如：初中生,高中生,大学生
        /// </summary>
        //[EntAttributes.DBColumn("SuitablePerson")] 
        //[DisplayName("活动面向人群分类   如：初中生,高中生,大学生")] 
        [DataMember]
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
        //[EntAttributes.DBColumn("ProjectTemplatePlaceName")] 
        //[DisplayName("活动地点 描述  如:中国-上海、美国-纽约")] 
        [DataMember]
        public String ProjectTemplatePlaceName { get; set; }
        /// <summary>
        /// 活动地点  如:中国-上海、美国-纽约
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTemplatePlaceLocationId")] 
        //[DisplayName("活动地点  如:中国-上海、美国-纽约")] 
        [DataMember]
        public Guid ProjectTemplatePlaceLocationId { get; set; }
        /// <summary>
        /// 主办方
        /// </summary>
        //[EntAttributes.DBColumn("Sponsor")] 
        //[DisplayName("主办方")] 
        [DataMember]
        public String Sponsor { get; set; }
        /// <summary>
        /// 推荐指数  1~5
        /// </summary>
        //[EntAttributes.DBColumn("RecommendedIndex")] 
        //[DisplayName("推荐指数  1~5")] 
        [DataMember]
        public Int32 RecommendedIndex { get; set; }
        /// <summary>
        /// 包含服务： 用,逗号分隔   活动所包含的服务信息标识  住宿 签证 保险
        /// </summary>
        //[EntAttributes.DBColumn("Service")] 
        //[DisplayName("包含服务： 用,逗号分隔   活动所包含的服务信息标识  住宿 签证 保险")] 
        [DataMember]
        public String Service { get; set; }
        /// <summary>
        /// 项目费用 市场价格标价
        /// </summary>
        //[EntAttributes.DBColumn("MarketPrice")] 
        //[DisplayName("项目费用 市场价格标价")] 
        [DataMember]
        public Decimal MarketPrice { get; set; }
        /// <summary>
        /// 活动价格，实际价格
        /// </summary>
        //[EntAttributes.DBColumn("ActualPrice")] 
        //[DisplayName("活动价格，实际价格")] 
        [DataMember]
        public Decimal ActualPrice { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        //[EntAttributes.DBColumn("DepositPrice")] 
        //[DisplayName("定金")] 
        [DataMember]
        public Decimal DepositPrice { get; set; }
        /// <summary>
        /// 折扣,帮助营销使用
        /// </summary>
        //[EntAttributes.DBColumn("Discount")] 
        //[DisplayName("折扣,帮助营销使用")] 
        [DataMember]
        public Decimal Discount { get; set; }
        /// <summary>
        /// 项目介绍   如:项目详细描述
        /// </summary>
        //[EntAttributes.DBColumn("ProjectTemplateDescription")] 
        //[DisplayName("项目介绍   如:项目详细描述")] 
        [DataMember]
        public String ProjectTemplateDescription { get; set; }
        /// <summary>
        /// 行程安排
        /// </summary>
        //[EntAttributes.DBColumn("Schedule")] 
        //[DisplayName("行程安排")] 
        [DataMember]
        public String Schedule { get; set; }
        /// <summary>
        /// 费用说明  费用说明清单描述
        /// </summary>
        //[EntAttributes.DBColumn("Fee")] 
        //[DisplayName("费用说明  费用说明清单描述")] 
        [DataMember]
        public String Fee { get; set; }
        /// <summary>
        /// 住宿安排描述
        /// </summary>
        //[EntAttributes.DBColumn("Stay")] 
        //[DisplayName("住宿安排描述")] 
        [DataMember]
        public String Stay { get; set; }
        /// <summary>
        /// 签证说明描述
        /// </summary>
        //[EntAttributes.DBColumn("Visa")] 
        //[DisplayName("签证说明描述")] 
        [DataMember]
        public String Visa { get; set; }
        /// <summary>
        /// 行程须知
        /// </summary>
        //[EntAttributes.DBColumn("Stroke")] 
        //[DisplayName("行程须知")] 
        [DataMember]
        public String Stroke { get; set; }
        /// <summary>
        /// 安全保障
        /// </summary>
        //[EntAttributes.DBColumn("Security")] 
        //[DisplayName("安全保障")] 
        [DataMember]
        public String Security { get; set; }
        /// <summary>
        /// 难度系数
        /// </summary>
        [DataMember]
        public int DifficultyValue { get; set; }
        /// <summary>
        /// 完成系数
        /// </summary>
        [DataMember]
        public int CompletionValue { get; set; }
        /// <summary>
        /// 性价比
        /// </summary>
        [DataMember]
        public int FeeValue { get; set; }
        /// <summary>
        /// 英语得分
        /// </summary>
        [DataMember]
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学科得分
        /// </summary>
        [DataMember]
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质得分
        /// </summary>
        [DataMember]
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 分值项目名称 用, 分隔
        /// </summary>
        [DataMember]
        public String ScoreItemNames { get; set; }
        [DataMember]
        public String NameAbstract {
            get {
                return string.Format("[活动][{1}]{0}__增加成绩[英语({2}),学科({3}),素质({4})]", Name, ProjectTypeName, EnglishScore, LearnScore, QualityScore);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public EState State { get; set; }

        [DataMember]
        public String StateName {
            get {
                return State.ToString();
            }
        } 
        /// <summary>
        /// 项目分值
        /// </summary>
        public List<VmScoresEdit> listScores {get;set;}


        /// <summary>
        /// 适合年级 数字类型  如7小学一年级
        /// </summary>
        public String FitGrade { get; set; }
        #endregion

        #region Extendsions

        public ProjectTemplate CreateNewDataModel()
        {
            var model = new ProjectTemplate();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProjectTypeId = ProjectTypeId;
            model.ProjectTypeName = ProjectTypeName;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.ProjectTemplatePlaceName = ProjectTemplatePlaceName;
            model.ProjectTemplatePlaceLocationId = ProjectTemplatePlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.Service = Service;
            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.ProjectTemplateDescription = ProjectTemplateDescription;
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
            model.ScoreItemNames = ScoreItemNames;
            model.State = State;
            model.FitGrade = FitGrade;
            return model;
        }

        public ProjectTemplate MergeDataModel(ProjectTemplate model)
        {
            model.ProjectTypeId = ProjectTypeId;
            model.ProjectTypeName = ProjectTypeName;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.SuitablePerson = SuitablePerson;
            model.ProjectTemplatePlaceName = ProjectTemplatePlaceName;
            model.ProjectTemplatePlaceLocationId = ProjectTemplatePlaceLocationId;
            model.Sponsor = Sponsor;
            model.RecommendedIndex = RecommendedIndex;
            model.Service = Service;
            model.MarketPrice = MarketPrice;
            model.ActualPrice = ActualPrice;
            model.DepositPrice = DepositPrice;
            model.Discount = Discount;
            model.ProjectTemplateDescription = ProjectTemplateDescription;
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
            model.ScoreItemNames = ScoreItemNames;
            model.State = State;
            model.FitGrade = FitGrade;
            return model;
        }
        #endregion
    }

}
