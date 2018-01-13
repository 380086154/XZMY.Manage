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
    /// 会员信息
    /// </summary>
    [Serializable]
    [DBTable("Hyxx")]
    public partial class HyxxDto : EntityBase, IDataModel
    {
        public HyxxDto()
        {
        }
        public Guid DataId { get; set; }
        public Guid BranchDataId { get; set; }
        public long id { get; set; }
        public string fid { get; set; }
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
        public string xb { get; set; }
        /// <summary>
        /// 开卡日期
        /// </summary>
        public DateTime csrq { get; set; }
        public string dwzy { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string yddh { get; set; }
        public string gddh { get; set; }
        public string dzyj { get; set; }
        public string zjlx { get; set; }
        public string zjhm { get; set; }
        public string lxdz { get; set; }
        public string qtxx { get; set; }
        public bool klx { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string klxmc { get; set; }
        /// <summary>
        /// 会员级别
        /// </summary>
        public string kmc { get; set; }

        public decimal hyjf { get; set; }
        /// <summary>
        /// 累计金额
        /// </summary>
        public decimal hyje { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal knje { get; set; }
        /// <summary>
        /// 开卡日期
        /// </summary>
        public DateTime jrrq { get; set; }
        /// <summary>
        /// 消费次数
        /// </summary>
        public int count { get; set; }
        public decimal syje { get; set; }
        public string fdid { get; set; }
        public long zxzt { get; set; }
        public string bzxx { get; set; }
        public string idkh { get; set; }
        public string gly { get; set; }
    }
}
