using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XZMY.Manage.Web.Models.Api
{
    /// <summary>
    /// 会员信息
    /// </summary>
    public class HyxxApiModel : IApiResultItems
    {
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string hykh { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string hyxm { get; set; }
        /// <summary>
        /// 会员级别
        /// </summary>
        public string kmc { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal knje { get; set; }
        /// <summary>
        /// 累计金额
        /// </summary>
        public decimal hyje { get; set; }
        /// <summary>
        /// 开卡日期
        /// </summary>
        public DateTime jrrq { get; set; }
    }
}