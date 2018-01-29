using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //加载 菜单 内容
        public ActionResult AjaxMenuList()
        {
            var resourceList = new List<VmMenu>();
            var currentUserAccount = LoggedUserManager.GetCurrentUserAccount();

            //获取所有资源信息
            var ads = new AuthDataSource();
            #region 初版
            //{
            //    var modules = ads.GetAllModules();
            //    var actions = ads.GetAllActions();

            //    foreach (var item in modules)
            //    {
            //        if (item.Visible == EVisible.隐藏 || item.State == EState.禁用) continue;

            //        resourceList.Add(new VmMenu
            //        {
            //            id = item.Id,
            //            pId = item.ParentId,
            //            name = item.Name,
            //            open = true,

            //            FontIconsClass = item.FontIconsClass,
            //            Type = "1",
            //            Sort = item.Sort,
            //            Visible = item.Visible.GetHashCode(),
            //            State = item.State.GetHashCode()
            //        });

            //        resourceList.AddRange(actions.Where(x => x.ModuleId == item.Id && x.Visible == EVisible.显示 && x.State == EState.启用).Select(x => new VmMenu
            //        {
            //            id = x.Id,
            //            pId = item.Id,
            //            name = x.Name,
            //            open = true,

            //            Url = x.Url,
            //            Type = "2",
            //            Sort = x.Sort,
            //            Visible = x.Visible.GetHashCode(),
            //            State = x.State.GetHashCode()
            //        }));
            //    }
            //}
            #endregion

            {
                var userResource = ads.GetUserResource(currentUserAccount.AccountId);
                //userResource.Merge();
                foreach (var item in userResource.Menu.Modules.OrderBy(x => x.Sort))
                {
                    if (item.Visible == EVisible.隐藏 || item.State == EState.禁用) continue;

                    resourceList.Add(new VmMenu
                    {
                        id = item.Id,
                        //pId = item.ParentId,
                        name = item.Name,
                        open = true,

                        FontIconsClass = item.FontIconsClass,
                        Type = "1",
                        Sort = item.Sort,
                        Visible = item.Visible.GetHashCode(),
                        State = item.State.GetHashCode()
                    });

                    resourceList.AddRange(item.Items.Where(x => x.Visible == EVisible.显示 && x.State == EState.启用).OrderBy(x => x.Sort).Select(x => new VmMenu
                    {
                        id = x.Id,
                        pId = item.Id,
                        name = x.Name,
                        open = true,

                        Url = x.Url,
                        Type = "2",
                        Sort = x.Sort,
                        Visible = x.Visible.GetHashCode(),
                        State = x.State.GetHashCode()
                    }));
                }
            }

            return Json(new { success = true, rows = resourceList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Knock()
        {
            return Json(new { reply = "Who's knocking at the door." }, JsonRequestBehavior.AllowGet);
        }
    }
}