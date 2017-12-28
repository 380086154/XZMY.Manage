
using System;
using System.Collections.Generic;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel.User;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmPlannerEdit :ViewBase, IActionViewModel<Planner>
    {
        #region Properties 

        /// <summary>
        /// 规划师Id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("规划师Id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 规划师登录账号Id外键
        /// </summary>
        //[EntAttributes.DBColumn("UserId")] 
        //[DisplayName("规划师登录账号Id外键")] 
        public Guid UserId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("规划师姓名")] 
        public String Name { get; set; }
        /// <summary>
        /// 规划师编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("规划师编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 规划师照片 多张 用;分割
        /// </summary>
        //[EntAttributes.DBColumn("Pictures")] 
        //[DisplayName("规划师照片 多张 用;分割")] 
        public String Pictures { get; set; }
        /// <summary>
        /// 规划师资质  数据字典
        /// </summary>
        //[EntAttributes.DBColumn("QualificationsId")] 
        //[DisplayName("规划师资质  数据字典")] 
        public Guid QualificationsId { get; set; }
        /// <summary>
        /// 规划师资质  数据字典
        /// </summary>
        //[EntAttributes.DBColumn("QualificationsName")] 
        //[DisplayName("规划师资质  数据字典")] 
        public String QualificationsName { get; set; }
        /// <summary>
        /// 规划师级别 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("LevelId")] 
        //[DisplayName("规划师级别 数据字典")] 
        public Guid LevelId { get; set; }
        /// <summary>
        /// 规划师级别 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("LevelName")] 
        //[DisplayName("规划师级别 数据字典")] 
        public String LevelName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("描述")] 
        public String Description { get; set; }
        #region Statistics 统计学生参加课程活动的数据
        /// <summary>
        /// 参加过活动的学生人数
        /// </summary>
        public Int32 StatisticsAttendProject { get; set; } 
        /// <summary>
        /// 参加过课程的学生人数
        /// </summary>
        public Int32 StatisticsAttendCourse { get; set; } 
        /// <summary>
        /// 学生总数
        /// </summary>
        public Int32 StatisticsStudent { get; set; } 
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public VmUserAccountEdit modelUser { get; set; }
        #endregion

        #region Extendsions

        public Planner CreateNewDataModel()
        {
            var model = new Planner();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.UserId = UserId;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.QualificationsId = QualificationsId;
            model.QualificationsName = QualificationsName;
            model.LevelId = LevelId;
            model.LevelName = LevelName;
            model.Description = Description;
            return model;
        }

        public Planner MergeDataModel(Planner model)
        {
            model.UserId = UserId;
            model.Name = Name;
            model.Code = Code;
            model.Pictures = Pictures;
            model.QualificationsId = QualificationsId;
            model.QualificationsName = QualificationsName;
            model.LevelId = LevelId;
            model.LevelName = LevelName;
            model.Description = Description;
            return model;
        }
        #endregion
    }

}

