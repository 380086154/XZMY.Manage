using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
using T2M.CoastLine.Utils.Model.Attributes;

namespace XZMY.Manage.Service.Auth.Models.DataModels.SqlServer
{
    /// <summary>
    /// 动作
    /// </summary>
    [Serializable]
    [DBTable("Sys_Action")]
    public class Sys_Action : EntityBase, IDataModel
    {
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ModuleCode { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public EState State { get; set; }
        public EVisible Visible { get; set; }
        public int Sort { get; set; }
    }
}
