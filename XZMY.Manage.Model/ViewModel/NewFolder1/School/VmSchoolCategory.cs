

using System;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.School
{
    [Serializable]
    public class VmSchoolCategory : IActionViewModel<SchoolCategory>
    {
        #region Properties 

        /// <summary>
        /// 学校类别Id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("学校类别Id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        //[EntAttributes.DBColumn("ParentId")] 
        //[DisplayName("父级Id")] 
        public Guid ParentId { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("类别名称")] 
        public String Name { get; set; }
        /// <summary>
        /// 类别编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("类别编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 层级名称
        /// </summary>
        //[EntAttributes.DBColumn("PathName")] 
        //[DisplayName("层级名称")] 
        public String PathName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        //[EntAttributes.DBColumn("Level")] 
        //[DisplayName("级别")] 
        public Int32 Level { get; set; }

        #endregion

        #region Extendsions

        public SchoolCategory CreateNewDataModel()
        {
            var model = new SchoolCategory();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ParentId = ParentId;
            model.Name = Name;
            model.Code = Code;
            model.PathName = PathName;
            model.Level = Level;
            return model;
        }

        public SchoolCategory MergeDataModel(SchoolCategory model)
        {
            model.ParentId = ParentId;
            model.Name = Name;
            model.Code = Code;
            model.PathName = PathName;
            model.Level = Level;
            return model;
        }
        #endregion
    }

}
