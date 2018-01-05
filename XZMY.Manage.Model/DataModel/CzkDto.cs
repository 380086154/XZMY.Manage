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
    /// 充值卡
    /// </summary>
    [Serializable]
    [DBTable("Czk")]
    public partial class CzkDto : EntityBase, IDataModel
    {
        public CzkDto()
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
        /// 优惠种类
        /// </summary>
        public string yhzl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yhl { get; set; }
        public decimal ljxf { get; set; }
        public decimal zhje { get; set; }
        public decimal yhzlid { get; set; }
        public decimal yetx { get; set; }
    }
}
