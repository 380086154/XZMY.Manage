using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Program
{
    /// <summary>
    /// 代报名使用的活动列表
    /// </summary>
    [DBTable("V_AgentProject")]
    public class AgentProject : EntityBase, IDataModel
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 活动编码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 活动成长项
        /// </summary>
        public String ScoreItemNames { get; set; }
        /// <summary>
        /// 活动主办方
        /// </summary>
        public String Sponsor { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
