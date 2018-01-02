using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Order
{
    /// <summary>
    /// 支付信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmPayInCome
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public String OrderNo { get; set; }
        /// <summary>
        /// 支付宝交易流水号
        /// </summary>
        public String TradeNo { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public String PayPrice { get; set; }
        /// <summary>
        /// 支付账号
        /// </summary>
        public String PayAccount { get; set; }
        /// <summary>
        /// 支付宝人姓名
        /// </summary>
        public String PayName { get; set; }
    }
}
