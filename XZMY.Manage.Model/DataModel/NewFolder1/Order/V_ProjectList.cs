using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Order
{
    /// <summary>
    /// 活动视图
    /// </summary>
    public class V_ProjectList : EntityBase, IDataModel
    {
        public String ProjectName { get; set; }
        public String Code { get; set; }
        public String ProjectPlaceName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Sponsor { get; set; }
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName  { get; set; }
        public string ProcessState { get; set; }
    }
}
