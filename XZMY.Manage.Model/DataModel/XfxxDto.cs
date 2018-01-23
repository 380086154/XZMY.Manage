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
    /// 消费信息
    /// </summary>
    [Serializable]
    [DBTable("Xfxx")]
    public partial class XfxxDto : EntityBase, IDataModel
    {
        public XfxxDto()
        {

        }

        /// <summary>
        /// 分店Id
        /// </summary>
        public Guid BranchDataId { get; set; }
        
        public string id { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string hykh { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string hyxm { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal xfje { get; set; }
        public DateTime xfrq { get; set; }
        public decimal dzhje { get; set; }
        public decimal sdjf { get; set; }
        public decimal sdje { get; set; }
        public string bz { get; set; }
        public string czy { get; set; }
        public string jsrjf { get; set; }
        public string fdid { get; set; }
        /// <summary>
        /// 累计消费金额
        /// </summary>
        public decimal je { get; set; }
        public decimal xj { get; set; }
        public decimal czk { get; set; }
        public decimal xyk { get; set; }
        public decimal djq { get; set; }
    }
}
