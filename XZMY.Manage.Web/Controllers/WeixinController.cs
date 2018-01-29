using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Handlers.Weixin;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using System.Web.Security;
using System.Xml;
using XZMY.Manage.Service.Weixin.Tools;
using XZMY.Manage.Service.Weixin;
using XZMY.Manage.Log.Models;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class WeixinController : Controller
    {
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        //令牌(Token) - E17680A936674932B358
        //消息加解密密钥(EncodingAESKey) - kHYlTj3tNzB2hprUeUf5bd6K8MMJfEa1wztACFyQJAr
        const string Token = "E17680A936674932B358";

        public void ProcessRequest()
        {
            try
            {
                var stream = Request.InputStream;
                var byteArray = new byte[stream.Length];
                stream.Read(byteArray, 0, (int)stream.Length);
                var postXmlstr = System.Text.Encoding.UTF8.GetString(byteArray);

                if (string.IsNullOrWhiteSpace(postXmlstr))
                {
                    Valid();
                    return;
                }

                var doc = new XmlDocument();
                doc.LoadXml(postXmlstr);

                ResponseMessage(doc);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("WeixinController 异常", ex.Message, LogLevel.Debug, ex);
            }
        }

        //无返回值
        public void Valid()
        {
            var echostr = (Request["echoStr"] ?? "").ToString();
            if (CheckSignature() && !string.IsNullOrWhiteSpace(echostr))
            {
                Response.Write(echostr);
                Response.Flush();//推送...不然微信平台无法验证 Token
            }
        }

        public void ResponseMessage(XmlDocument doc)
        {
            var content = "";
            var type = WeixinXml.GetFromXml(doc, "MsgType");
            var text = WeixinXml.GetFromXml(doc, "Content");

            switch (type)
            {
                case "event"://
                    {
                        var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxdfadf3e2ae2aeb01&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";
                    }
                    break;
                case "subscribe"://订阅
                    break;
                case "unsubscribe"://取消订阅
                    break;
                case "CLICK":
                    break;
                case "text":
                    content = AutoReplyMessageService.Reply(doc);
                    break;
                default:
                    break;
            }

            LogHelper.Log("XmlDocument：" + type, Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc), LogLevel.Debug);
            //LogHelper.Log("WeixinController 日志：" + type, "说：“" + text + "”  Reply：" + content, LogLevel.Debug);

            if (!string.IsNullOrWhiteSpace(content))
            {
                var result = WeixinXml.CreateTextMessage(doc, content);
                Response.Write(result);
                Response.Flush();
            }
            else Valid();
        }

        //验证签名
        private bool CheckSignature()
        {
            string signature = (Request.QueryString["signature"] ?? "").ToString();
            string timestamp = (Request.QueryString["timestamp"] ?? "").ToString();
            string nonce = (Request.QueryString["nonce"] ?? "").ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();

            return tmpStr == signature;
        }
    }
}