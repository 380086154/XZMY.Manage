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
    /// 会员快速查询信息
    /// </summary>
    [Serializable]
    [DBTable("Hyxx")]
    public class HyxxQuickSearchDto : EntityBase, IDataModel
    {
        /// <summary>
        /// 分店Id
        /// </summary>
        public Guid BranchDataId { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string hykh { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string hyxm { get; set; }
        /// <summary>
        /// 姓名简码
        /// </summary>
        public string xmjm { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string yddh { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string klxmc { get; set; }
        /// <summary>
        /// 会员级别
        /// </summary>
        public string kmc { get; set; }
    }
}
