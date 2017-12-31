using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Location;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Location;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.ViewModel.Assessment;

namespace XZMY.Manage.Web.Controllers
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class HyxxController : ControllerBase
    {
        //客户列表
        [AutoCreateAuthAction(Name = "客户管理", Code = "HyxxList", ModuleCode = "SYSTEM", Url = "/Hyxx/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        [AutoCreateAuthAction(Name = "消费信息", Code = "PaymentList", ModuleCode = "SYSTEM", Url = "/Hyxx/Payment", Visible = true, Remark = "")]
        public ActionResult Payment(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        //客户列表 Ajax 获取数据
        public ActionResult AjaxCustomerList(VmSearchBase model)
        {
            var service = new CustomSearchWithPaginationService<HyxxDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<HyxxDto>>(),
                SortMember = new Expression<Func<HyxxDto, object>>[] { x => x.csrq }
            };

            if (string.IsNullOrWhiteSpace(model.Keyword))
            {
                service.CustomConditions.Add(new CustomConditionPlus<HyxxDto>
                {
                    Value = model.Keyword ?? string.Empty,
                    Operation = SqlOperation.Like,
                    Member = new Expression<Func<HyxxDto, object>>[] {
                            x => x.yddh,
                            x =>x.hyxm,
                            x =>x.xmjm,
                            x =>x.hykh,
                        }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //消费列表 Ajax 获取数据
        public ActionResult AjaxPaymentList(VmPayment model)
        {
            var service = new CustomSearchWithPaginationService<XfxxDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<XfxxDto>>
                {
                    new CustomConditionPlus<XfxxDto>
                    {
                        Value = model.Id ?? ViewBag.Id,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<XfxxDto, object>>[] {
                            x =>x.hykh,
                        }
                    }
                },
                SortMember = new Expression<Func<XfxxDto, object>>[] { x => x.xfrq }
            };

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new HyxxDto();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<HyxxDto>(id.Value);
                entity = service.Invoke() ?? new HyxxDto();
            }

            return View(entity);
        }
    }
}