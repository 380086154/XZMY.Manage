using System;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.User
{
    public class VmUserRole : ViewBase, IActionViewModel<UserRole>
    {
        public Guid DataId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        public UserRole CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return new UserRole
            {
                DataId = DataId,
                UserId = UserId,
                RoleId = RoleId
            };
        }

        public UserRole MergeDataModel(UserRole model)
        {
            model.DataId = DataId;
            model.UserId = UserId;
            model.RoleId = RoleId;

            return model;
        }
    }
}
