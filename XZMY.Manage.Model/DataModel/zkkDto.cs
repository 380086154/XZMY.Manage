using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel
{
    /// <summary>
    /// 折扣卡
    /// </summary>
    [Serializable]
    [DBTable("Zkk")]
    public partial class ZkkDto : EntityBase, IDataModel
    {
        public ZkkDto()
        {

        }

        /// <summary>
        /// 分店Id
        /// </summary>
        public Guid BranchNameDataId { get; set; }
        
        public string id { get; set; }
        /// <summary>
        /// 卡名称
        /// </summary>
        public string kmc { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public string zkl { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public string jfbz { get; set; }
        public decimal sx { get; set; }
        public decimal xx { get; set; }
    }
}
