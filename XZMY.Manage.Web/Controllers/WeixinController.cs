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
using XZMY.Manage.Log.Models;
using System.Web.Security;
using System.Xml;
using XZMY.Manage.Web.Controllers.Apis.Tools;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class WeixinController : Controller
    {
        //微信号 - fuck-you-too
        //发送方帐号 - oYVeUwOSNj7wCFrvZMPbW8SBA-Y8
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        //EncodingAESKey - kHYlTj3tNzB2hprUeUf5bd6K8MMJfEa1wztACFyQJAr
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
                LogHelper.LogException("ProcessRequest 异常", ex.StackTrace, LogLevel.Error, ex);
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

            switch (type)
            {
                case "event"://
                    break;
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
                        content = "余额查询中";
                    }
                    else if (text == "时间" || text == "几点了")
                    {
                        content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (text == "作者")
                    {
                        content = "Xiaoping.Liu";
                    }
                    break;
                default:
                    break;
            }
            var result = WeixinXml.CreateTextMessage(doc, content);
            LogHelper.Log("ProcessRequest 日志：" + type, result, LogLevel.Debug);
            Response.Write(result);
            Response.Flush();
        }

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