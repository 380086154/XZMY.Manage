using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.SiteSetting;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers.SiteSetting;
using XZMY.Manage.Service.Handlers.User;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;


namespace XZMY.Manage.Web.Controllers.SiteSetting
{
    /// <summary>
    /// 网站广告
    /// </summary>
    public class SiteADController : ControllerBase
    {
        //[AutoCreateAuthAction(Name = "广告列表", Code = "SiteADList", ModuleCode = "AD", Url = "/SiteAD/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new SiteAD();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<SiteAD>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<SiteAD, VmSiteADEdit>());
        }
        public ActionResult EditAd(Guid? id)
        {
            var entity = new SiteAD();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<SiteAD>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<SiteAD, VmSiteADEdit>());
            // string id2 = Request["id"].ToString();
            //return View();
        }

        [HttpPost]
        public ActionResult Edit(VmSiteADEdit model)
        {
            var handler = new SiteADCreateHandler(model);
            var res = handler.Invoke();

            return View();
        }

        public ActionResult Modify()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Modify(VmSiteADEdit model)
        {
            var handler = new SiteADModifyHandler(model);
            var res = handler.Invoke();

            return View();
        }

        //用户 创建/编辑 
        [HttpPost]
        public ActionResult AjaxEdit(VmSiteADEdit model)
        {
            if (Request.Params["hid_photo_ImageUrl[]"] != null)
            {
                model.ImageUrl = Request.Params["hid_photo_ImageUrl[]"].ToString();
            }
            if (Request.Params["hid_photo_Url[]"] != null)
            {
                model.Url = Request.Params["hid_photo_Url[]"].ToString();
            }
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new SiteADCreateHandler(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new SiteADModifyHandler(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}

            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", model.ErrorMessage);
            //return Json(new { status = false, errors = GetErrors() });
        }
        public ActionResult AjaxList(VmSiteADEdit model)
        {
            var service = new CustomSearchWithPaginationService<SiteAD>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<SiteAD>>
                {
                    new CustomConditionPlus<SiteAD>
                    {
                        Value = model.Keywords ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<SiteAD, object>>[] { x => x.Name,x=>x.Code }
                    }
                },
                SortMember = new Expression<Func<SiteAD, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<SiteAD>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<SiteAD, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid? id)
        {
            var entity = new SiteAD();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<SiteAD>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<SiteAD, VmSiteADEdit>());
        }
    }
}