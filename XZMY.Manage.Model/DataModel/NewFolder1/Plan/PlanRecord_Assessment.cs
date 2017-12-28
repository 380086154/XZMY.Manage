using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Plan
{
    [Serializable]
    [DBTable("PlanRecord_Assessment")]
    public class PlanRecord_Assessment : EntityBase, IDataModel
    {
        /// <summary>
        /// 规划ID
        /// </summary>
        public Guid PlanRecordId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 获得总分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime AssessmentTime { get; set; }
    }
}
