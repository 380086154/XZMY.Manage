﻿using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Members
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    public class Student : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("学生姓名")] 
        public String Name { get; set; }
        /// <summary>
        /// 学生编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("学生编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 英文名字或是拼音
        /// </summary>
        //[EntAttributes.DBColumn("EnglishName")] 
        //[DisplayName("英文名字或是拼音")] 
        public String EnglishName { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        //[EntAttributes.DBColumn("QQ")] 
        //[DisplayName("QQ")] 
        public String QQ { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[EntAttributes.DBColumn("Email")] 
        //[DisplayName("邮箱")] 
        public String Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        //[EntAttributes.DBColumn("Mobile")] 
        //[DisplayName("手机号")] 
        public String Mobile { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        //[EntAttributes.DBColumn("BirthDate")] 
        //[DisplayName("出生日期")] 
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 性别  0保密 1男  2女
        /// </summary>
        //[EntAttributes.DBColumn("Gender")] 
        //[DisplayName("性别  0保密 1男  2女")] 
        public EGender Gender { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string GenderName  { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        //[EntAttributes.DBColumn("IdentityCard")] 
        //[DisplayName("身份证")] 
        public String IdentityCard { get; set; }
        /// <summary>
        /// 曾经用名字
        /// </summary>
        //[EntAttributes.DBColumn("OnceName")] 
        //[DisplayName("曾经用名字")] 
        public String OnceName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        //[EntAttributes.DBColumn("PoliticalStatus")] 
        //[DisplayName("政治面貌")] 
        public String PoliticalStatus { get; set; }
        /// <summary>
        /// 民族：汉族
        /// </summary>
        //[EntAttributes.DBColumn("Nationality")] 
        //[DisplayName("民族：汉族")] 
        public String Nationality { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        //[EntAttributes.DBColumn("NativePlace")] 
        //[DisplayName("籍贯")] 
        public String NativePlace { get; set; }
        /// <summary>
        /// 户口类型  城镇、农村
        /// </summary>
        //[EntAttributes.DBColumn("RegisteredPermanentResidenceType")] 
        //[DisplayName("户口类型  城镇、农村")] 
        public String RegisteredPermanentResidenceType { get; set; }
        /// <summary>
        /// 户口所在地
        /// </summary>
        //[EntAttributes.DBColumn("RegisteredPermanentResidenceLocation")] 
        //[DisplayName("户口所在地")] 
        public String RegisteredPermanentResidenceLocation { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        //[EntAttributes.DBColumn("LocationId")] 
        //[DisplayName("所在地")] 
        public Guid LocationId { get; set; }
        /// <summary>
        /// 所在地名称
        /// </summary>
        //[EntAttributes.DBColumn("LocationPathName")] 
        //[DisplayName("所在地名称")] 
        public String LocationPathName { get; set; }
        /// <summary>
        /// 通讯地址
        /// </summary>
        //[EntAttributes.DBColumn("CorrespondenceAddress")] 
        //[DisplayName("通讯地址")] 
        public String CorrespondenceAddress { get; set; }
        /// <summary>
        /// 在读状态   1是  2否
        /// </summary>
        //[EntAttributes.DBColumn("LearningState")] 
        //[DisplayName("在读状态   1是  2否")] 
        public ELearningState LearningState { get; set; }
        /// <summary>
        /// 所在学校
        /// </summary>
        //[EntAttributes.DBColumn("School")] 
        //[DisplayName("所在学校")] 
        public String School { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        //[EntAttributes.DBColumn("Grade")] 
        //[DisplayName("年级")] 
        public String Grade { get; set; }
        /// <summary>
        /// 规划师Id
        /// </summary>
        //[EntAttributes.DBColumn("PlannerId")] 
        //[DisplayName("规划师Id")] 
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 是否Vip会员 1是 2否
        /// </summary>
        //[EntAttributes.DBColumn("IsVip")] 
        //[DisplayName("是否Vip会员 1是 2否")] 
        public Int32 IsVip { get; set; }
        /// <summary>
        /// 当前学生英语得分
        /// </summary>
        //[EntAttributes.DBColumn("EnglishScore")] 
        //[DisplayName("当前学生英语得分")] 
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 当前学生学科得分
        /// </summary>
        //[EntAttributes.DBColumn("LearnScore")] 
        //[DisplayName("当前学生学科得分")] 
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 当前学生素质得分
        /// </summary>
        //[EntAttributes.DBColumn("QualityScore")] 
        //[DisplayName("当前学生素质得分")] 
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 护照
        /// </summary>
        //[EntAttributes.DBColumn("Passport")] 
        //[DisplayName("护照")] 
        public String Passport { get; set; }
        /// <summary>
        /// 护照签发日期
        /// </summary>
        //[EntAttributes.DBColumn("PassportDate")] 
        //[DisplayName("护照签发日期")] 
        public DateTime PassportDate { get; set; }
        /// <summary>
        /// 护照签发地
        /// </summary>
        //[EntAttributes.DBColumn("PassportPlaceOfIssue")] 
        //[DisplayName("护照签发地")] 
        public String PassportPlaceOfIssue { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        //[EntAttributes.DBColumn("HomeAddress")] 
        //[DisplayName("家庭住址")] 
        public String HomeAddress { get; set; }
        /// <summary>
        /// 就读学校名称
        /// </summary>
        //[EntAttributes.DBColumn("SchoolName")] 
        //[DisplayName("就读学校名称")] 
        public String SchoolName { get; set; }
        /// <summary>
        /// 学校地址
        /// </summary>
        //[EntAttributes.DBColumn("SchoolAddress")] 
        //[DisplayName("学校地址")] 
        public String SchoolAddress { get; set; }
        /// <summary>
        /// 学校电话
        /// </summary>
        //[EntAttributes.DBColumn("SchoolTelephone")] 
        //[DisplayName("学校电话")] 
        public String SchoolTelephone { get; set; }
        /// <summary>
        /// 入校时间
        /// </summary>
        //[EntAttributes.DBColumn("InSchoolTime")] 
        //[DisplayName("入校时间")] 
        public DateTime InSchoolTime { get; set; }
        /// <summary>
        /// 所在班级
        /// </summary>
        //[EntAttributes.DBColumn("gradeName")] 
        //[DisplayName("所在班级")] 
        public String GradeName { get; set; }
        /// <summary>
        /// 班主任姓名
        /// </summary>
        //[EntAttributes.DBColumn("TeacherName")] 
        //[DisplayName("班主任姓名")] 
        public String TeacherName { get; set; }
        /// <summary>
        /// 班主任手机号
        /// </summary>
        //[EntAttributes.DBColumn("TeacherMoblie")] 
        //[DisplayName("班主任手机号")] 
        public String TeacherMoblie { get; set; }
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        //[EntAttributes.DBColumn("Hobby")] 
        //[DisplayName("兴趣爱好")] 
        public String Hobby { get; set; }
        /// <summary>
        /// 分配人Id
        /// </summary>
        public Guid AssignId { get; set; }
        /// <summary>
        /// 分配人名称
        /// </summary>
        public String AssignName { get; set; }
        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AssignTime { get; set; }
        /// <summary>
        /// VIP申请时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        ///家长ID
        /// </summary>
        public Guid ParentsId { get; set; }
        /// <summary>
        /// 是否需要帮助
        /// </summary>
        public bool IsHelp { get; set; }
        /// <summary>
        /// 美国社会保险号码
        /// </summary>
        public String USASocialSecurityNumber { get; set; } 
        /// <summary>
        /// 申请集 陈述 概述  word文件
        /// </summary>
        public String Summary { get; set; }
        /// <summary>
        /// 申请集 个人简历 个人陈述上传WORD路径
        /// </summary>
        public String Resume { get; set; } 
        /// <summary>
        /// 规划师给学生的综合评价
        /// </summary>
        public String StudentsEvaluation { get; set; }
        /// <summary>
        /// 需要翻译的申请集页
        /// </summary>
        public String IsHelpItemName { get; set; }
        #endregion

        #region Collection

        #endregion
    }
}
