using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Weixin.Models;

namespace XZMY.Manage.Weixin.Service.Event
{
    /// <summary>
    /// 微信用户授权登录
    /// 注意：本事件必须是同步执行的事件
    /// </summary>
    public class WxMemberAuthorizeEvent : IEvent
    {
        public WxQyUserinfo WxUserinfo { get; set; }

        /// <summary>
        /// 第三方WebAPI Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 第三方WebAPI 错误码
        /// 为 0 表示无异常
        /// </summary>
        public int ErrorCode { get; set; }


        public WxMemberAuthorizeEvent(WxQyUserinfo wxUserinfo)
        {
            WxUserinfo = wxUserinfo;

            //本事件必须是同步执行的事件
            Async = false;
        }


        public bool CancelBubble { get; set; }
        public int Order { get; set; }
        public bool Async { get; set; }
    }
}
