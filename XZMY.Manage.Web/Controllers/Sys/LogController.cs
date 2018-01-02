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

namespace XZMY.Manage.Web.Controllers.Sys
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class LogController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "日志管理", Code = "LogList", ModuleCode = "SYSTEM", Url = "/Log/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //删除
        public ActionResult Delete(Guid? id)
        {
            var flag = false;
            if (id.HasValue)
            {
                var handler = new BaseDeleteService<LogEntity>(id.Value);
                var res = handler.Invoke();
                flag = res;
            }
            return Json(new { success = flag, Id = id, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //列表 Ajax 获取数据
        public ActionResult AjaxList(VmLog model)
        {
            var service = new CustomSearchWithPaginationService<LogEntity>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<LogEntity>>
                {
                    new CustomConditionPlus<LogEntity>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<LogEntity, object>>[] { x => x.Title,x=>x.Message, x=>x.UserName }
                    }
                },
                SortMember = new Expression<Func<LogEntity, object>>[] { x => x.CreatedTime }
            };

            if (model.Level > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<LogEntity>
                {
                    Value = model.Level,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<LogEntity, object>>[] { x => x.Level }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new LogEntity();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<LogEntity>(id.Value);
                entity = service.Invoke() ?? new LogEntity();
            }

            return View(entity);
        }
    }
}