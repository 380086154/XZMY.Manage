using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models.WxMessage
{
    /// <summary>
    /// JS_SDK 票据
    /// </summary>
    public class JsTicketResult
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }

        /// <summary>
        /// JsTicket票据
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 过期时间 7200秒
        /// </summary>
        public string expires { get; set; }
    }
}
