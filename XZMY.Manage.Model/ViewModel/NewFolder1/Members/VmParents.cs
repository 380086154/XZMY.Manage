
using System;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;


namespace XZMY.Manage.Model.ViewModel.Members
{
    [Serializable]
    public class VmParents :ViewBase, IActionViewModel<Parent>
    {
        #region Properties 
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 家长账号id外键
        /// </summary>
        //[EntAttributes.DBColumn("MemberId")] 
        //[DisplayName("家长账号id外键")] 
        public Guid MemberId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        //[EntAttributes.DBColumn("Name")] 
        //[DisplayName("姓名")] 
        public String Name { get; set; }
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
        /// 性别  0保密 1男  2女
        /// </summary>
        //[EntAttributes.DBColumn("Gender")] 
        //[DisplayName("性别  0保密 1男  2女")] 
        public EGender Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        //[EntAttributes.DBColumn("BirthDate")] 
        //[DisplayName("出生日期")] 
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 所在地:重庆
        /// </summary>
        //[EntAttributes.DBColumn("Location")] 
        //[DisplayName("所在地:重庆")] 
        public String Location { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        //[EntAttributes.DBColumn("PoliticalStatus")] 
        //[DisplayName("政治面貌")] 
        public String PoliticalStatus { get; set; }
        /// <summary>
        /// 工作电话
        /// </summary>
        //[EntAttributes.DBColumn("WorkPhone")] 
        //[DisplayName("工作电话")] 
        public String WorkPhone { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        //[EntAttributes.DBColumn("CompanyName")] 
        //[DisplayName("工作单位")] 
        public String CompanyName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        //[EntAttributes.DBColumn("Position")] 
        //[DisplayName("职位")] 
        public String Position { get; set; }

        #endregion

        #region Extendsions

        public Parent CreateNewDataModel()
        {
            var model = new Parent();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.BirthDate= BirthDate;
            model.CompanyName= CompanyName;
            model.Email= Email;
            model.Gender= Gender;
            model.Location= Location;
            model.MemberId= MemberId;
            model.Mobile= Mobile;
            model.Name= Name;
            model.PoliticalStatus= PoliticalStatus;
            model.Position= Position;
            model.WorkPhone= WorkPhone;
            
            return model;
        }

        public Parent MergeDataModel(Parent model)
        {
            model.BirthDate = BirthDate;
            model.CompanyName = CompanyName;
            model.Email = Email;
            model.Gender = Gender;
            model.Location = Location;
            model.MemberId = MemberId;
            model.Mobile = Mobile;
            model.Name = Name;
            model.PoliticalStatus = PoliticalStatus;
            model.Position = Position;
            model.WorkPhone = WorkPhone;
            return model;
        }
        #endregion
    }
}
