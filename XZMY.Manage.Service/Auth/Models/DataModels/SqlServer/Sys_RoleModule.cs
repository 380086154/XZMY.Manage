using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
using T2M.CoastLine.Utils.Model.Attributes;

namespace XZMY.Manage.Service.Auth.Models.DataModels.SqlServer
{
    [DBTable("Sys_RoleModule")]
    public class Sys_RoleModule : EntityBase, IDataModel
    {
        public Guid ModuleId { get; set; }
        public Guid RoleId { get; set; }
    }
}
