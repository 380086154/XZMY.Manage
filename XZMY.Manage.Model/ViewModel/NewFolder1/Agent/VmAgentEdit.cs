
using System;
using System.Configuration;
using XZMY.Manage.Model.DataModel.Agent;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Agent
{
    [Serializable]
    public class VmAgentEdit : ViewBase, IActionViewModel<DataModel.Agent.Agent>
    {
        public VmAgentEdit()
        {
            AgentContact = new VmAgentContact();
        }

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
        /// 代理商等级名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 代理商性质  数字字典
        /// </summary>
        //[EntAttributes.DBColumn("Nature")] 
        //[DisplayName("代理商性质  数字字典")] 
        public Guid NatureId { get; set; }
        /// <summary>
        /// 代理商性质名称
        /// </summary>
        public string NatureName { get; set; }
        /// <summary>
        /// 代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道
        /// </summary>
        //[EntAttributes.DBColumn("CategoryId")] 
        //[DisplayName("代理商类型 数据字典：留学中介、语培机构、国际学校、移民公司、其他渠道")] 
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 代理商类型名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 开户行  数据字典
        /// </summary>
        //[EntAttributes.DBColumn("Bank")] 
        //[DisplayName("开户行  数据字典")] 
        public Guid BankId { get; set; }
        /// <summary>
        /// 开户行  数据字典 描述
        /// </summary>
        //[EntAttributes.DBColumn("BankName")] 
        //[DisplayName("开户行  数据字典 描述")] 
        public String BankName { get; set; }
        /// <summary>
        /// 开户行地址
        /// </summary>
        //[EntAttributes.DBColumn("BankFullName")] 
        //[DisplayName("开户行地址")] 
        public String BankFullName { get; set; }
        /// <summary>
        /// 开户行账号
        /// </summary>
        //[EntAttributes.DBColumn("BankAccount")] 
        //[DisplayName("开户行账号")] 
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

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Zipcode { get; set; }
        public string Location { get; set; }
        public DateTime RegisteredTime { get; set; }
        public EGender Gender { get; set; }
        public int Source { get; set; }
        public EState State { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public VmAgentContact AgentContact { get; set; }

        #endregion

        #region Extendsions

        public DataModel.Agent.Agent CreateNewDataModel()
        {
            if (UserId == Guid.Empty) UserId = Guid.NewGuid();
            var model = new DataModel.Agent.Agent();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.UserId = UserId;
            model.CompanyName = CompanyName;
            model.LegalPerson = LegalPerson;
            model.LegalMobile = LegalMobile;
            model.LevelId = LevelId;
            model.NatureId = NatureId;
            model.CategoryId = CategoryId;
            model.BankAccount = BankAccount;
            model.BankNumber = BankNumber;
            model.BankName = BankName;
            model.BankFullName = BankFullName;
            model.BankAccount = BankAccount;
            model.Description = Description;
            model.Commission = Commission;
            model.LocationId = LocationId == Guid.Empty ? Guid.Parse("4113185D-BE3C-42CE-8321-3B0FEE980FCD") : LocationId;
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
            model.LevelId = LevelId;
            model.NatureId = NatureId;
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


        public UserAccount CreateNewUserAccountDataModel()
        {
            if (UserId == Guid.Empty) UserId = Guid.NewGuid();
            return new UserAccount
            {
                DataId = UserId,
                LoginName = LoginName,
                RealName = RealName,
                Password = Password.ToMd5(),
                Mobile = Mobile,
                Email = Email,
                QQ = QQ,
                Gender = Gender,
                State = State,
                Location = Location,
                Zipcode = Zipcode,
                RegisteredTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                Source = Source,
            };
        }

        public UserAccount MergeDataModel(UserAccount model)
        {
            model.Email = Email;
            model.Gender = Gender;
            model.Location = Location;
            model.LoginName = LoginName;
            model.Password = Password;
            model.Mobile = Mobile;
            model.QQ = QQ;
            model.RealName = RealName;
            model.Source = Source;
            model.State = State;
            model.Zipcode = Zipcode;

            return model;
        }


        public UserRole CreateNewUserRoleDataModel()
        {
            return new UserRole()
            {
                DataId = Guid.NewGuid(),
                UserId = UserId,
                RoleId = new Guid(ConfigurationManager.AppSettings["RoleAgentId"].ToString())
            };
        }


        public AgentContact CreateNewAgentContactDataModel()
        {
            return new AgentContact()
            {
                DataId = Guid.NewGuid(),
                AgentId = DataId,
                IsMain = true,
                Name = AgentContact.Name,
                Mobile = AgentContact.Mobile,
                Tel = AgentContact.Tel,
                Email = AgentContact.Email,
                QQ = AgentContact.QQ,
                Description = AgentContact.Description
            };
        }
        #endregion
    }

}

