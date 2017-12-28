using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Agent;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Agent;
using XZMY.Manage.Model.ViewModel.User;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers.Agent;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Utils.DataDictionary;
using XZMY.Manage.Web.Controllers.Auth;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Planners
{
    /// <summary>
    /// 代理商
    /// </summary>
    public class AgentController : ControllerBase
    {
        #region 页面
        //详细
        public ActionResult Details(Guid? id)
        {
            var vm = new VmAgentEdit();
            if (id.HasValue)
            {
                {
                    var service = new GetEntityByIdService<Agent>(id.Value);
                    var entity = service.Invoke() ?? new Agent();
                    vm = entity.CreateViewModel<Agent, VmAgentEdit>();
                }

                {
                    var AgentContacts = GetAgentContact(id.Value);
                    if (AgentContacts != null)
                        vm.AgentContact = AgentContacts.FirstOrDefault(x => x.IsMain);
                }

                {
                    var service = new GetEntityByIdService<UserAccount>(vm.UserId);
                    var entity = service.Invoke() ?? new UserAccount();

                    vm.LoginName = entity.LoginName;
                    vm.RealName = entity.RealName;
                    vm.Gender = entity.Gender;
                    vm.State = entity.State;
                }

                {//数据字典获取
                    var list = new List<Guid>
                    {
                        vm.LevelId,
                        vm.NatureId,
                        vm.CategoryId
                    };
                    var dict = DataDictionaryManager.GetDataByIdList(list);

                    vm.LevelName = dict.GetName(vm.LevelId);
                    vm.NatureName = dict.GetName(vm.NatureId);
                    vm.CategoryName = dict.GetName(vm.CategoryId);
                }
            }

            return View(vm);
        }
        //列表
        [AutoCreateAuthAction(Name = "代理商列表", Code = "AgentList", ModuleCode = "AGENT", Url = "/Agent/List", Visible = true)]
        public ActionResult List()
        {
            return View();
        }

        //创建/编辑
        [HttpGet]
        [AutoCreateAuthAction(Name = "创建代理商", Code = "AgentEdit", ModuleCode = "AGENT", Url = "/Agent/Edit", Visible = true)]
        public ActionResult Edit(Guid? id)
        {
            var entity = new VmAgentEdit();
            if (id.HasValue)
            {
                {
                    var service = new GetEntityByIdService<Agent>(id.Value);
                    entity = service.Invoke().ConvertTo<VmAgentEdit>();
                }

                {
                    var AgentContacts = GetAgentContact(id.Value);
                    if (AgentContacts != null)
                        entity.AgentContact = AgentContacts.FirstOrDefault(x => x.IsMain);
                }

                {
                    var service = new GetEntityByIdService<UserAccount>(entity.UserId);
                    var userAccount = service.Invoke() ?? new UserAccount();

                    entity.LoginName = userAccount.LoginName;
                    entity.RealName = userAccount.RealName;
                    entity.Gender = userAccount.Gender;
                    entity.State = userAccount.State;
                }
            }
            return View(entity);
        }

        /// <summary>
        /// 代理商学生列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentList()
        {
            VmAgent model = new VmAgent();
            Guid id = Guid.Empty;
            if (LoggedUserManager.IsLogin())
            {
                var User = LoggedUserManager.GetCurrentUserAccount();
                model = GetAgentUserId(User.AccountId);
            }
            if (Request.QueryString["AgentId"] != null)
            {
                Guid.TryParse(Request.QueryString["AgentId"], out id);
                model = GetAgent(id);
            }
            return View(model);
        }
        #endregion

        #region AJAX
        //保存 创建/编辑
        [HttpPost]
        public ActionResult AjaxEdit(VmAgentEdit model)
        {
            model.CategoryId = Request.Params["CategoryId"].ToGuid(Guid.Empty);
            model.NatureId = Request.Params["NatureId"].ToGuid(Guid.Empty);
            model.LevelId = Request.Params["LevelId"].ToGuid(Guid.Empty);
   
            //if (ModelState.IsValid)
            //{
            if (model.DataId == Guid.Empty)
            {
                var handler = new AgentCreateHandler(model);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    return Json(new { success = false, message = res.Message });
                }
                else
                {
                    //权限
                    var UserRole = new VmUserRole()
                    {
                        UserId = model.UserId,
                        RoleId = new Guid(ConfigurationManager.AppSettings["RoleAgentId"].ToString())
                    };
                    UserAccountController bllUserAccount = new UserAccountController();
                    bllUserAccount.UserRoleAdd(UserRole);
                }

                return Json(new { success = res.Success, Id = model.DataId });
            }
            else
            {
                var handler = new AgentModifyHandler(model);
                var res = handler.Invoke();

                return Json(new { success = res.Success, Id = model.DataId, message = res.Message });
            }
            //}
            //return Json(new { status = false, errors = GetErrors() });
        }

        //删除

        //验证 是否重复
        public ActionResult AjaxIsExist(Guid? id, string companyName)
        {
            var service = new GetEntityBySingleColumnService<Agent> { ColumnMember = x => x.CompanyName, ColumnValue = companyName };

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
        public ActionResult AjaxList(VmAgentEdit model)
        {
            var service = new CustomSearchWithPaginationService<Agent>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                CustomConditions = new List<CustomCondition<Agent>>
                   {
                       new CustomConditionPlus<Agent>
                       {
                           Value = model.Keyword ?? string.Empty,
                           Operation = SqlOperation.Like,
                        Member   = new Expression<Func<Agent, object>>[] { x => x.CompanyName, x=>x.Description }
                       }
                   },
                SortMember = new Expression<Func<Agent, object>>[] { x => x.CreatedTime }
            };

            //if (model.State > 0)
            //{
            //    service.CustomConditions.Add(new CustomConditionPlus<Agent>
            //    {
            //        Value = model.State,
            //        Operation = SqlOperation.Equals,
            //        Member = new Expression<Func<Agent, object>>[] { x => x.State }
            //    });
            //}

            var result = service.Invoke();

            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 功能
        /// <summary>
        /// 获取代理商信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmAgent GetAgent(Guid Id)
        {
            var entity = new Agent();
            var service = new GetEntityByIdService<Agent>(Id);
            entity = service.Invoke();
            return entity.CreateViewModel<Agent, VmAgent>();
        }
        /// <summary>
        /// 通过UserID获取代理商信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public VmAgent GetAgentUserId(Guid UserId)
        {
            VmAgent model = new VmAgent();
            var service = new CustomSearchWithPaginationService<Agent>
            {
                PageIndex = 1,
                PageSize = 1,
                CustomConditions = new List<CustomCondition<Agent>>()
                {
                    new CustomConditionBase<Agent>()
                    {
                        Value =UserId,
                        Operation = SqlOperation.Equals,
                        Member = x => x.UserId
                    }
                }
            };
            var result = service.Invoke();
            if (result.Results.Count > 0)
            {
                model = result.Results[0].CreateViewModel<Agent, VmAgent>();
            }
            return model;
        }

        /// <summary>
        /// 联系人
        /// </summary>
        /// <param name="AgentId"></param>
        /// <returns></returns>
        private List<VmAgentContact> GetAgentContact(Guid AgentId)
        {
            List<VmAgentContact> list = null;

            var service = new CustomSearchWithPaginationService<AgentContact>
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                CustomConditions = new List<CustomCondition<AgentContact>>()
                {
                    new CustomConditionBase<AgentContact>()
                    {
                        Value =AgentId,
                        Operation = SqlOperation.Equals,
                        Member = x => x.AgentId
                    }
                }
            };

            var result = service.Invoke();

            if (result.Results != null && result.Results.Any())
            {
                list = new List<VmAgentContact>();

                foreach (var item in result.Results)
                {
                    list.Add(item.CreateViewModel<AgentContact, VmAgentContact>());
                }
            }
            return list;
        }
        #endregion
    }
}