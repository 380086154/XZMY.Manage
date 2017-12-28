using System;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;

namespace XZMY.Manage.Service.Auth.Models.ViewModel
{
    public class VmActionEdit : IViewModel<Sys_Action>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid DataId { get; set; }

        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ModuleCode { get; set; }
        public string Url { get; set; }

        public int Sort { get; set; }
        public EVisible Visible { get; set; }
        public EState State { get; set; }
    }
}
