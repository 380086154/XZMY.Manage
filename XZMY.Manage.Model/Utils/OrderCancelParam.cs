using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.Utils
{
    /// <summary>
    /// 取消订单
    /// </summary>
    public class OrderCancelParam
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        public int Type { get; set; }
    }
}
