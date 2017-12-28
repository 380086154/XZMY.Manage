using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Advisories;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ViewModel.Advisories
{

    [Serializable]
    public class VmAdvisory : IActionViewModel<Advisory>
    {
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("标题")] 
        public String Title { get; set; }
        /// <summary>
        /// 来源类型  1活动 2课程
        //3服务 4留学规划 5其它
        /// </summary>
        //[EntAttributes.DBColumn("TypeId")] 
        //[DisplayName("来源类型  1活动 2课程 3服务 4留学规划 5其它")] 
        public Guid TypeId { get; set; }
        /// <summary>
        /// 来源类型  1活动 2课程
        //3服务 4留学规划 5其它
        /// </summary>
        //[EntAttributes.DBColumn("TypeName")] 
        //[DisplayName("来源类型  1活动 2课程
        //3服务 4留学规划 5其它")] 
        public String TypeName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("描述")] 
        public String Description { get; set; }
        /// <summary>
        /// 咨询人
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("咨询人")] 
        public String Name { get; set; }
        /// <summary>
        /// 联系方式 1手机 2QQ 3微信 4其它
        /// </summary>
        //[EntAttributes.DBColumn("ContactTypeId")] 
        //[DisplayName("联系方式 1手机 2QQ 3微信 4其它")] 
        public Guid ContactTypeId { get; set; }
        /// <summary>
        /// 联系方式 1手机 2QQ 3微信 4其它
        /// </summary>
        //[EntAttributes.DBColumn("ContactTypeName")] 
        //[DisplayName("联系方式 1手机 2QQ 3微信 4其它")] 
        public String ContactTypeName { get; set; }
        /// <summary>
        /// 联系号码
        /// </summary>
        //[EntAttributes.DBColumn("ContactNumber")] 
        //[DisplayName("联系号码")] 
        public String ContactNumber { get; set; }

        public EState State { get; set; }

        #endregion

        #region Extendsions

        public Advisory CreateNewDataModel()
        {
            var model = new Advisory();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Title = Title;
            model.TypeId = TypeId;
            model.TypeName = TypeName;
            model.Description = Description;
            model.Name = Name;
            model.ContactTypeId = ContactTypeId;
            model.ContactTypeName = ContactTypeName;
            model.ContactNumber = ContactNumber;
            model.State = State;
            return model;
        }

        public Advisory MergeDataModel(Advisory model)
        {
            model.Title = Title;
            model.TypeId = TypeId;
            model.TypeName = TypeName;
            model.Description = Description;
            model.Name = Name;
            model.ContactTypeId = ContactTypeId;
            model.ContactTypeName = ContactTypeName;
            model.ContactNumber = ContactNumber;
            model.State = State;
            return model;
        }
        #endregion
    }
}
