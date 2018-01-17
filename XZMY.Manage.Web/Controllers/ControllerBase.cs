using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XZMY.Manage.Service;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Web.Utils;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    public class ControllerBase : Controller
    {
        private static Dictionary<string, Dictionary<string, AutoCreateAuthActionAttribute>> AUTH_RES;

        /// <summary>
        /// 是否管理员
        /// </summary>
        protected bool IsAdmin { get { return LoggedUserManager.IsAdmin; } }

        /// <summary>
        /// 当前分店 Id
        /// </summary>
        protected Guid CurrentBranchDataId { get { return LoggedUserManager.GetCookieBranchDataId(); } }

        /// <summary>
        /// 当前登录用户 Id
        /// </summary>
        protected Guid CurrentAccountId { get { return LoggedUserManager.GetCurrentUserAccount().AccountId; } }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected CurrentUserAccountModel CurrentAccount { get { return LoggedUserManager.GetCurrentUserAccount(); } }

        static ControllerBase()
        {
            AUTH_RES = new Dictionary<string, Dictionary<string, AutoCreateAuthActionAttribute>>();
            var ass = Assembly.GetExecutingAssembly();
            var ctrls = ass.GetTypes().Where(m => m.IsSubclassOf(typeof(ControllerBase)));
            foreach (var item in ctrls)
            {
                AUTH_RES[item.Name.ToUpper()] = new Dictionary<string, AutoCreateAuthActionAttribute>();
                var acts = item.GetMethods().Where(m => m.GetCustomAttribute<AutoCreateAuthActionAttribute>() != null).ToList();
                foreach (var act in acts)
                {
                    var attr = act.GetCustomAttribute<AutoCreateAuthActionAttribute>();
                    AUTH_RES[item.Name.ToUpper()].Add(act.Name.ToUpper(), attr);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var current = LoggedUserManager.GetCurrentUserAccount();
            if (string.IsNullOrEmpty(current.Name))
            {
                ViewBag.LoginName = current.AccountName;
            }
            else
            {
                ViewBag.LoginName = current.Name;
            }
            var bl = AuthorityCheck();
            var controllerName = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).ControllerDescriptor.ControllerName;
            if (controllerName != "Login")
            {
                if (!LoggedUserManager.IsLogin())
                    filterContext.Result = RedirectToRoute(new { Controller = "Login", Action = "Index", go = Request.Url.ToString() });
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AuthorityCheck()
        {
            var ctrl = this.GetControllerName() + "Controller";
            var act = this.GetActionName();
            var attr = GetAuthAttribute(ctrl.ToUpper(), act.ToUpper());
            if (attr == null) return true;
            var current = LoggedUserManager.GetCurrentUserAccount();
            if (String.IsNullOrEmpty(current.Name))
            {
                ViewBag.LoginName = current.AccountName;
            }
            else
            {
                ViewBag.LoginName = current.Name;
            }
            return AuthCenter.ActionAuthorityCheck(current.AccountId, attr.ModuleCode, attr.Code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        private AutoCreateAuthActionAttribute GetAuthAttribute(string ctrl, string act)
        {
            var current = LoggedUserManager.GetCurrentUserAccount();
            if (String.IsNullOrEmpty(current.Name))
            {
                ViewBag.LoginName = current.AccountName;
            }
            else
            {
                ViewBag.LoginName = current.Name;
            }
            if (!AUTH_RES.ContainsKey(ctrl)) return null;
            var dic = AUTH_RES[ctrl];
            if (!dic.ContainsKey(act)) return null;
            return dic[act];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected List<KeyValuePair<string, string>> GetErrors()
        {
            var errorList = new List<KeyValuePair<string, string>>();
            //获取所有错误的Key
            var keys = ModelState.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in keys)
            {
                var errors = ModelState[key].Errors.ToList();
                if (errors.Count > 0)
                {
                    errorList.Add(new KeyValuePair<string, string>(key, string.Join(",", errors.Select(i => i.ErrorMessage))));
                }
            }
            return errorList;
        }
    }
}