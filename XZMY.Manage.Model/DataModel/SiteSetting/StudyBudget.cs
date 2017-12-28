using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 留学预算
    /// </summary>
    [DBTable("StudyBudget")]
    public class StudyBudget : EntityBase, IDataModel
    {
        /// <summary>
        /// 留学预算名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public int Budget { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get;set;}

    }
}
