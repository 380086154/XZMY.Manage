using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Handlers;
using XZMY.Manage.Service.Handlers.User;
using XZMY.Manage.Service.Handlers.UserRole;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Auth
{
    public class UserAccountController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "人员管理", Code = "UserAccountList", ModuleCode = "SYSTEM", Url = "/UserAccount/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //角色赋值
        public ActionResult Assignment(Guid? id)
        {
            var entity = new UserAccount();

            if (id.HasValue)
            {
                {
                    var service = new GetEntityByIdService<UserAccount>(id.Value);
                    entity = service.Invoke() ?? new UserAccount();

                    ViewBag.Id = id.Value;
                    ViewBag.Name = string.IsNullOrWhiteSpace(entity.LoginName) ? "当前管理员" : entity.LoginName;
                }
                {
                    var ads = new AuthDataSource();
                    var list = ads.GetUserResource(entity.DataId);
                    var service = new GetEntityListService<Sys_Role>
                    {
                        PageIndex = 1,
                        PageSize = 100,
                    };
                    var result = service.Invoke();

                    ViewBag.RoleList = result.Results.OrderBy(x => x.Name).ToList();//所有角色
                    ViewBag.RoleIdList = list.Roles;//当前用户角色
                }
            }

            return View();
        }

        //保存角色用户
        public ActionResult AjaxSaveAssignment()
        {
            var idList = (Request.Params["idList[]"] ?? "").Split(',').Select(x => x.Trim().ToGuid(Guid.Empty)).Where(x => x != Guid.Empty);
            var userId = (Request.Params["userId"] ?? "").ToGuid(Guid.Empty);

            var list = idList.Select(item => new VmUserRoleEdit
            {
                UserId = userId,
                RoleId = item,
            }).ToList();

            var handler = new UserRoleModifyHandler(list);
            var result = handler.Invoke();

            return Json(new { success = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        //创建/编辑
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new UserAccount();
            if (id.HasValue)
            {
                var service = new GetEntityByIdService<UserAccount>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<UserAccount, VmUserAccountEdit>());
        }

        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmUserAccountEdit model)
        {
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new UserAccountCreateHandler(model);
                var res = handler.Invoke();
                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                //ModelState.Remove("LoginName");
                //model.LoginName = null;
                var handler = new UserAccountModifyHandler(model);
                var res = handler.Invoke();
                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}
            //model.ErrorMessage = "操作失败";
            //ModelState.AddModelError("error", model.ErrorMessage);
            //return Json(new { status = false, errors = GetErrors() });
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string loginName)
        {
            var service = new GetEntityBySingleColumnService<UserAccount> { ColumnMember = x => x.LoginName, ColumnValue = loginName };

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
        public ActionResult AjaxList(VmUserAccountEdit model, Guid? UserId)
        {
            if (model.PageIndex == 0) model.PageIndex = 1;
            if (model.PageSize == 0) model.PageSize = 10;
            var service = new CustomSearchWithPaginationService<UserAccount>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<UserAccount>>
                {
                    new CustomConditionPlus<UserAccount>
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<UserAccount, object>>[] { x => x.LoginName,x=>x.RealName }
                    }
                },
                SortMember = new Expression<Func<UserAccount, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<UserAccount>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<UserAccount, object>>[] { x => x.State }
                });
            }

            if (UserId.HasValue)
            {
                service.CustomConditions.Add(new CustomConditionPlus<UserAccount>
                {
                    Value = UserId,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<UserAccount, object>>[] { x => x.DataId }
                });
            }
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new UserAccount();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<UserAccount>(id.Value);
                entity = service.Invoke() ?? new UserAccount();
            }

            return View(entity);
        }

        #region 功能
        public VmUserAccountEdit GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<UserAccount>(Id);
            var entity = service.Invoke();
            return entity.CreateViewModel<UserAccount, VmUserAccountEdit>();
        }
        public Guid UserAccountAddEdit(VmUserAccountEdit model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<UserAccount>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Model.DataModel.User.UserAccount>(model);
                var res = handler.Invoke();
                if (res.Code == 0)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }


        public Guid UserRoleAdd(VmUserRole model)
        {
            var handler = new BaseCreateHandler<UserRole>(model);
            var res = handler.Invoke();
            if (res.Code == 0)
                return res.Output;
            return Guid.Empty;

        }
        #endregion
    }
}
