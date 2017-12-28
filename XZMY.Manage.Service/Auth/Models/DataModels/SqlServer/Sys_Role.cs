using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
using T2M.CoastLine.Utils.Model.Attributes;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Service.Auth.Models.DataModels.SqlServer
{
    [DBTable("Sys_Role")]
    public class Sys_Role : EntityBase, IDataModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public string StateName
        {
            get { return State.ToString(); }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
