
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
    public class VmMember : ViewBase, IActionViewModel<Member>
    {
        #region Properties 

        /// <summary>
        /// 主键
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("主键")] 
        public Guid DataId { get; set; }

        /// <summary>
        /// 代理商ID 
        /// </summary>
        public Guid AgentId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        //[EntAttributes.DBColumn("LoginName")] 
        //[DisplayName("登录名")] 
        public String LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        //[EntAttributes.DBColumn("Password")] 
        //[DisplayName("登录密码")] 
        public String Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        //[EntAttributes.DBColumn("RealName")] 
        //[DisplayName("真实姓名")] 
        public String RealName { get; set; }
        /// <summary>
        /// 分类 ：1 学生  2 家长
        /// </summary>
        //[EntAttributes.DBColumn("Type")] 
        //[DisplayName("分类 ：1 学生  2 家长")] 
        public Int32 Type { get; set; }
        /// <summary>
        /// 分类 ：1 学生  2 家长
        /// </summary>
        public String TypeName {
            get {
                string strValue = "学生";
                switch (Type)
                {
                    case 1:
                        strValue = "学生";
                        break;
                    case 2:
                        strValue = "家长";
                        break;
                }
                return strValue;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String StateName {
            get {
                string strValue = "其它";
                switch (State)
                {
                    case EState.启用:
                        strValue = "启用";
                        break;
                    case EState.禁用:
                        strValue = "禁用";
                        break;
                    case EState.其它:
                        strValue = "其它";
                        break;
                }
                return strValue;
            }
        }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        public EGender Gender { get; set; }
        public String GenderName {
            get {
                string strValue = "保密";

                switch (Gender)
                {
                    case EGender.保密:
                        strValue = "保密";
                        break;
                    case EGender.女:
                        strValue = "女";
                        break;
                    case EGender.男:
                        strValue = "男";
                        break;
                }
                return strValue;
            }
        }
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
        /// 注册时间
        /// </summary>
        //[EntAttributes.DBColumn("RegisteredTime")] 
        //[DisplayName("注册时间")] 
        public DateTime RegisteredTime { get; set; }

        #endregion

        #region Extendsions

        public Member CreateNewDataModel()
        {
            var model = new Member();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.AgentId = AgentId;
            model.LoginName = LoginName;
            model.Password = Password.ToMd5();
            model.RealName = RealName;
            model.Type = Type;
            model.Email = Email;
            model.Mobile = Mobile;
            model.RegisteredTime = RegisteredTime;
            return model;
        }

        public Member MergeDataModel(Member model)
        {
            model.LoginName = LoginName;
            model.Password = Password;
            model.RealName = RealName;
            model.Gender = Gender;
            model.Type = Type;
            model.Email = Email;
            model.Mobile = Mobile;
            model.State = State;
            model.RegisteredTime = RegisteredTime;
            return model;
        }
        #endregion
    }

}