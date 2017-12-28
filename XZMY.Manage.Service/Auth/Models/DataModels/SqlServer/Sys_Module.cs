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
    [DBTable("Sys_Module")]
    [Serializable]
    public class Sys_Module : EntityBase, IDataModel
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 字体图标样式
        /// </summary>
        public string FontIconsClass { get; set; }
        public string Description { get; set; }
        public EState State { get; set; }
        public EVisible Visible { get; set; }
        public int Sort { get; set; }
    }
}
