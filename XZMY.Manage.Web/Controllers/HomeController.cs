﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel.Customer;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Customer;
using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            GetBranch();
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

        /// <summary>
        /// 获取分店信息
        /// </summary>
        private void GetBranch()
        {
            ViewBag.IsAdmin = IsAdmin;
            if (!IsAdmin) return;

            var service = new CustomSearchWithPaginationService<BranchDto>
            {
                PageIndex = 1,
                PageSize = 20,
                CustomConditions = new List<CustomCondition<BranchDto>>
                {
                    new CustomConditionPlus<BranchDto>
                    {
                        Value = EState.启用,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<BranchDto, object>>[] { x => x.State }
                    }
                },
                SortMember = new Expression<Func<BranchDto, object>>[] { x => x.Name }
            };
            ViewBag.BranchList = service.Invoke().Results;
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

        public ActionResult GetKeywords(string keywords)
        {
            var service = new HyxxQuickSearchService();

            var result = service.GetKeywords(new VmQuickSearch
            {
                PageIndex = 1,
                PageSize = 50,
                Keywords = keywords,
                BranchDataId = this.CurrentBranchDataId
            });

            //var list = new List<string>();
            //foreach (var item in result.Results)
            //{
            //    list.Add(item.hykh.Trim() + "-" + item.hyxm.Trim());
            //}

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Knock()
        {
            return Json(new { reply = "Who's knocking at the door." }, JsonRequestBehavior.AllowGet);
        }
    }
}