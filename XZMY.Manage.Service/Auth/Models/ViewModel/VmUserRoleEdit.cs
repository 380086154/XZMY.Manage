using System;
using System.Security.Policy;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;

namespace XZMY.Manage.Service.Auth.Models.ViewModel
{
    [Serializable]
    public class VmUserRoleEdit : ViewBase, IActionViewModel<Sys_UserRole>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        #region Extendsions

        public Sys_UserRole CreateNewDataModel()
        {
            var model = new Sys_UserRole();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.DataId = DataId;
            model.UserId = UserId;
            model.RoleId = RoleId;
            return model;
        }

        public Sys_UserRole MergeDataModel(Sys_UserRole model)
        {
            model.UserId = UserId;
            model.RoleId = RoleId;
            return model;
        }

        #endregion
    }
}
