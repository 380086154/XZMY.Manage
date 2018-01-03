using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Xml;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Web.Controllers.Apis.Message;
using XZMY.Manage.Web.Controllers.Apis.Tools;
using XZMY.Manage.Web.Models.Api;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 客户接口
    /// </summary>
    public class WeixinController : ApiControllerBase
    {
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        const string Token = "E17680A936674932B358";

        [HttpGet]
        [HttpPost]
        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
                return;
            
            var stream = context.Request.InputStream;
            var byteArray = new byte[stream.Length];
            stream.Read(byteArray, 0, (int)stream.Length);
            var postXmlstr = System.Text.Encoding.UTF8.GetString(byteArray);

            if (!string.IsNullOrWhiteSpace(postXmlstr))
            {
                Valid(context);
                return;
            }

            var doc = new XmlDocument();
            doc.LoadXml(postXmlstr);

            ResponseMessage(context, doc);
        }

        //无返回值
        public void Valid(HttpContext context)
        {
            var echostr = (context.Request["echoStr"] ?? "").ToString();
            if (CheckSignature(context.Request) && !string.IsNullOrWhiteSpace(echostr))
            {
                context.Response.Write(echostr);
                context.Response.Flush();//推送...不然微信平台无法验证 Token
            }
        }

        public void ResponseMessage(HttpContext context, XmlDocument doc)
        {
            var result = "";
            var type = WeixinXml.GetFromXml(doc, "MsgType");

            switch (type)
            {
                case "subscribe"://订阅
                    break;
                case "unsubscribe"://取消订阅
                    break;
                case "CLICK":
                    break;
                case "text":
                    var text = WeixinXml.GetFromXml(doc, "Content");
                    if (text == "查余额" || text == "余额")
                    {
                        result = WeixinXml.CreateTextMessage(doc, "余额查询中");
                    }
                    break;
                default:
                    break;
            }
        }




        /// <summary>
        /// 处理信息并应答
        /// </summary>
        private void Handle(HttpContext current, string postStr)
        {
            var request = current.Request;
            var response = current.Response;

            var help = new MessageBase();
            string responseContent = help.ReturnMessage(postStr);

            response.ContentEncoding = Encoding.UTF8;
            response.Write(responseContent);
        }

        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetTime()
        {
            return Success("DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [HttpGet]
        public ApiResult Verify()
        {
            var current = HttpContext.Current;
            if (current == null)
                return null;

            var request = current.Request;
            var response = current.Response;

            string echoStr = (request.QueryString["echoStr"] ?? "").ToString();

            if (CheckSignature(request) && !string.IsNullOrEmpty(echoStr))
            {
                response.Write(echoStr);
                response.End();
            }
            return null;
        }

        private bool CheckSignature(HttpRequest request)
        {
            string signature = (request.QueryString["signature"] ?? "").ToString();
            string timestamp = (request.QueryString["timestamp"] ?? "").ToString();
            string nonce = (request.QueryString["nonce"] ?? "").ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();

            return tmpStr == signature;
        }

    }
}