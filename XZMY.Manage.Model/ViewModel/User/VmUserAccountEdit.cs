using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.User
{
    public class VmUserAccountEdit : ViewBase, IActionViewModel<UserAccount>
    {
        public Guid DataId { get; set; }
        public int Code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Zipcode { get; set; }
        public string Location { get; set; }
        public EGender Gender { get; set; }
        public int Source { get; set; }
        public EState State { get; set; }

        /// <summary>
        /// 角色id，用半角逗号分隔
        /// </summary>
        public string Roles { get; set; }

        #region Extendsions

        public List<Guid> GetRoleIdList()
        {
            try
            {
                if (Roles == null) return null;
                return Roles.Split(",").Select(m => m.ToGuid()).Where(m => m != null).Select(m => m.Value).ToList();
            }
            catch
            {
                return null;
            }
        }

        public UserAccount CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return new UserAccount
            {
                DataId = DataId,
                LoginName = LoginName,
                RealName = RealName,
                Password = Password.ToMd5(),
                Code = Code,
                Mobile = Mobile,
                Email = Email,
                QQ = QQ,
                Gender = Gender,
                Location = Location,
                Zipcode = Zipcode,
                RegisteredTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                Source = Source,
                State = State,
            };
        }

        public UserAccount MergeDataModel(UserAccount model)
        {
            model.Email = Email;
            model.Gender = Gender;
            model.Location = Location;
            //model.LoginName = LoginName;
            if (!String.IsNullOrWhiteSpace(Password))
            {
                model.Password = Password.ToMd5();
            }
            model.Mobile = Mobile;
            model.QQ = QQ;
            model.RealName = RealName;
            model.Source = Source;
            model.State = State;
            model.Zipcode = Zipcode;

            return model;
        }

        #endregion
    }
}
