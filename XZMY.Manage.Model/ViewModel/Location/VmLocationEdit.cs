﻿
using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Location
{
    [Serializable]
    public class VmLocationEdit : IActionViewModel<Model.DataModel.Location.Location>
    {
        #region Properties 

        /// <summary>
        /// 地区id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("地区id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 地区名字
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("地区名字")] 
        public String Name { get; set; }
        /// <summary>
        /// 地区编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("地区编码")] 
        public String EName { get; set; }
        /// <summary>
        /// 上级地区Id
        /// </summary>
        //[EntAttributes.DBColumn("ParentId")] 
        //[DisplayName("上级地区Id")] 
        public Guid ParentId { get; set; }
        /// <summary>
        /// 级别   1国家 2省级 3城市 4县级
        /// </summary>
        //[EntAttributes.DBColumn("Level")] 
        //[DisplayName("级别   1国家 2省级 3城市 4县级")] 
        public Int32 Level { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        //[EntAttributes.DBColumn("Sort")] 
        //[DisplayName("排序")] 
        public Int32 Sort { get; set; }
        /// <summary>
        /// 路径Id   如  xxxxx,ddddd,ddd
        /// </summary>
        //[EntAttributes.DBColumn("PathId")] 
        //[DisplayName("路径Id   如  xxxxx,ddddd,ddd")] 
        public String PathId { get; set; }
        /// <summary>
        /// 路径名词  如  xxxxx,ddddd,ddd
        /// </summary>
        //[EntAttributes.DBColumn("PathName")] 
        //[DisplayName("路径名词  如  xxxxx,ddddd,ddd")] 
        public String PathName { get; set; }

        #endregion

        #region Extendsions

        public Model.DataModel.Location.Location CreateNewDataModel()
        {
            var model = new Model.DataModel.Location.Location();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.EName = EName;
            model.ParentId = ParentId;
            model.Level = Level;
            model.Sort = Sort;
            model.PathId = PathId;
            model.PathName = PathName;
            return model;
        }

        public Model.DataModel.Location.Location MergeDataModel(Model.DataModel.Location.Location model)
        {
            model.Name = Name;
            model.EName = EName;
            model.ParentId = ParentId;
            model.Level = Level;
            model.Sort = Sort;
            model.PathId = PathId;
            model.PathName = PathName;
            return model;
        }
        #endregion
    }

}

