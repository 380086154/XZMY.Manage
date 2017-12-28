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
    public class LoginController : ControllerBase
    {
        //开发者ID(AppID) - wxdfadf3e2ae2aeb01
        const string Token = "E17680A936674932B358";

        /// <summary>
        /// 
        /// </summary>
        public LoginController()
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

        //登录
        public ActionResult AjaxLogin(VmLogin model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "操作失败");
                return Json(new { status = false, errors = GetErrors() });
            }

            var service = new CustomSearchService<UserAccount>();
            service.CustomConditions = new List<CustomCondition<UserAccount>>
            {
                new CustomConditionPlus<UserAccount> { Operation = SqlOperation.Equals,Value = model.LoginName.Trim(),
                    Member = new System.Linq.Expressions.Expression<Func<UserAccount, object>>[] {
                        m=>m.LoginName,m=>m.Mobile
                    }
                }
            };

            var result = service.Invoke();

            if (result == null)
            {
                return Json(new { status = false, errors = "帐号不存在" });
            }

            var account = result.FirstOrDefault();
            if (account == null)
            {
                return Json(new { status = false, errors = "帐号或密码错误" });
            }
            if (account.Password != model.Password.ToMd5())
            {
                return Json(new { status = false, errors = "帐号或密码错误" });
            }
            LoggedUserManager.SetCurrentUserAccount(account.DataId, Request.UserHostAddress);

            var urlReferrer = Request.UrlReferrer;

            var url = "";
            if (urlReferrer != null)
            {
                url = urlReferrer.Query;
                var ruery = url.Split('=');
                if (ruery.Count() > 1) url = HttpUtility.UrlDecode(ruery[1]);
            }

            return Json(new { success = true, url = url });
        }

        //退出
        public ActionResult AjaxLoginout()
        {

            return View();
        }
    }
}