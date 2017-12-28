using System;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;

namespace XZMY.Manage.Service.Auth.Models.ViewModel
{
    /// <summary>
    /// 角色创建/编辑
    /// </summary>
    public class VmRoleEdit : ViewBase, IViewModel<Sys_Role>
    {
        public Guid DataId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EState State { get; set; }

        /// <summary>
        /// 模块Id列表，用逗号分隔
        /// </summary>
        public string Modules { get; set; }
        /// <summary>
        /// 行为Id列表，用逗号分隔
        /// </summary>
        public string Actions { get; set; }
    }
}
