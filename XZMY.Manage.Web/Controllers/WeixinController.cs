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

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class WeixinController : Controller
    {
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        const string Token = "E17680A936674932B358";

        /// <summary>
        /// 
        /// </summary>
        public WeixinController()
        {
            LoggedUserManager.Loginout();
        }

        //登录页
        public ActionResult Index()
        {
            var echoStr = (Request.QueryString["echoStr"] ?? "").ToString();

            if (CheckSignature() && !string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }

            var weixin = new WeixinHandler(Request);

            Response.Write("测试信息：" + echoStr);
            Response.End();

            //string responseMsg = weixin.Response();
            //context.Response.Clear();
            //context.Response.Charset = "UTF-8";
            //context.Response.Write(responseMsg);
            //context.Response.End();

            return View();
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