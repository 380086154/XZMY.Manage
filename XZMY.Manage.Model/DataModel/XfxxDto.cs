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
        public Guid BranchNameDataId { get; set; }
        
        public string id { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string hykh { get; set; }
        public decimal xfje { get; set; }
        public DateTime xfrq { get; set; }
        public decimal dzhje { get; set; }
        public decimal sdjf { get; set; }
        public decimal sdje { get; set; }
        public string bz { get; set; }
        public string czy { get; set; }
        public string jsrjf { get; set; }
        public string fdid { get; set; }
        public decimal je { get; set; }
        public decimal xj { get; set; }
        public decimal czk { get; set; }
        public decimal xyk { get; set; }
        public decimal djq { get; set; }
    }
}
