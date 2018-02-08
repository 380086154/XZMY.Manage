using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;
using XZMY.Manage.Service.Handlers.Role;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Service.Handlers;

namespace XZMY.Manage.Web.Controllers.Auth
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleController : ControllerBase
    {
        //列表
        [AutoCreateAuthAction(Name = "角色管理", Code = "RoleList", ModuleCode = "SYSTEM", Url = "/Role/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            return View();
        }

        //赋值
        public ActionResult Assignment(Guid? id)
        {
            //根据角色Id获取菜单信息
            if (id.HasValue)
            {
                var ads = new AuthDataSource();

                var roleResource = ads.GetRoleResource(id.Value);

                var moduleIdList = roleResource.GetModuleIdList();
                var actionIdList = roleResource.GetActionIdList();

                var idList = new List<Guid>();
                idList.AddRange(moduleIdList);
                idList.AddRange(actionIdList);

                ViewBag.IdList = JsonConvert.SerializeObject(idList);
                {
                    var service = new GetEntityByIdService<Sys_Role>(id.Value);
                    var entity = service.Invoke() ?? new Sys_Role();
                    ViewBag.Name = string.IsNullOrWhiteSpace(entity.Name) ? "当前角色" : entity.Name;
                }

                ViewBag.Id = id.Value;
            }

            return View();
        }

        //加载 Tree 内容
        public ActionResult AjaxAssignmentList(VmSearchBase model)
        {
            var resourceList = new List<VmMenu>();
            //获取所有资源信息
            var ads = new AuthDataSource();

            var modules = ads.GetAllModules().OrderBy(x => x.Sort).ToList();
            var actions = ads.GetAllActions();

            var roleResource = ads.GetRoleResource(model.Id);

            var idList = roleResource.GetActionIdList();

            foreach (var item in modules)
            {
                resourceList.Add(new VmMenu
                {
                    id = item.DataId,
                    pId = item.ParentId,
                    name = item.Name,
                    font = item.State == EState.禁用 ? "{color:red}" : "",
                    open = true,

                    Type = "1",
                    Sort = item.Sort,
                    Visible = item.Visible.GetHashCode(),
                    State = item.State.GetHashCode()
                });

                resourceList.AddRange(actions.Where(x => x.ModuleId == item.DataId).Select(x => new VmMenu
                {
                    id = x.DataId,
                    pId = item.DataId,
                    name = x.Name,
                    font = item.State == EState.禁用 || x.State == EState.禁用 ? "{color:red}" : "",
                    open = true,

                    Type = "2",
                    Sort = x.Sort,
                    Visible = x.Visible.GetHashCode(),
                    State = x.State.GetHashCode()
                }));
            }


            return Json(new { success = true, rows = GetItem(modules, actions, idList), errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //获取 JSON 数据
        public string GetItem(IList<Sys_Module> modules, IList<Sys_Action> actions, IList<Guid> idList, bool isChildren = true)
        {
            if (modules == null || !modules.Any()) return "[]";
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var module in modules)
            {
                var childrenList = actions.Where(x => x.ModuleId == module.DataId).OrderBy(x => x.Sort).ToList();

                sb.AppendFormat(@"{{""text"":""{0}""", module.Name.Escape());
                sb.AppendFormat(@",""id"":""{0}_1""", module.DataId);//id 后面的数字用于判断是 Module 还是 Action
                sb.AppendFormat(@",""sort"":""{0}""", module.Sort);
                sb.AppendFormat(@",""status"":""{0}""", module.State.GetHashCode());
                sb.Append(@",""state"":{opened:true}");

                if (childrenList.Any())
                {
                    sb.AppendFormat(@",""children"":{0}", GetActionItem(childrenList, idList, true));//是否有子级
                }

                sb.Append("},");
            }

            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);//剔除最后一个逗号
            sb.Append("]");

            return sb.ToString();
        }

        //获取 JSON 数据
        public string GetActionItem(IList<Sys_Action> childrenList, IList<Guid> idList, bool isChildren = false)
        {
            if (childrenList == null || !childrenList.Any()) return "[]";
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var item in childrenList)
            {
                sb.AppendFormat(@"{{""text"":""{0}""", item.Name.Escape());
                sb.AppendFormat(@",""id"":""{0}_2""", item.DataId);//id 后面的数字用于判断是 Module 还是 Action
                sb.AppendFormat(@",""sort"":""{0}""", item.Sort);
                sb.AppendFormat(@",""status"":""{0}""", item.State.GetHashCode());

                if (idList.Contains(item.DataId)) sb.Append(@",""state"":{selected:true}");

                sb.Append("},");
            }

            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);//剔除最后一个逗号
            sb.Append("]");

            return sb.ToString();
        }

        //保存变更权限
        public ActionResult AjaxSaveAssignment()
        {
            var id = (Request.Params["id"] ?? "").ToGuid(Guid.Empty);

            var service = new GetEntityByIdService<Sys_Role>(id);
            var entity = service.Invoke();
            var model = entity.ConvertTo<VmRoleEdit>();

            model.Modules = (Request.Params["module"] ?? "");//这个数据暂时没用了
            model.Actions = (Request.Params["action"] ?? "");

            var ads = new AuthDataSource();
            var moduleIdList = new List<Guid>();

            foreach (var item in ads.GetAllActions())
            {
                if (model.Actions.Contains(item.DataId.ToString()))
                {
                    if (!moduleIdList.Contains(item.ModuleId))
                        moduleIdList.Add(item.ModuleId);
                }
            }

            model.Modules = string.Join(",", moduleIdList);

            var handler = new RoleModifyHandler(model);
            var res = handler.Invoke();

            return Json(new { success = res.Success, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //重命名
        public ActionResult AjaxRename()
        {
            var type = (Request.Params["Type"] ?? "").ToInt32(1);
            var id = (Request.Params["id"] ?? "").ToGuid(Guid.Empty);
            var name = Request.Params["name"] ?? "";
            var flag = false;

            if (type == 1) //模块
            {
                var model = new VmActionEdit
                {
                    DataId = id,
                    Name = name
                };

                //var handler = new BaseModifyHandler<Sys_Action>(model);
                //var res = handler.Invoke();

            }
            else//操作
            {
                var model = new VmActionEdit
                {
                    DataId = id,
                    Name = name
                };

                //var handler = new BaseModifyHandler<Sys_Action>(model);
                //var res = handler.Invoke();
            }


            return Json(new { success = flag, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //删除
        public ActionResult AjaxDelete()
        {
            var model = new VmRoleEdit
            {
                DataId = (Request.Params["id"] ?? "").ToGuid(Guid.Empty),
                Modules = (Request.Params["module"] ?? ""),
                Actions = (Request.Params["action"] ?? "")
            };

            var handler = new RoleModifyHandler(model);
            var res = handler.Invoke();

            return Json(new { success = res.Success, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        
        //创建/编辑
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var entity = new Sys_Role();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Sys_Role>(id.Value);
                entity = service.Invoke();
            }
            return View(entity.CreateViewModel<Sys_Role, VmRoleEdit>());
        }

        //保存 创建/编辑 
        [HttpPost]
        public ActionResult AjaxEdit(VmRoleEdit model)
        {
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new RoleCreateHandler(model);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    return Json(new { success = false, errors = GetErrors() });
                }

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            else
            {
                var handler = new RoleModifyHandler(model);
                var res = handler.Invoke();

                return Json(new { success = res.Success, Id = model.DataId, errors = GetErrors() });
            }
            //}

            //return Json(new { status = false, errors = GetErrors() });
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string name)
        {
            var service = new GetEntityBySingleColumnService<Sys_Role> { ColumnMember = x => x.Name, ColumnValue = name };

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
        public ActionResult AjaxList(VmRoleEdit model)
        {
            var service = new CustomSearchWithPaginationService<Sys_Role>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Sys_Role>>()
                {
                    new CustomConditionPlus<Sys_Role>()
                    {
                        Value = model.Keyword ?? string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<Sys_Role, object>>[] { x => x.Name,x=>x.Description }
                    }
                },
                SortMember = new Expression<Func<Sys_Role, object>>[] { x => x.CreatedTime }
            };

            if (model.State > 0)
            {
                service.CustomConditions.Add(new CustomConditionPlus<Sys_Role>
                {
                    Value = model.State,
                    Operation = SqlOperation.Equals,
                    Member = new Expression<Func<Sys_Role, object>>[] { x => x.State }
                });
            }

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        //详细
        public ActionResult Details(Guid? id)
        {
            var entity = new Sys_Role();

            if (id.HasValue)
            {
                var service = new GetEntityByIdService<Sys_Role>(id.Value);
                entity = service.Invoke() ?? new Sys_Role();
            }

            return View(entity);
        }
    }
}