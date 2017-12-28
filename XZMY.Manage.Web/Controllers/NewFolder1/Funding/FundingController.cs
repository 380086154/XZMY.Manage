using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Funding;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Funding;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Funding
{
    //基金
    public class FundingController : ControllerBase
    {
        // GET: Funding
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }


        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new GetEntityListService<MemberFund>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                SortMember = new Expression<Func<MemberFund, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }


        //创建/编辑 
        [HttpPost]
        public ActionResult AjaxEdit(VmMemberFund model)
        {
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<MemberFund>(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                return RedirectToAction("Edit/" + model.DataId);
            }
            else
            {
                var handler = new BaseModifyHandler<MemberFund>(model);
                var res = handler.Invoke();

                return Json(new { success = res.Success, errors = GetErrors() });
            }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", "操作失败");
            //return Json(new { status = false, errors = GetErrors() });
        }
    }
}