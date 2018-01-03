using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Weixin.Models.WxMessage;

namespace XZMY.Manage.Weixin.Service
{
    /// <summary>
    /// 微信消息推送服务
    /// </summary>
    public interface IWxMessageApiService
    {
        /// <summary>
        /// 企业号发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        bool QySendMessage<T>(T msgModel) where T : QyMsgBase;

        /// <summary>
        /// 当用户每次进入企业应用时,发送推送消息
        /// </summary>
        /// <param name="xml">微信服务器传过来的数据包</param>
        /// <param name="sendMsg"></param>
        void EventPushByEnterAgent(string xml, string sendMsg);

        /// <summary>
        /// 获取企业号JS_SDK 签名数据包
        /// 例如{"appId":"wxf7ed638bb9d6b280","timestamp":1502962344,"nonceStr":"tF4XtH57Qkpfa2HC","signature":"155c2b626c67bd93a5b90b0ff0cd33dae47d41e6"}
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string GetQyJsapiTicket(string url);
    }
}
