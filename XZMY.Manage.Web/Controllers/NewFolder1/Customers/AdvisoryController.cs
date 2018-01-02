using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Advisories;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Advisories;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Customers
{
    //咨询留言
    public class AdvisoryController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "咨询列表", Code = "AdvisoryList", ModuleCode = "ADVISORY", Url = "/Advisory/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //创建/编辑
        [HttpGet]
        [AutoCreateAuthAction(Name = "创建咨询", Code = "AdvisoryEdit", ModuleCode = "ADVISORY", Url = "/Advisory/Edit", Visible = true, Remark = "")]
        public ActionResult Edit(Guid? id)
        {
            var entity = new Advisory();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Advisory>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Advisory, VmAdvisory>());
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new Advisory();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Advisory>(id.Value);
                entity = service.Invoke() ?? new Advisory();
            }

            return View(entity);
        }
        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmAdvisory model)
        {
            //if (ModelState.IsValid)
            //{
                if (model.DataId == Guid.Empty)
                {
                    var handler = new BaseCreateHandler<Advisory>(model);
                    var res = handler.Invoke();

                    if (res.Code != 0)
                    {
                        return Json(new { success = false, errors = GetErrors() });
                    }

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
                else
                {
                    var handler = new BaseModifyHandler<Advisory>(model);
                    var res = handler.Invoke();

                    return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
                }
            //}
            //return Json(new { status = false, errors = GetErrors() });
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string title)
        {
            var service = new GetEntityBySingleColumnService<Advisory> { ColumnMember = x => x.Title, ColumnValue = title };

            var result = service.Invoke();
            var flag = false;
            var entity = result.FirstOrDefault(x => x.DataId != id);
            if (result.Count > 0 && entity != null && id != entity.DataId)
            {
                flag = true;
            }

            return Json(flag ? "已存在" : "true", JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmSearchBase<Advisory> model)
        {
            var service = new CustomSearchWithPaginationService<Advisory>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Advisory>>
                {
                    new CustomConditionPlus<Advisory>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Advisory, object>>[] { x => x.Title,x=>x.Description }
                    }
                },
                SortMember = new Expression<Func<Advisory, object>>[] { x => x.CreatedTime }
            };

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        
    }
}