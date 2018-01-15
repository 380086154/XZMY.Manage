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
using XZMY.Manage.Model.ViewModel.Sys;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Service.Sys;

namespace XZMY.Manage.Web.Controllers.Sys
{
    /// <summary>
    /// 分店
    /// </summary>
    public class BranchController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "分店管理", Code = "BranchList", ModuleCode = "BranchList", Url = "/Branch/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //删除
        public ActionResult Delete(Guid? id)
        {
            var flag = true;
            var res = 0;
            if (id.HasValue)
            {
                var databaseManageService = new DatabaseManageService();
                res = databaseManageService.ClearDatabase(id.Value);
                //flag = res > 0;
            }
            return Json(new { success = flag, count = res, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new CustomSearchWithPaginationService<BranchDto>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<BranchDto>>
                {
                    new CustomConditionPlus<BranchDto>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<BranchDto, object>>[] { x => x.Name, x=>x.Value, x=>x.DataId }
                    }
                },
                SortMember = new Expression<Func<BranchDto, object>>[] { x => x.CreatedTime }
            };

            if (model.State.GetHashCode() > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<BranchDto>
                {
                    Value = model.State.GetHashCode(),
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<BranchDto, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new BranchDto();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<BranchDto>(id.Value);
                entity = service.Invoke() ?? new BranchDto();
            }

            return View(entity);
        }
    }
}