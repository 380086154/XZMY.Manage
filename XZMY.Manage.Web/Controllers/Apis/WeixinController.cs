using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Web.Models.Api;

namespace XZMY.Manage.Web.Controllers.Apis
{
    /// <summary>
    /// 客户接口
    /// </summary>
    public class WeixinController : ApiControllerBase
    {
        public ApiResult Weixin()
        {
            //string echoStr = (Request.QueryString["echoStr"] ?? "").ToString();

            //if (CheckSignature() && !string.IsNullOrEmpty(echoStr))
            //{
            //    Response.Write(echoStr);
            //    Response.End();
            //}
            return null;
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

        //private bool CheckSignature()
        //{
        //    string signature = (Request.QueryString["signature"] ?? "").ToString();
        //    string timestamp = (Request.QueryString["timestamp"] ?? "").ToString();
        //    string nonce = (Request.QueryString["nonce"] ?? "").ToString();
        //    string[] ArrTmp = { Token, timestamp, nonce };
        //    Array.Sort(ArrTmp);
        //    string tmpStr = string.Join("", ArrTmp);
        //    tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();

        //    return tmpStr == signature;
        //}

    }
}