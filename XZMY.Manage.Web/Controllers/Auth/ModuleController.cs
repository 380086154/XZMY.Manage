using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Handlers.Module;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Auth
{
    public class ModuleController : ControllerBase
    {
        //创建/编辑
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new Sys_Module();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Sys_Module>(id.Value);
                entity = service.Invoke();
            }
            return View(entity);
        }

        //删除

        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmModuleEdit model)
        {
            if (model.DataId == Guid.Empty)
            {
                var handler = new ModuleCreateHandler(model);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                //var handler = new ModuleModifyHandler(model);
                var handler = new ModuleBaseModifyHandler(model);
                var res = handler.Invoke();

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
        }

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string name)
        {
            var service = new GetEntityBySingleColumnService<Sys_Module> { ColumnMember = x => x.Name, ColumnValue = name };

            var result = service.Invoke();
            var flag = false;
            var entity = result.FirstOrDefault(x => x.DataId != id);
            if (result.Count > 0 && entity != null && id != entity.DataId)
            {
                flag = true;
            }

            return Json(flag ? "已存在" : "true", JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new Sys_Action();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Sys_Action>(id.Value);
                entity = service.Invoke() ?? new Sys_Action();
            }

            return View(entity);
        }
    }
}