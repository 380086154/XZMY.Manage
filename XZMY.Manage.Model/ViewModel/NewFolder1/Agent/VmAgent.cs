
using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Agent
{
    [Serializable]
    public class VmAgent : IActionViewModel<DataModel.Agent.Agent>
    {
        #region Properties 

        /// <summary>
        /// 代理商id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("代理商id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 代理商会员账号id
        /// </summary>
        //[EntAttributes.DBColumn("UserId")] 
        //[DisplayName("代理商会员账号id")] 
        public Guid UserId { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        //[EntAttributes.DBColumn("CompanyName")] 
        //[DisplayName("企业名称")] 
        public String CompanyName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        //[EntAttributes.DBColumn("LegalPerson")] 
        //[DisplayName("法人代表")] 
        public String LegalPerson { get; set; }
        /// <summary>
        /// 代理商等级 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("Level")] 
        //[DisplayName("代理商等级 数据字典")] 
        public Guid Level { get; set; }
        /// <summary>
        /// 代理商性质  数字字典
        /// </summary>
        //[EntAttributes.DBColumn("Nature")] 
        //[DisplayName("代理商性质  数字字典")] 
        public Guid Nature { get; set; }
        /// <summary>
        /// 代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道
        /// </summary>
        //[EntAttributes.DBColumn("CategoryId")] 
        //[DisplayName("代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道")] 
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 开户卡号
        /// </summary>
        //[EntAttributes.DBColumn("BankNumber")] 
        //[DisplayName("开户卡号")] 
        public String BankNumber { get; set; }
        /// <summary>
        /// 开户行  数据字典 描述
        /// </summary>
        //[EntAttributes.DBColumn("BankName")] 
        //[DisplayName("开户行  数据字典 描述")] 
        public String BankName { get; set; }
        /// <summary>
        /// 开户行所在地全名
        /// </summary>
        //[EntAttributes.DBColumn("BankFullName")] 
        //[DisplayName("开户行所在地全名")] 
        public String BankFullName { get; set; }
        /// <summary>
        /// 开户行账号
        /// </summary>
        //[EntAttributes.DBColumn("BankAccount")] 
        //[DisplayName("开户行账号")] 
        public String BankAccount { get; set; }
        /// <summary>
        /// 代理商描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("代理商描述")] 
        public String Description { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        //[EntAttributes.DBColumn("ContactName")] 
        //[DisplayName("联系人姓名")] 
        public String ContactName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        //[EntAttributes.DBColumn("ContactMobile")] 
        //[DisplayName("联系人电话")] 
        public String ContactMobile { get; set; }
        /// <summary>
        /// 联系人邮件
        /// </summary>
        //[EntAttributes.DBColumn("ContactEmail")] 
        //[DisplayName("联系人邮件")] 
        public String ContactEmail { get; set; }
        /// <summary>
        /// 佣金比例
        /// </summary>
        //[EntAttributes.DBColumn("Commission")] 
        //[DisplayName("佣金比例")] 
        public Decimal Commission { get; set; }
        /// <summary>
        /// 地区Id
        /// </summary>
        //[EntAttributes.DBColumn("LocationId")] 
        //[DisplayName("地区Id")] 
        public Guid LocationId { get; set; }
        /// <summary>
        /// 地区层级名称
        /// </summary>
        //[EntAttributes.DBColumn("LocationPathName")] 
        //[DisplayName("地区层级名称")] 
        public String LocationPathName { get; set; }

        /// <summary>
        /// 代理商所在城市ID
        /// </summary>
        public Guid LocationCityId { get; set; }

        /// <summary>
        /// 代理商所在城市层级   如中国,重庆,重庆市
        /// </summary>
        public string LocationCityPathName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        //[EntAttributes.DBColumn("Address")] 
        //[DisplayName("地址")] 
        public String Address { get; set; }

        #endregion

        #region Extendsions

        public DataModel.Agent.Agent CreateNewDataModel()
        {
            var model = new DataModel.Agent.Agent();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.UserId = UserId;
            model.CompanyName = CompanyName;
            model.LegalPerson = LegalPerson;
            model.LevelId = Level;
            model.NatureId = Nature;
            model.CategoryId = CategoryId;
            model.BankNumber = BankNumber;
            model.BankName = BankName;
            model.BankFullName = BankFullName;
            model.BankAccount = BankAccount;
            model.Description = Description;
            model.Commission = Commission;
            model.LocationId = LocationId;
            model.LocationPathName = LocationPathName;
            model.LocationCityId = LocationCityId;
            model.LocationCityPathName = LocationCityPathName;
            model.Address = Address;
            return model;
        }

        public DataModel.Agent.Agent MergeDataModel(DataModel.Agent.Agent model)
        {
            model.UserId = UserId;
            model.CompanyName = CompanyName;
            model.LegalPerson = LegalPerson;
            model.LevelId = Level;
            model.NatureId = Nature;
            model.CategoryId = CategoryId;
            model.BankNumber = BankNumber;
            model.BankName = BankName;
            model.BankFullName = BankFullName;
            model.BankAccount = BankAccount;
            model.Description = Description;
            model.Commission = Commission;
            model.LocationId = LocationId;
            model.LocationPathName = LocationPathName;
            model.LocationCityId = LocationCityId;
            model.LocationCityPathName = LocationCityPathName;
            model.Address = Address;
            return model;
        }
        #endregion
    }

}

