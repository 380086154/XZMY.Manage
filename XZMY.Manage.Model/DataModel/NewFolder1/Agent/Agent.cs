using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Agent
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DBTable("Agent")]
    public class Agent : EntityBase, IDataModel
    {
        #region Properties 

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
        /// 法人联系电话
        /// </summary>
        //[EntAttributes.DBColumn("LegalMobile")] 
        //[DisplayName("法人联系电话")] 
        public String LegalMobile { get; set; }
        /// <summary>
        /// 代理商等级 数据字典
        /// </summary>
        //[EntAttributes.DBColumn("Level")] 
        //[DisplayName("代理商等级 数据字典")] 
        public Guid LevelId { get; set; }
        /// <summary>
        /// 代理商性质  数字字典
        /// </summary>
        //[EntAttributes.DBColumn("Nature")] 
        //[DisplayName("代理商性质  数字字典")] 
        public Guid NatureId { get; set; }
        /// <summary>
        /// 代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道
        /// </summary>
        //[EntAttributes.DBColumn("CategoryId")] 
        //[DisplayName("代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道")] 
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 开户行  数据字典 描述
        /// </summary>
        //[EntAttributes.DBColumn("BankName")] 
        //[DisplayName("开户行")] 
        public String BankName { get; set; }
        /// <summary>
        /// 开户行所在地全名
        /// </summary>
        //[EntAttributes.DBColumn("BankFullName")] 
        //[DisplayName("开户行全称")] 
        public String BankFullName { get; set; }
        /// <summary>
        /// 开户人
        /// </summary>
        //[EntAttributes.DBColumn("BankAccount")] 
        //[DisplayName("开户人")] 
        public String BankAccount { get; set; }
        /// <summary>
        /// 开户卡号
        /// </summary>
        //[EntAttributes.DBColumn("BankNumber")] 
        //[DisplayName("开户卡号")] 
        public String BankNumber { get; set; }
        /// <summary>
        /// 代理商描述
        /// </summary>
        //[EntAttributes.DBColumn("Description")] 
        //[DisplayName("代理商描述")] 
        public String Description { get; set; }
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

        #region Collection

        #endregion
    }
}