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
    [Serializable]
    [DBTable("Sys_UserRole")]
    public class Sys_UserRole: EntityBase, IDataModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
